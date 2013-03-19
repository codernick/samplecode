using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Windows.Forms;
using SquareItUp.Model;

namespace SquareItUp
{
    public partial class frmHome : Form
    {
        bool isVSPhone;
        bool hasPlayerMoved;
        int level;
        int arraySize;
        int boardSize;
        int originX, originY;
        int locationOnScreen;
        int player1points = 0, player2points = 0;
        SquareItUpBoard myBoard;
        MakeMove myPhoneMove;
        Settings mySettings;
        public frmHome()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            myBoard.InitializeBoard(arraySize);
            DrawBoard();
        }

        private void DrawBoard()
        {
            int x, y = 40;
            originX = 10;
            originY = 40;
            myBoard = new SquareItUpBoard();
            myBoard.InitializeBoard(arraySize);
            for (int row = 0; row < arraySize; row++)
            {
                x = 10;
                for (int col = 0; col < arraySize; col++)
                {
                    myBoard.InitializeBoardValues(row, col, x, y);
                    x += 25;
                }
                y += 25;
            }
            y = 40;
            for (int row = 0; row < boardSize; row++)
            {
                x = 10;
                for (int col = 0; col < boardSize; col++)
                {
                    PictureBox myPicBox = new PictureBox();
                    myPicBox.Image = Resource.node2;
                    myPicBox.Location = new Point(x, y);
                    myPicBox.Height = 15;
                    myPicBox.Width = 15;
                    this.Controls.Add(myPicBox);
                    x += 50;
                }
                y += 50;
            }
            myBoard.PegZeroList.ToString();
        }
        private bool PointInPolygon(Point p, Point[] poly)
        {
            Point p1, p2;
            bool inside = false;
            if (poly.Length < 3)
                return inside;
            Point oldPoint = new Point(poly[poly.Length - 1].X, poly[poly.Length - 1].Y);
            for (int i = 0; i < poly.Length; i++)
            {
                Point newPoint = new Point(poly[i].X, poly[i].Y);
                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }
                if ((newPoint.X < p.X) == (p.X <= oldPoint.X)
                    && ((long)p.Y - (long)p1.Y) * (long)(p2.X - p1.X)
                     < ((long)p2.Y - (long)p1.Y) * (long)(p.X - p1.X))
                {
                    inside = !inside;
                }
                oldPoint = newPoint;
            }
            return inside;
        }

        private void frmHome_Load(object sender, EventArgs e)
        {

            mySettings = new Settings();
            hasPlayerMoved = mySettings.GetXMLNodeValue("whoplaysfirst") == "P2" ? true : false;
            isVSPhone = mySettings.GetXMLNodeValue("againstwhom") == "Phone" ? true : false;
            level = Convert.ToInt32(mySettings.GetXMLNodeValue("level"));
            arraySize = 9;
            boardSize = 5;
            locationOnScreen = 50;
            // hasPlayerMoved = isFirstMoveByPlayer2;
            myPhoneTimer.Enabled = true;
            myPhoneMove = new MakeMove();
            DrawBoard();
            //x = 10 y = 40
            Point point = new Point(37, 41);

            Point point1 = new Point(10, 40);
            Point point2 = new Point(35, 15);
            Point point3 = new Point(60, 40);
            Point point4 = new Point(35, 65);

            Point[] poly = new Point[4];
            poly[0] = point1;
            poly[1] = point2;
            poly[2] = point3;
            poly[3] = point4;
            if (PointInPolygon(point, poly))
                myScore.Text = "Inside";
            else
                myScore.Text = "Outside";

            //PointInPolygon(
        }

        private bool isPointinRange(Point point)
        {
            Point point1 = new Point(originX, originY);
            Point point2 = new Point(originX + (30 * (arraySize - 1)), originY);
            Point point3 = new Point(originX + (30 * (arraySize - 1)), originY + (30 * (arraySize - 1)));
            Point point4 = new Point(originX, originY + (30 * (arraySize - 1)));

            Point[] poly = new Point[4];
            poly[0] = point1;
            poly[1] = point2;
            poly[2] = point3;
            poly[3] = point4;
            return PointInPolygon(point, poly);

        }


        private void frmHome_MouseDown(object sender, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);
            if (isPointinRange(point))
            {
                if (!myBoard.IsGameOver() && !hasPlayerMoved)
                {
                    foreach (Peg peg in myBoard.PegZeroList)
                    {
                        if (PointInPolygon(point, peg.poly))
                        {
                            Peg myNewPeg = new Peg();
                            myNewPeg.Col = peg.Col;
                            myNewPeg.Row = peg.Row;
                            myNewPeg.X = peg.X;
                            myNewPeg.Y = peg.Y;
                            myNewPeg.Type = Peg.PegType.One;
                            myBoard.EditBoard(peg.Row, peg.Col, myNewPeg);
                            myNewPeg = null;

                            if (peg.Col % 2 == 1 && peg.Row % 2 == 0)
                                DrawHorizontalLine(peg.X - 15, peg.Y + 5);
                            else
                                DrawVerticalLine(peg.X + 5, peg.Y - 15);
                            int localPlayer1Points = player1points;
                            UpdateScore(peg.Row, peg.Col);
                            if (localPlayer1Points == player1points)
                                hasPlayerMoved = true;
                        }
                    }
                }
            }
        }
        private void UpdateScore(int currentRow, int currentCol)
        {
            if (currentRow % 2 == 0)
            {
                try
                {
                    if (((Peg)myBoard.Board[currentRow - 1, currentCol - 1]).Type == Peg.PegType.One &&
                     ((Peg)myBoard.Board[currentRow - 1, currentCol + 1]).Type == Peg.PegType.One &&
                     ((Peg)myBoard.Board[currentRow - 2, currentCol]).Type == Peg.PegType.One)
                        if (!hasPlayerMoved)
                            player1points++;
                        else
                            player2points++;
                }
                catch
                { }
                try
                {
                    if (((Peg)myBoard.Board[currentRow + 1, currentCol - 1]).Type == Peg.PegType.One &&
                     ((Peg)myBoard.Board[currentRow + 1, currentCol + 1]).Type == Peg.PegType.One &&
                     ((Peg)myBoard.Board[currentRow + 2, currentCol]).Type == Peg.PegType.One)
                        if (!hasPlayerMoved)
                            player1points++;
                        else
                            player2points++;
                }
                catch
                { }
            }
            else
            {
                try
                {
                    if (((Peg)myBoard.Board[currentRow - 1, currentCol - 1]).Type == Peg.PegType.One &&
                       ((Peg)myBoard.Board[currentRow + 1, currentCol - 1]).Type == Peg.PegType.One &&
                       ((Peg)myBoard.Board[currentRow, currentCol - 2]).Type == Peg.PegType.One)
                        if (!hasPlayerMoved)
                            player1points++;
                        else
                            player2points++;
                }
                catch
                { }
                try
                {
                    if (((Peg)myBoard.Board[currentRow - 1, currentCol + 1]).Type == Peg.PegType.One &&
                     ((Peg)myBoard.Board[currentRow + 1, currentCol + 1]).Type == Peg.PegType.One &&
                     ((Peg)myBoard.Board[currentRow, currentCol + 2]).Type == Peg.PegType.One)
                        if (!hasPlayerMoved)
                            player1points++;
                        else
                            player2points++;
                }
                catch
                { }
            }
            myScore.Text = " Player1: " + player1points + " Player2: " + player2points;
        }

        private void DrawHorizontalLine(int x, int y)
        {
            PictureBox myPicBox = new PictureBox();
            myPicBox.Image = Resource.line1;
            myPicBox.Location = new Point(x, y);
            myPicBox.Height = 1;
            myPicBox.Width = 38;
            this.Controls.Add(myPicBox);
        }

        private void DrawVerticalLine(int x, int y)
        {
            PictureBox myPicBox = new PictureBox();
            myPicBox.Image = Resource.line1;
            myPicBox.Location = new Point(x, y);
            myPicBox.Height = 38;
            myPicBox.Width = 1;
            this.Controls.Add(myPicBox);
        }

        private void myPhoneTimer_Tick(object sender, EventArgs e)
        {
            if (!myBoard.IsGameOver() && hasPlayerMoved)
            {
                myPhoneMove.TakeDecisionForLevel(myBoard, arraySize);

                if (myPhoneMove.Col % 2 == 1 && myPhoneMove.Row % 2 == 0)
                    DrawHorizontalLine(myPhoneMove.X - 15, myPhoneMove.Y + 5);
                else
                    DrawVerticalLine(myPhoneMove.X + 5, myPhoneMove.Y - 15);
                int localplayer2points = player2points;
                UpdateScore(myPhoneMove.Row, myPhoneMove.Col);
                if (localplayer2points == player2points)
                    hasPlayerMoved = false;
            }
            if (myBoard.IsGameOver())
            {
                if (player1points > player2points)
                    MessageBox.Show("Player1 Wins!");
                else if (player1points < player2points)
                    MessageBox.Show("Player2 Wins!");
                else
                    MessageBox.Show("It is a tie!");
                myPhoneTimer.Enabled = false;
            }
        }

        private void btnPhone_Click(object sender, EventArgs e)
        {
            mySettings.ModifySettingsFile("againstwhom", "Phone");
        }

        private void btnOtherPlayer_Click(object sender, EventArgs e)
        {
            mySettings.ModifySettingsFile("againstwhom", "OP");
        }

        private void btnLevel1_Click(object sender, EventArgs e)
        {
            mySettings.ModifySettingsFile("level", "1");
        }

        private void btnLevel2_Click(object sender, EventArgs e)
        {
            mySettings.ModifySettingsFile("level", "2");
        }

        private void btnLevel3_Click(object sender, EventArgs e)
        {
            mySettings.ModifySettingsFile("level", "3");
        }

        private void btnLevel4_Click(object sender, EventArgs e)
        {
            mySettings.ModifySettingsFile("level", "4");
        }

        private void btnLevel5_Click(object sender, EventArgs e)
        {
            mySettings.ModifySettingsFile("level", "5");
        }

        private void btnPlayer1_Click(object sender, EventArgs e)
        {
            mySettings.ModifySettingsFile("whoplaysfirst", "P1");
        }

        private void btnPlayer2_Click(object sender, EventArgs e)
        {
            mySettings.ModifySettingsFile("whoplaysfirst", "P2");
        }
    }
}