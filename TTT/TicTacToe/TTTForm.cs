using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class TTTForm : Form
    {
        public TTTForm()
        {
            InitializeComponent();
        }

        const string USER_SYMBOL = "X";
        const string COMPUTER_SYMBOL = "O";
        const string EMPTY = "";

        const int SIZE = 5;

        // constants for the 2 diagonals
        const int TOP_LEFT_TO_BOTTOM_RIGHT = 1;
        const int TOP_RIGHT_TO_BOTTOM_LEFT = 2;

        // constants for IsWinner
        const int NONE = -1;
        const int ROW = 1;
        const int COLUMN = 2;
        const int DIAGONAL = 3;

        // Rectangular array to hold Xs and Os
        private string[,] board = new string[SIZE, SIZE];

        // This method takes a row and column as parameters and 
        // returns a reference to a label on the form in that position
        private Label GetSquare(int row, int column)
        {
            int labelNumber = row * SIZE + column + 1;
            return (Label)(this.Controls["label" + labelNumber.ToString()]);
        }

        // This method does the "reverse" process of GetSquare
        // It takes a label on the form as it's parameter and
        // returns the row and column of that square as output parameters
        private void GetRowAndColumn(Label l, out int row, out int column)
        {
            int position = int.Parse(l.Name.Substring(5));
            row = (position - 1) / SIZE;
            column = (position - 1) % SIZE;
        }

        //* TODO: Modify this so it uses the array rather than a square in the UI
        // This method takes a row (in the range of 0 - 4) and returns true if 
        // the row on the form contains 5 Xs or 5 Os.
        // Use it as a model for writing IsColumnWinner
        private bool IsRowWinner(int row)
        {
            string symbol = board[row, 0]; // get the symbol from the first column 
            for (int col = 1; col < SIZE; col++) // compare symbol to all other column symbols
            {
                if (symbol == EMPTY || board[row, col] != symbol) // if the symbol is EMPTY or different
                {
                    return false; // the row is not a winner
                }
            }
            return true; // the row is a winner
            /*
            Label square = GetSquare(row, 0);
            string symbol = square.Text;
            for (int col = 1; col < SIZE; col++)
            {
                square = GetSquare(row, col);
                if (symbol == EMPTY || square.Text != symbol)
                    return false;
            }
            return true;
            */
        }

        //* TODO:  finish all of these that return true
        private bool IsAnyRowWinner()
        {
            return true;
        }

        private bool IsColumnWinner(int col)
        {
            return true;
        }

        private bool IsAnyColumnWinner()
        {
            return true;
        }

        private bool IsDiagonal1Winner()
        {
            return true;
        }

        private bool IsDiagonal2Winner()
        {
            Label square = GetSquare(0, (SIZE - 1));
            string symbol = square.Text;
            for (int row = 1, col = SIZE - 2; row < SIZE; row++, col--)
            {
                square = GetSquare(row, col);
                if (symbol == EMPTY || square.Text != symbol)
                    return false;
            }
            return true;
        }

        private bool IsAnyDiagonalWinner()
        {
            return true;
        }

        private bool IsFull()
        {
            return true;
        }

        // This method determines if any row, column or diagonal on the board is a winner.
        // It returns true or false and the output parameters will contain appropriate values
        // when the method returns true.  See constant definitions at top of form.
        private bool IsWinner(out int whichDimension, out int whichOne)
        {
            // rows
            for (int row = 0; row < SIZE; row++)
            {
                if (IsRowWinner(row))
                {
                    whichDimension = ROW;
                    whichOne = row;
                    return true;
                }
            }
            // columns
            for (int column = 0; column < SIZE; column++)
            {
                if (IsColumnWinner(column))
                {
                    whichDimension = COLUMN;
                    whichOne = column;
                    return true;
                }
            }
            // diagonals
            if (IsDiagonal1Winner())
            {
                whichDimension = DIAGONAL;
                whichOne = TOP_LEFT_TO_BOTTOM_RIGHT;
                return true;
            }
            if (IsDiagonal2Winner())
            {
                whichDimension = DIAGONAL;
                whichOne = TOP_RIGHT_TO_BOTTOM_LEFT;
                return true;
            }
            whichDimension = NONE;
            whichOne = NONE;
            return false;
        }

        // I wrote this method to show you how to call IsWinner
        private bool IsTie()
        {
            int winningDimension, winningValue;
            return (IsFull() && !IsWinner(out winningDimension, out winningValue));
        }

        //* TODO:  finish this
        private void ResetArray()
        {
        }

        //* TODO:  Modify this so it uses the array rather than the UI to make the move.
        // Setting the text and disabling the square will happen in the SyncArrayAndSquares method
        private void MakeComputerMove()
        {
            Random gen = new Random();
            int row;
            int column;
            //Label square; 
            do
            {
                row = gen.Next(0, SIZE);
                column = gen.Next(0, SIZE);
                //square = GetSquare(row, column);
            } while (board[row, column] != EMPTY);
            board[row, column] = COMPUTER_SYMBOL;
            //DisableSquare(square);
        }

        // ALL OF THESE METHODS MANIPULATE THE UI AND SHOULDN'T CHANGE
        // This method takes an integer in the range 0 - 2 that represents a column
        // as it's parameter and changes the font color of that cell to red.
        private void HighlightColumn(int col)
        {
            for (int row = 0; row < SIZE; row++)
            {
                Label square = GetSquare(row, col);
                square.Enabled = true;
                square.ForeColor = Color.Red;
            }
        }

        private void HighlightDiagonal(int whichDiagonal)
        {
            if (whichDiagonal == TOP_LEFT_TO_BOTTOM_RIGHT)
                HighlightDiagonal1();
            else
                HighlightDiagonal2();

        }

        private void HighlightDiagonal2()
        {
            for (int row = 0, col = SIZE - 1; row < SIZE; row++, col--)
            {
                Label square = GetSquare(row, col);
                square.Enabled = true;
                square.ForeColor = Color.Red;
            }
        }

        private void HighlightRow(int row)
        {
            for (int col = 0; col < SIZE; col++)
            {
                Label square = GetSquare(row, col);
                square.Enabled = true;
                square.ForeColor = Color.Red;
            }
        }

        private void HighlightDiagonal1()
        {
            for (int row = 0, col = 0; row < SIZE; row++, col++)
            {
                Label square = GetSquare(row, col);
                square.Enabled = true;
                square.ForeColor = Color.Red;
            }
        }

        private void HighlightWinner(string player, int winningDimension, int winningValue)
        {
            switch (winningDimension)
            {
                case ROW:
                    HighlightRow(winningValue);
                    resultLabel.Text = (player + " wins!");
                    break;
                case COLUMN:
                    HighlightColumn(winningValue);
                    resultLabel.Text = (player + " wins!");
                    break;
                case DIAGONAL:
                    HighlightDiagonal(winningValue);
                    resultLabel.Text = (player + " wins!");
                    break;
            }
        }

        private void ResetSquares()
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    Label square = GetSquare(row, col);
                    square.Text = EMPTY;
                    square.ForeColor = Color.Black;
                }
            }
        }

        // Setting the enabled property changes the look and feel of the cell.
        // Instead, this code removes the event handler from each square.
        // Use it when someone wins or the board is full to prevent clicking a square.
        private void DisableAllSquares()
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    Label square = GetSquare(row, col);
                    DisableSquare(square);
                }
            }
        }

        // Inside the click event handler you have a reference to the label that was clicked
        // Use this method (and pass that label as a parameter) to disable just that one square
        private void DisableSquare(Label square)
        {
            square.Click -= new System.EventHandler(this.label_Click);
        }

        // You'll need this method to allow the user to start a new game
        private void EnableAllSquares()
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    Label square = GetSquare(row, col);
                    square.Click += new System.EventHandler(this.label_Click);
                }
            }
        }

        //* TODO:  Finish this method
        // It should set the text property of each square in the UI to the value in the corresponding element of the array
        // and disable the squares that are not empty (you don't have to enable the others because they're enabled by default.
        private void SyncArrayAndSquares()
        {
            for(int row = 0; row < SIZE; row++)
            {
                for(int col = 0; col < SIZE; col++)
                {
                    Label square = GetSquare(row, col);
                    square.Text = board[row, col];
                    if(square.Text != EMPTY)
                    {
                        DisableSquare(square);
                    }
                }
            }
        }

        //* TODO:  modify this so that it uses the array and UI methods appropriately
        private void label_Click(object sender, EventArgs e)
        {
            int winningDimension = NONE;
            int winningValue = NONE;

            //Label clickedLabel = (Label)sender;
            //clickedLabel.Text = USER_SYMBOL;
            //DisableSquare(clickedLabel);

            int row, column;
            Label clickedLabel = (Label)sender;
            GetRowAndColumn(clickedLabel, out row, out column);
            board[row, column] = USER_SYMBOL;
            SyncArrayAndSquares();

            if (IsWinner(out winningDimension, out winningValue))
            {
                HighlightWinner("The User", winningDimension, winningValue);
                //DisableAllSquares();
            }
            else if (IsFull())
            {
                resultLabel.Text = "It's a Tie.";
                //DisableAllSquares();
            }
            else
            {
                MakeComputerMove();
                SyncArrayAndSquares();
                if (IsWinner(out winningDimension, out winningValue))
                {
                    HighlightWinner("The Computer", winningDimension, winningValue);
                    //DisableAllSquares();
                }
                else if (IsFull())
                {
                    resultLabel.Text = "It's a Tie.";
                    //DisableAllSquares();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetArray();
            ResetSquares();
            EnableAllSquares();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
