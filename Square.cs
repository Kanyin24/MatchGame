using System;
using System.Drawing;

namespace MatchGame
{
    class Square
    {
        //fields
        private int mSize;
        private Color mBackColour;
        private Color mBorderColour;
        private Color mHiddenColour;
        private bool mMatched;
        

        //properties
        public int Size
        {
            get { return mSize; }
            set { mSize = value; }
        }

        public Color BackColour
        {
            get { return mBackColour; }
            set { mBackColour = value; }
        }

        public Color BorderColour
        {
            get { return mBorderColour; }
            set { mBorderColour = value; }
        }
        public Color HiddenColour
        {
            get { return mHiddenColour; }
            set { mHiddenColour = value; }
        }
        public bool Matched
        {
            get { return mMatched; }
            set { mMatched = value; }
        }

        //constructor
        public Square()
        {
            //default property of squares
            mSize = 30;
            mBackColour = Color.White;
            mBorderColour = Color.Black;
        }

        public Square(int Size, Color BackColour, Color BorderColour, Color HiddenColour, bool Matched)
        {
            //properties of square with input
            mSize = Size;
            mBackColour = BackColour;
            mBorderColour = BorderColour;
            mHiddenColour = HiddenColour;
            mMatched = Matched;
        }

        //methods
        public void Draw(Graphics g, int X, int Y)
        {
            //create the pen and brush
            Pen BorderPen = new Pen(mBorderColour);
            SolidBrush BackBrush = new SolidBrush(mBackColour);

            //drawing the cell
            g.FillRectangle(BackBrush, X, Y, mSize, mSize);
            g.DrawRectangle(BorderPen, X, Y, mSize, mSize);

            //dispose of pen and brush
            BorderPen.Dispose();
            BackBrush.Dispose();
        }

        
    }
}
