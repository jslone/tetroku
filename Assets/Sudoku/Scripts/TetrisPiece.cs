using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Box {
	public Box(Vector2 pos, int value) {
		this.pos = pos;
		this.value = value;
	}
	public Vector2 pos;
	public int value;
}

public class TetrisPiece {

	public List<Box> boxes;

	public bool isAnchored;

	public TetrisPiece() {
		boxes = new List<Box>(3);
		isAnchored =  false;
	}

	public TetrisPiece(Vector2 firstSolPos, Vector2 secondSolPos,
	                   Vector2 thirdSolPos, int firstVal, 
	                   int secondVal, int thirdVal) : this() {

		boxes.Add(new Box(firstSolPos,firstVal));
		boxes.Add(new Box(secondSolPos,secondVal));
		boxes.Add(new Box(thirdSolPos,thirdVal));
	}

}
