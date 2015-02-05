using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpliceBoard : MonoBehaviour {

	private const int BOARD_DIMENSION = 9;
	private const int PIECE_TYPE_COUNT = 7;

	//Preprocessing array, stores the char with the number at that place
	//in the board, as well as a bool saying whether that board square
	//has been spliced into a tetris piece yet.
	private KeyValuePair<char, bool>[,] puzzleArray;

	/* Takes in the puzzle which was randomly selected and splices
	   it into Tetris pieces. */
	public void splicePuzzle(string puzzle) {
		char[,] puzzleArray = new char[BOARD_DIMENSION, BOARD_DIMENSION];
		generateArray (puzzle);

		//Plan: Iterate through the board, randomly choose one of the piece
		//types, make sure it can be made out of that type at that position, splice it
		//if so, if not choose another random piece type, after a certain threshold
		//just say that piece will be an extra
		//need some way to detect what has already been spliced 
		//maybe puzzleArray should be an array of char-bool pairs

		for (int i = 0; i < BOARD_DIMENSION; i++) {
			for(int j = 0; j < BOARD_DIMENSION; j++) {
				/* Choose one of the seven tetris piece types randomly. These are:
				 * 1 - Four in a line
				 * 2 - Three bottom with one on top left
				 * 3 - Three bottom with one on top right
				 * 4 - Three bottom with one on top center
				 * 5 - Four box
				 * 6 - Two on top, two on bottom, skewed left
				 * 7 - Two on top, two on bottom, skewed rigt
				 */

				/* Changing to tetris pieces of size 3 instead
				 * but keeping that list up there just in case we switch
				 * to 4. However, given piece rotation there will be 6 types
				 * though there are only 2 real shapes
				 */
				int pieceType = Random.Range(0, 6);
				bool canSplice = checkValidSplice(pieceType, i, j);
			}
		}



	}

	//Generates a 2-dimensional array given a puzzle string.
	private void generateArray(string puzzle) {

		/* Important: Blocks of 9 chars in the string correspond to
		   COLUMNS of the Sudoku board. Code will be less efficient
		   since updating non-continous blocks of memory but for
		   our purposes it should be fine. I can change it if needed
		   but this way it makes more sense conceptually with the board.
		 */
		int row = 0;
		int col = 0;

		for(int i = 0; i < puzzle.Length; i++) {

			puzzleArray[row, col] = new KeyValuePair<char, bool>(puzzle[i], false);

			if(row > BOARD_DIMENSION) {
				//If we've reached the end of a col, go to the next one.
				row = 0;
				col++;
			} else {
				//We haven't reached the end of the col, so go to the
				//next row.
				row++;
			}
		}
	}

	//Check if a certain tetris piece can be spliced at a particular location
	private bool checkValidSplice(int pieceType, int row, int col) {
		bool firstOccupied;
		bool secondOccupied;
		bool thirdOccupied;

		switch (pieceType) {
		case 0:
		{
			//Three in horizontal row
			if(col > BOARD_DIMENSION - 3) {
				//The piece is too close to the edge of the board.
				return false;
			} else {
				firstOccupied = puzzleArray[row,col].Value;
				secondOccupied = puzzleArray[row,col+1].Value;
				thirdOccupied = puzzleArray[row,col+2].Value;
				if(firstOccupied || secondOccupied || thirdOccupied) {
					return false;
				} else {
					return true;
				}
			}
			break;
		}
		case 1:
		{
			//Three in vertical row
			if(row > BOARD_DIMENSION - 3) {
				//The piece is too close to the edge of the board.
				return false;
			} else {
				firstOccupied = puzzleArray[row,col].Value;
				secondOccupied = puzzleArray[row+1,col].Value;
				thirdOccupied = puzzleArray[row+1,col].Value;
				if(firstOccupied || secondOccupied || thirdOccupied) {
					return false;
				} else {
					return true;
				}
			}
			break;
		}
		case 2:
		{
			//One top left, two bottom
			if(row > BOARD_DIMENSION - 2 || col > BOARD_DIMENSION - 2) {
				//The piece is too close to the edge of the board.
				return false;
			} else {
				firstOccupied = puzzleArray[row,col].Value;
				secondOccupied = puzzleArray[row+1,col].Value;
				thirdOccupied = puzzleArray[row+1,col+1].Value;
				if(firstOccupied || secondOccupied || thirdOccupied) {
					return false;
				} else {
					return true;
				}
			}
			break;
		}
		case 3:
		{
			//Two top, one bottom left
			if(row > BOARD_DIMENSION - 2 || col > BOARD_DIMENSION - 2) {
				//The piece is too close to the edge of the board.
				return false;
			} else {
				firstOccupied = puzzleArray[row,col].Value;
				secondOccupied = puzzleArray[row,col+1].Value;
				thirdOccupied = puzzleArray[row+1, col].Value;
				if(firstOccupied || secondOccupied || thirdOccupied) {
					return false;
				} else {
					return true;
				}
			}
			break;
		}
		case 4:
		{
			//Two top, one bottom right
			if(row > BOARD_DIMENSION - 2 || col > BOARD_DIMENSION - 2) {
				//The piece is too close to the edge of the board.
				return false;
			} else {
				firstOccupied = puzzleArray[row,col].Value;
				secondOccupied = puzzleArray[row,col+1].Value;
				thirdOccupied = puzzleArray[row+1, col+1].Value;
				if(firstOccupied || secondOccupied || thirdOccupied) {
					return false;
				} else {
					return true;
				}
			}
			break;
		}
		case 5:
		{
			//One top right, two bottom
			if(row > BOARD_DIMENSION - 2 || col == 0) {
				//The piece is too close to the edge of the board.
				return false;
			} else {
				firstOccupied = puzzleArray[row,col].Value;
				secondOccupied = puzzleArray[row+1,col].Value;
				thirdOccupied = puzzleArray[row+1, col-1].Value;
				if(firstOccupied || secondOccupied || thirdOccupied) {
					return false;
				} else {
					return true;
				}
			}
			break;
		}
		default:
			return false;
		}
	}
}
