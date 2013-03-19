using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace SquareItUp.Model
{
    public class Peg
    {
        private int myHeight;
        private int myWidth;
        private int myX;
        private int myY;
        private int myRow;
        private int myCol;
        private PegType myType;
        public Point[] poly = new Point[4];
        
        public int Height
        {
            get 
            { 
                return myHeight; 
            }
            set 
            { 
                myHeight = value; 
            }
        }
        public int Width
        {
            get
            {
                return myWidth;
            }
            set
            {
                myWidth = value;
            }
        }
        public int X
        {
            get
            {
                return myX;
            }
            set
            {
                myX = value;
            }
        }
        public int Y
        {
            get
            {
                return myY;
            }
            set
            {
                myY = value;
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

        public PegType Type
        {
            get
            {
                return myType;
            }
            set
            {
                myType = value;
            }
        }
        
       public enum PegType{ Zero, One, None, Solid};
        

    }
}
