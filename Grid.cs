using System;
using System.Drawing;
using System.Threading.Tasks;

namespace MatchGame
{
    class Grid
    {
        //fields
        private Square[,] mGrid;
        private int mSize;
        private int mRows;
        private int mColumns;
        private int mScoreP1;
        private int mScoreP2; 
        private Color mFirstGuess;
        private Color mSecondGuess;
        private bool mPlayer1 = true;
        private bool mPlayer2;
        private int mTotalTile = 0;
        

        //properties
        public Color FirstGuess
        {
            get { return mFirstGuess; }
            set { mFirstGuess = value; }
        }
        public Color SecondGuess
        {
            get { return mSecondGuess; }
            set { mSecondGuess = value; }
        }
        public bool Player1
        {
            get { return mPlayer1; }
            set { mPlayer1 = value; }
        }
        public bool Player2
        {
            get { return mPlayer2; }
            set { mPlayer2 = value; }
        }
        public int ScoreP1
        {
            get { return mScoreP1; }
            set { mScoreP1 = value; }
        }
        public int ScoreP2
        {
            get { return mScoreP2; }
            set { mScoreP2 = value; }
        }
        public int TotalTile
        {
            get { return mTotalTile; }
            set { mTotalTile = value; }
        }
        

        //constructors

        public Grid(int Rows, int Columns)
        {
            mRows = Rows;
            mColumns = Columns;
            mGrid = new Square[mRows, mColumns];

            //create each cell in the grid/array and assign colours
            mSize = 30;

            for (int i = 0; i < mRows; i++)
            {
                for (int j = 0; j < mColumns; j++)
                {
                    if (i == 0 && j == 0 || i == 0 && j == 1)
                    {
                        mGrid[i, j] = new Square(mSize, Color.White, Color.Black, Color.Red, false);
                    }
                    else if (i == 0 && j == 2 || i == 0 && j == 3)
                    {
                        mGrid[i, j] = new Square(mSize, Color.White, Color.Black, Color.Blue, false);
                    }
                    else if (i == 1 && j == 0 || i == 1 && j == 1)
                    {
                        mGrid[i, j] = new Square(mSize, Color.White, Color.Black, Color.Green, false);
                    }
                    else if (i == 1 && j == 2 || i == 1 && j == 3)
                    {
                        mGrid[i, j] = new Square(mSize, Color.White, Color.Black, Color.Maroon, false);
                    }
                    else if (i == 2 && j == 0 || i == 2 && j == 1)
                    {
                        mGrid[i, j] = new Square(mSize, Color.White, Color.Black, Color.Brown, false);
                    }
                    else if (i == 2 && j == 2 || i == 2 && j == 3)
                    {
                        mGrid[i, j] = new Square(mSize, Color.White, Color.Black, Color.Black, false);
                    }
                    else if (i == 3 && j == 0 || i == 3 && j == 1)
                    {
                        mGrid[i, j] = new Square(mSize, Color.White, Color.Black, Color.Yellow, false);
                    }
                    else if (i == 3 && j == 2 || i == 3 && j == 3)
                    {
                        mGrid[i, j] = new Square(mSize, Color.White, Color.Black, Color.Orange, false);
                    }
                    else if (i == 4 && j == 0 || i == 4 && j == 1)
                    {
                        mGrid[i, j] = new Square(mSize, Color.White, Color.Black, Color.Violet, false);
                    }
                    else
                    {
                        mGrid[i, j] = new Square(mSize, Color.White, Color.Black, Color.Aqua, false);
                    }
                }
            }

        }



        //methods
        
        public Square GetSquare(int RowIndex, int ColumnIndex)
        {
            return mGrid[RowIndex, ColumnIndex];
        }

        
        public void Draw(Graphics g, int X, int Y)
        {
            //co-ordinate values for looping and drawing the grid
            int pX = X;
            int pY = Y;


            for (int i = 0; i < mRows; i++)
            {
                for (int j = 0; j < mColumns; j++)
                {
                    mGrid[i, j].Draw(g, pX, pY);
                    pX += mSize;
                }
            
                pX = X;
                pY += mSize;
            }
        }

        //store firstguess row and column to use later on
        private (int Row, int Column) StoreFirstGuess;

        //private void StoreFirstGuess(int Row, int Column)
        //{
        //    int RecordRow = Row
        //    int RecordColumn = Column;
        //}

        //methods
        public void Shuffle()
        {
            //shuffle colours of squares
            Color Temp;
            Random Rand = new Random();
            Rand = new Random();

            int RandomRows = Rand.Next(0, mRows);
            int RandomColumns = Rand.Next(0, mColumns);

            for (int i = 0; i < mRows; i++)
            {
                for (int j = 0; j < mColumns; j++)
                {
                    Temp = mGrid[i, j].HiddenColour;
                    mGrid[i, j].HiddenColour = mGrid[RandomRows, RandomColumns].HiddenColour;
                    mGrid[RandomRows, RandomColumns].HiddenColour = Temp;
                }
            }
        }
        
        
        public async Task CheckTiles(int Columns, int Rows)
        {
            //check tiles to see if there is a match with specific colours (assign points) or no match to change back the back colour
            Columns = (Columns - 240) / 30;
            Rows = (Rows - 150) / 30;


            if (mGrid[Rows, Columns].Matched == false)
            {
                if (mSecondGuess == Color.Empty || mFirstGuess == Color.Empty)
                {
                    if (mFirstGuess == Color.Empty)
                    {
                        StoreFirstGuess = (Rows, Columns);
                        mGrid[Rows, Columns].BackColour = mGrid[Rows, Columns].HiddenColour;
                        mFirstGuess = mGrid[Rows, Columns].HiddenColour;

                    }
                    else if (mFirstGuess != Color.Empty && mSecondGuess == Color.Empty)
                    {
                        mGrid[Rows, Columns].BackColour = mGrid[Rows, Columns].HiddenColour;
                        mSecondGuess = mGrid[Rows, Columns].HiddenColour;

                    }

                if (mFirstGuess == Color.Red && mSecondGuess == Color.Red)
                {
                    mGrid[Rows, Columns].Matched = true;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].Matched = true;

                    if (Player1)
                    {
                        mScoreP1 += 1;
                    }
                    else
                    {
                        mScoreP2 += 1;
                    }

                    mTotalTile += 2;

                    mGrid[Rows, Columns].BackColour = Color.Transparent;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].BackColour = Color.Transparent;

                    ResetGuess();

                }
                else if (mFirstGuess == Color.Blue && mSecondGuess == Color.Blue)
                {
                    mGrid[Rows, Columns].Matched = true;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].Matched = true;

                    if (Player1)
                    {
                        mScoreP1 += 1;
                    }
                    else 
                    {
                        mScoreP2 += 1;
                    }

                    mTotalTile += 2;

                    mGrid[Rows, Columns].BackColour = Color.Transparent;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].BackColour = Color.Transparent;

                    ResetGuess();

                }
                else if (mFirstGuess == Color.Green && mSecondGuess == Color.Green)
                {
                    mGrid[Rows, Columns].Matched = true;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].Matched = true;

                    if (Player1)
                    {
                        mScoreP1 += 1;
                    }
                    else 
                    {
                        mScoreP2 += 1;
                    }

                    mTotalTile += 2;

                    mGrid[Rows, Columns].BackColour = Color.Transparent;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].BackColour = Color.Transparent;

                    ResetGuess();
                }
                else if (mFirstGuess == Color.Maroon && mSecondGuess == Color.Maroon)
                {
                    mGrid[Rows, Columns].Matched = true;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].Matched = true;

                    if (Player1)
                    {
                        mScoreP1 += 1;
                    }
                    else 
                    {
                        mScoreP2 += 1;
                    }

                    mTotalTile += 2;

                    mGrid[Rows, Columns].BackColour = Color.Transparent;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].BackColour = Color.Transparent;

                    ResetGuess();
                }
                else if (mFirstGuess == Color.Brown && mSecondGuess == Color.Brown)
                {
                    mGrid[Rows, Columns].Matched = true;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].Matched = true;

                    if (Player1)
                    {
                        mScoreP1 += 1;
                    }
                    else 
                    {
                        mScoreP2 += 1;
                    }

                    mTotalTile += 2;

                    mGrid[Rows, Columns].BackColour = Color.Transparent;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].BackColour = Color.Transparent;

                    ResetGuess();
                }
                else if (mFirstGuess == Color.Black && mSecondGuess == Color.Black)
                {
                    mGrid[Rows, Columns].Matched = true;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].Matched = true;

                    if (Player1)
                    {
                        mScoreP1 += 1;
                    }
                    else 
                    {
                        mScoreP2 += 1;
                    }

                    mTotalTile += 2;

                    mGrid[Rows, Columns].BackColour = Color.Transparent;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].BackColour = Color.Transparent;

                    ResetGuess();
                }
                else if (mFirstGuess == Color.Yellow && mSecondGuess == Color.Yellow)
                {
                    mGrid[Rows, Columns].Matched = true;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].Matched = true;

                    if (Player1)
                    {
                        mScoreP1 += 1;
                    }
                    else 
                    {
                        mScoreP2 += 1;
                    }

                    mTotalTile += 2;

                    mGrid[Rows, Columns].BackColour = Color.Transparent;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].BackColour = Color.Transparent;

                    ResetGuess();
                }
                else if (mFirstGuess == Color.Orange && mSecondGuess == Color.Orange)
                {
                    mGrid[Rows, Columns].Matched = true;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].Matched = true;
                    
                    if (Player1)
                    {
                        mScoreP1 += 2;
                    }
                    else 
                    {
                        mScoreP2 += 2;
                    }

                    mTotalTile += 2;

                    mGrid[Rows, Columns].BackColour = Color.Transparent;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].BackColour = Color.Transparent;

                    ResetGuess();
                }
                else if (mFirstGuess == Color.Violet && mSecondGuess == Color.Violet)
                {
                    mGrid[Rows, Columns].Matched = true;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].Matched = true;
                    if (Player1)
                    {
                        mScoreP1 += 3;
                    }
                    else 
                    {
                        mScoreP2 += 3;
                    }

                    mTotalTile += 2;

                    mGrid[Rows, Columns].BackColour = Color.Transparent;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].BackColour = Color.Transparent;

                    ResetGuess();
                }
                else if (mFirstGuess == Color.Aqua && mSecondGuess == Color.Aqua)
                {
                    mGrid[Rows, Columns].Matched = true;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].Matched = true;

                    if (Player1)
                    {
                        mScoreP1 += 5;
                    }
                    else 
                    {
                        mScoreP2 += 5;
                    }

                    mTotalTile += 2;

                    mGrid[Rows, Columns].BackColour = Color.Transparent;
                    mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].BackColour = Color.Transparent;

                    ResetGuess();
                }
                else if (mFirstGuess != Color.Empty && mSecondGuess != Color.Empty)
                {
                    if (mFirstGuess != mSecondGuess)
                    {
                        mGrid[Rows, Columns].Matched = false;
                        mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].Matched = false;

                        await Task.Delay(1000);
                        mGrid[Rows, Columns].BackColour = Color.White;
                        mGrid[StoreFirstGuess.Row, StoreFirstGuess.Column].BackColour = Color.White;
                        
                        ResetGuess();

                        SwitchPlayers();
                            
                    }
                    }
                    

                }
            }

        }

        public void ResetGuess()
        {
            //reset guess after guesses
            mFirstGuess = Color.Empty;
            mSecondGuess = Color.Empty;
        }

        public void SwitchPlayers()
        {
            //switch players if there is no match 
            if (Player1 == true && Player2 == false)
            {
                Player1 = false;
                Player2 = true;

                ResetGuess();
            }
            else if (Player2 == true && Player1 == false)
            {
                Player2 = false;
                Player1 = true;

                ResetGuess();
            }
        }


    }
}
