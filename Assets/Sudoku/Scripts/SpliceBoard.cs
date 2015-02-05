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
				int pieceType = Random.Range(0, PIECE_TYPE_COUNT);
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
		bool fourthOccupied;

		switch (pieceType) {
		case 1:
		{
			//Four in a row
			if(col > BOARD_DIMENSION - 4) {
				//The piece is too close to the edge of the board.
				return false;
			} else {
				firstOccupied = puzzleArray[row,col].Value;
				secondOccupied = puzzleArray[row,col+1].Value;
				thirdOccupied = puzzleArray[row,col+2].Value;
				fourthOccupied = puzzleArray[row,col+3].Value;
				if(firstOccupied || secondOccupied || thirdOccupied || fourthOccupied) {
					return false;
				} else {
					return true;
				}
			}
			break;
		}
		case 2:
		{
			break;
		}
		case 3:
		{
			break;
		}
		case 4:
		{
			break;
		}
		case 5:
		{
			break;
		}
		case 6:
		{
			break;
		}
		case 7:
		{
			break;
		}
		default:
			return false;
		}
	}
}
