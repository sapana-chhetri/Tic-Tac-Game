/*
 * Programmed by:Sapana Chhetri
 * Application Name:TicTacToe Game
 * 
 * Revision history:
 *     27-sep-2023:Project: created
 *     
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SChhetriAssignment1
{
	/// <summary>
	/// Main Class of our Programme
	/// </summary>
	public partial class TicTacToe : Form
	{
		private char firstPlayer = 'X';
		private char currentPlayer;
		private String[] board = new string[9];
		Image xImage = Properties.Resource.XImage;
		Image oImage = Properties.Resource.OImage;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public TicTacToe()
		{
			InitializeComponent();
			IntializeGame();
			currentPlayer = firstPlayer;
		}
		private void IntializeGame()
		{
			for (int i = 0; i < 9; i++)
			{
				board[i] = "";
			}
		}

		private void pictureBox_Click(object sender, EventArgs e)
		{
			PictureBox pictureBox = (PictureBox)sender;
			int clickedRow = int.Parse(pictureBox.Name[pictureBox.Name.Length - 2].ToString());
			int clickedCol = int.Parse(pictureBox.Name[pictureBox.Name.Length - 1].ToString());

			if (clickedRow >= 0 && clickedCol >= 0)
			{
				if(pictureBox.Image==null)
				{
					if (currentPlayer == firstPlayer)
					{
						pictureBox.Image = xImage;
						board[clickedRow * 3 + clickedCol] = "X";
					}
					else
					{
						pictureBox.Image = oImage;
						board[clickedRow * 3 + clickedCol] = "O";
					}
					if (CheckForWin(currentPlayer))
					{
						MessageBox.Show($"{currentPlayer} wins!!");
						ResetGame();
					}
					else
					{
						if (IsBoardFull())
						{
							MessageBox.Show("Its a draw!");
							ResetGame();
						}
					}
					// Toggle to the other player's turn
					currentPlayer = (currentPlayer == 'O') ? firstPlayer : 'O';
				}
			}

		}
		/// <summary>
		///  Check for for rows, column and diagonal for win
		/// </summary>
		/// <param name="player">Who click the picturebox</param>
		/// <returns>True if the player win else false</returns>
		private bool CheckForWin(char player)
		{
			// Check rows, columns, and diagonals for a win
			for (int i = 0; i < 3; i++)
			{
				// Check rows
				if (board[i * 3] == player.ToString() && board[i * 3 + 1] == player.ToString() && board[i * 3 + 2] == player.ToString())
					return true;

				// Check columns
				if (board[i] == player.ToString() && board[i + 3] == player.ToString() && board[i + 6] == player.ToString())
					return true;
			}

			// Check diagonals
			if (board[0] == player.ToString() && board[4] == player.ToString() && board[8] == player.ToString())
				return true;
			if (board[2] == player.ToString() && board[4] == player.ToString() && board[6] == player.ToString())
				return true;

			return false;
		}
		/// <summary>
		/// To check game board is full or not
		/// </summary>
		/// <returns>True if board is full else return false</returns>
		private bool IsBoardFull()
		{
			// Check if all cells on the board are non-empty
			foreach (string cell in board)
			{
				if (string.IsNullOrEmpty(cell))
				{
					return false; // Board is not full
				}
			}
			return true; // Board is full
		}

	    /// <summary>
		/// To reset the game board and to clear the picturebox image
		/// </summary>
		private void ResetGame()
		{
			IntializeGame();

			// Reset the images on all PictureBoxes
			foreach (Control control in Controls)
			{
				if (control is PictureBox)
				{
					PictureBox pictureBox = (PictureBox)control;
					pictureBox.Image = null;
				}
			}
			// Set the current player back to O so it toggle to x before goes to event handler
			currentPlayer = 'O';
		}

	}

}
