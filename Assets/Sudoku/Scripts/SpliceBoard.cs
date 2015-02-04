using UnityEngine;
using System.Collections;

public class SpliceBoard : MonoBehaviour {

	private const int BOARD_DIMENSION = 9;
	private char[,] puzzleArray;

	/* Takes in the puzzle which was randomly selected and splices
	   it into Tetris pieces. */
	public void splicePuzzle(string puzzle) {
		char[,] puzzleArray = new char[BOARD_DIMENSION, BOARD_DIMENSION];
		generateArray (puzzle);

		//Plan: Iterate through the board, randomly choose one of the piece
		//types, make sure it can be made out of that type at that position, splice it
		//if so, if not choose another random piece type, after a certain threshold
		//just say that piece will be an extra
		//need some wya to detect what has already been spliced 
		//maybe puzzleArray should be an array of char-bool pairs
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

			puzzleArray[row, col] = puzzle[i];

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
}
