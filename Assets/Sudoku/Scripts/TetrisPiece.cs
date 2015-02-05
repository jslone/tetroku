using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TetrisPiece{

	/* The x and y positions (indices into the 2D solution array)
	 * that this piece was from. */
	public Vector2 firstSolPos;
	public Vector2 secondSolPos;
	public Vector2 thirdSolPos;

	/* Number values inside the piece's boxes.*/
	public int firstVal;
	public int secondVal;
	public int thirdVal;

	public bool isAnchored;

	public TetrisPiece(Vector2 firstSolPos, Vector2 secondSolPos,
	                   Vector2 thirdSolPos, int firstVal, 
	                   int secondVal, int thirdVal) {
		this.firstSolPos = firstSolPos;
		this.secondSolPos = secondSolPos;
		this.thirdSolPos = thirdSolPos;
		this.firstVal = firstVal;
		this.secondVal = secondVal;
		this.thirdVal = thirdVal;
		isAnchored = false;
	}

}
