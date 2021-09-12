using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchGame
{
    public partial class Form1 : Form
    {
        //global variables
        Grid oGrid;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (oGrid != null)
            {
                oGrid.Draw(this.CreateGraphics(), 240, 150);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lblPlayAgain.Visible == true)
                {
                    lblPlayAgain.Visible = false;
                }

                StartGame();
                lblScore1.Text = "Score: ";
                lblScore2.Text = "Score: ";

            }
            
            this.Refresh();
        }

        private void StartGame()
        {
            oGrid = new Grid(5, 4);
            lblEnter.Visible = false;
            
            tmrGame.Enabled = true;
            oGrid.Shuffle();
            oGrid.Shuffle();
            oGrid.Shuffle();
            oGrid.Shuffle();
            oGrid.Shuffle();
            oGrid.Shuffle();
            oGrid.Shuffle();
            oGrid.Shuffle();
            oGrid.Shuffle();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //store x and y value for the squares in the grid that are clicked
            if (oGrid != null)
            {

                int X = e.X;
                int Y = e.Y;

                if (X >= 240 && Y >= 150 || X <= 360 && Y <= 300)
                {
                    oGrid.CheckTiles(X, Y);
                }


                this.Refresh();
            }
        }

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            //show when the players are switched 

            if (oGrid.Player1 == false)
            {
                oGrid.Player2 = true;

                lblPlayer1.Text = "Player 1 : Player 2's Turn";
                lblPlayer2.Text = "Player 2 : Your Turn\nCLICK TO START";
                lblScore2.Text = "Score : " + oGrid.ScoreP2;

            }
            
            else if (oGrid.Player2 == false)
            {
                oGrid.Player1 = true;

                lblPlayer1.Text = "Player 1 : Your Turn.\nCLICK TO START";
                lblPlayer2.Text = "Player 2 : Player 1's Turn";
                lblScore1.Text = "Score : " + oGrid.ScoreP1;

            }

            //end game when all tiles are clicked and determine is there is a winner or a tie
            if (oGrid.TotalTile == 20)
            {
                if (oGrid.ScoreP1 > oGrid.ScoreP2)
                {
                    lblPlayer1.Text = "Player 1 : YOU WIN!!";
                    lblPlayer2.Text = "Player 2 : PLAYER 1 WINS!!";
                }
                else if (oGrid.ScoreP2 > oGrid.ScoreP1)
                {
                    lblPlayer1.Text = "Player 1 : PLAYER 2 WINS!!";
                    lblPlayer2.Text = "Player 2 : YOU WIN!!";
                }
                else
                {
                    lblPlayer1.Text = "Player 1 : IT'S A TIE!";
                    lblPlayer2.Text = "Player 2 : IT'S A TIE!";
                }


                tmrGame.Stop();
                oGrid = null;

                lblPlayAgain.Visible = true;

            }


        }

    }
}
