using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TetrisPiece{

	/* The x and y positions (indices into the 2D solution array)
	 * that this piece was from. */
	public KeyValuePair<int,int> firstSolPos;
	public KeyValuePair<int,int> secondSolPos;
	public KeyValuePair<int,int> thirdSolPos;

	/* Number values inside the piece's boxes.*/
	public int firstVal;
	public int secondVal;
	public int thirdVal;

	public bool isAnchored;

	public TetrisPiece(KeyValuePair<int,int> firstSolPos, KeyValuePair<int,int> secondSolPos,
	                   KeyValuePair<int,int> thirdSolPos, int firstVal, 
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
