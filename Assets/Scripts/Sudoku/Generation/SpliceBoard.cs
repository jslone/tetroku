using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CharExtensions {
	public static int ParseInt32(this char value) {
		return (int)(value - '0');
	}
}

public struct PieceData {
	public PieceData(char value, bool isSpliced) {
		this.value = value.ParseInt32();
		this.isSpliced = isSpliced;
	}
	public int value;
	public bool isSpliced;
}

public class SpliceBoard {

	private const int BOARD_DIMENSION = 9;
	private const int PIECE_TYPE_COUNT = 6;

	//Preprocessing array, stores the char with the number at that place
	//in the board, as well as a bool saying whether that board square
	//has been spliced into a tetris piece yet.
	private PieceData[,] puzzleArray = new PieceData[BOARD_DIMENSION,BOARD_DIMENSION];

	//Stores all the spliced pieces for this board.
	public List<TetrisPiece> pieces;

	//Stores positions of all the extra spaces that weren't spliced into a 
	//piece, if any.
	public List<Vector2> extras;

	public SpliceBoard() {
	}

	/* Takes in the puzzle which was randomly selected and splices
	   it into Tetris pieces. */
	public void splicePuzzle(string puzzle) {
		//char[,] puzzleArray = new char[BOARD_DIMENSION, BOARD_DIMENSION];
		generateArray (puzzle);
		pieces = new List<TetrisPiece> ();
		extras = new List<Vector2> ();

		//Plan: Iterate through the board, randomly choose one of the piece
		//types, make sure it can be made out of that type at that position, splice it
		//if so, if not choose another random piece type, after a certain threshold
		//just say that piece will be an extra


		Debug.Log("started splicing");

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
				int pieceType = 0;
				bool canSplice = false;

				int counter = 0;
				do {
					if(counter > 1000) {
						break;
					}
					pieceType = Random.Range (0, PIECE_TYPE_COUNT);
					canSplice = checkValidSplice(pieceType, i, j);
					//might be case with infinite loop, if a space
					//on board is boxedd in somehow, not surre if this
					//can happen, if so fix it

					//add a mechanism so that it checks if loop has
					//been running for too many iterations, if so
					//just make this tile an extra and move on
					counter++;
				} while(canSplice == false);

				if(canSplice == false) {
					//extras.Add(new Vector2(i,j));
				} else {
					TetrisPiece newPiece = splice (pieceType, i, j);
					pieces.Add (newPiece);
				}
			}
		}

		Debug.Log("finished splicing");

		/* Loop through and find all the pieces that couldn't be spliced. */
		for(int i = 0; i < BOARD_DIMENSION; i++) {
			for(int j = 0; j < BOARD_DIMENSION; j++) {
				if(!puzzleArray[i,j].isSpliced) {
					extras.Add (new Vector2(i,j));
				}
			}
		}

		Debug.Log (pieces.Count);
		Debug.Log (extras.Count);
	}

	//Generates a 2-dimensional array given a puzzle string.
	private void generateArray(string puzzle) {

		/* Important: Blocks of 9 chars in the string correspond to
		   COLUMNS of the Sudoku board. Code will be less efficient
		   since updating non-continous blocks of memory but for
		   our purposes it should be fine. I can change it if needed
		   but this way it makes more sense conceptually with the board.
		 */

		int i = 0;
		for(int row = 0; row < BOARD_DIMENSION; row++) {
			for(int col = 0; col < BOARD_DIMENSION; col++) {
				puzzleArray[row, col] = new PieceData(puzzle[i], false);
				i++;
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
				firstOccupied = puzzleArray[row,col].isSpliced;
				secondOccupied = puzzleArray[row,col+1].isSpliced;
				thirdOccupied = puzzleArray[row,col+2].isSpliced;
				if(firstOccupied || secondOccupied || thirdOccupied) {
					return false;
				} else {
					return true;
				}
			}
		}
		case 1:
		{
			//Three in vertical row
			if(row > BOARD_DIMENSION - 3) {
				//The piece is too close to the edge of the board.
				return false;
			} else {
				firstOccupied = puzzleArray[row,col].isSpliced;
				secondOccupied = puzzleArray[row+1,col].isSpliced;
				thirdOccupied = puzzleArray[row+2,col].isSpliced;
				if(firstOccupied || secondOccupied || thirdOccupied) {
					return false;
				} else {
					return true;
				}
			}
		}
		case 2:
		{
			//One top left, two bottom
			if(row > BOARD_DIMENSION - 2 || col > BOARD_DIMENSION - 2) {
				//The piece is too close to the edge of the board.
				return false;
			} else {
				firstOccupied = puzzleArray[row,col].isSpliced;
				secondOccupied = puzzleArray[row+1,col].isSpliced;
				thirdOccupied = puzzleArray[row+1,col+1].isSpliced;
				if(firstOccupied || secondOccupied || thirdOccupied) {
					return false;
				} else {
					return true;
				}
			}
		}
		case 3:
		{
			//Two top, one bottom left
			if(row > BOARD_DIMENSION - 2 || col < 1) {
				//The piece is too close to the edge of the board.
				return false;
			} else {
				firstOccupied = puzzleArray[row,col].isSpliced;
				secondOccupied = puzzleArray[row,col-1].isSpliced;
				thirdOccupied = puzzleArray[row+1, col-1].isSpliced;
				if(firstOccupied || secondOccupied || thirdOccupied) {
					return false;
				} else {
					return true;
				}
			}
		}
		case 4:
		{
			//Two top, one bottom right
			if(row > BOARD_DIMENSION - 2 || col > BOARD_DIMENSION - 2) {
				//The piece is too close to the edge of the board.
				return false;
			} else {
				firstOccupied = puzzleArray[row,col].isSpliced;
				secondOccupied = puzzleArray[row,col+1].isSpliced;
				thirdOccupied = puzzleArray[row+1, col+1].isSpliced;
				if(firstOccupied || secondOccupied || thirdOccupied) {
					return false;
				} else {
					return true;
				}
			}
		}
		case 5:
		{
			//One top right, two bottom
			if(row > BOARD_DIMENSION - 2 || col == 0) {
				//The piece is too close to the edge of the board.
				return false;
			} else {
				firstOccupied = puzzleArray[row,col].isSpliced;
				secondOccupied = puzzleArray[row+1,col].isSpliced;
				thirdOccupied = puzzleArray[row+1, col-1].isSpliced;
				if(firstOccupied || secondOccupied || thirdOccupied) {
					return false;
				} else {
					return true;
				}
			}
		}
		default:
			return false;
		}
	}

	/* Splice the piece out of the board, returning the piece. */
	TetrisPiece splice(int pieceType, int row, int col) {
		return new TetrisPiece(row,col,pieceType,puzzleArray);	
	}
}
