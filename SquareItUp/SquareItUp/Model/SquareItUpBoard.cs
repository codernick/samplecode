using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
namespace SquareItUp.Model
{
    public class SquareItUpBoard
    {
        private Peg[,] myBoard;
        private int myRow, myCol;
        private ArrayList myPegZeroList = new ArrayList();
        public ArrayList PegZeroList
        {
            get
            {
                if (myPegZeroList == null)
                    myPegZeroList = new ArrayList();
                
                SetZeroList();

                return myPegZeroList;
            }
        }
        public Peg[,] Board
        {
            get
            {
                return myBoard;
            }
        }
        public int Row
        {
            get
            {
                return myRow;
            }
            set
            {
                myRow = value;
            }
        }
        public int Col
        {
            get
            {
                return myCol;
            }
            set
            {
                myCol = value;
            }
        }
        public bool IsGameOver()
        {
            SetZeroList();
            return myPegZeroList.Count == 0?true:false;
            //return false;
        }
        public void InitializeBoardValues(int myRow, int myCol, int X, int Y)
        {
            if (myBoard != null)
            {
                /*for (int myRow = 0; myRow < Math.Sqrt(myBoard.Length); myRow++)
                {
                    for (int myCol = 0; myCol < Math.Sqrt(myBoard.Length); myCol++)
                    {*/
                Peg myPeg = new Peg();
                myPeg.Col = myCol;
                myPeg.Row = myRow;
                myPeg.X = X;
                myPeg.Y = Y;
                
                if ((myCol % 2 == 0) && (myRow % 2 == 0))
                {
                    myPeg.Type = Peg.PegType.Solid;
                }
                if ((myCol % 2 == 1) && (myRow % 2 == 1))
                {
                    myPeg.Type = Peg.PegType.None;
                }
                if (((myCol % 2 == 0) && (myRow % 2 == 1)) ||
                    ((myCol % 2 == 1) && (myRow % 2 == 0)))
                {
                    myPeg.Type = Peg.PegType.Zero;
                   
                    myPeg.poly[0] = new Point(X - 25, Y);
                    myPeg.poly[1] = new Point(X, Y - 25);
                    myPeg.poly[2] = new Point(X + 25, Y);
                    myPeg.poly[3] = new Point(X, Y + 25);
                }
                myBoard[myRow, myCol] = myPeg;
                myPeg = null;
                //}
                //}
            }
        }
        public void InitializeBoard(int myArraySize)
        {
            if (myArraySize > 0)
            {
                myBoard = new Peg[myArraySize, myArraySize];
                //InitializeBoardValues();
            }
            else
                myBoard = null;
        }
        public void EditBoard(int row, int col, Peg myPeg)
        {
            if (myBoard != null)
                myBoard[row, col] = myPeg;
        }

        public void AddPegZeroCoordinateToList(Peg peg)
        {
            if (myPegZeroList != null && peg != null)
            {
                myPegZeroList.Add(peg);
            }
        }
        public void SetZeroList()
        {
            myPegZeroList.Clear();
            for (int myRow = 0; myRow < Math.Sqrt(myBoard.Length); myRow++)
            {
                for (int myCol = 0; myCol < Math.Sqrt(myBoard.Length); myCol++)
                {
                    if (myBoard[myRow, myCol].Type == Peg.PegType.Zero)
                        AddPegZeroCoordinateToList(myBoard[myRow, myCol]);
                }
            }
        }
    }
}
