using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SquareItUp.Model
{
    public class MakeMove
    {
        private int myRow = -1;
        private int myCol = -1;

        private int myX = -1;
        private int myY = -1;
        private Random myRandom;
        public int X
        {
            get
            {
                return myX;
            }
        }
        public int Y
        {
            get
            {
                return myY;
            }
        }
        public int Col
        {
            get
            {
                return myCol;
            }
        }
        public int Row
        {
            get
            {
                return myRow;
            }
        }
        public void TakeDecisionForLevel( SquareItUpBoard board, int myArraySize)
        {
            int index = RandomNumberGenerator(board.PegZeroList.Count);
            Peg myPeg = (Peg)board.PegZeroList[index];
            myRow = myPeg.Row;
            myCol = myPeg.Col;
            myX = myPeg.X;
            myY = myPeg.Y;
            myPeg.Type = Peg.PegType.One;
            board.EditBoard(myPeg.Row, myPeg.Col,myPeg );
            //board.PegZeroList.RemoveAt(index);
        }

        public int RandomNumberGenerator(int myArraySize)
        {
            if (0 > myArraySize)
                return -1;
            if(myRandom == null)
                myRandom = new Random();
            return myRandom.Next(0,myArraySize);
        }

     
    }
}
