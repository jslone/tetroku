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
	private static Matrix4x4 transform = Matrix4x4.identity;

	public List<Box> boxes;

	public bool isAnchored;

	public TetrisPiece() {
		boxes = new List<Box>(3);
		isAnchored =  false;
		// transform from board to world coords
		transform.SetRow(0,new Vector4( 1, 0, 0,-4));
		transform.SetRow(1,new Vector4( 0,-1, 0, 4));
		transform.SetRow(2,new Vector4( 0, 0, 1, 0));
		transform.SetRow(3,new Vector4( 0, 0, 0, 1));

	}

	public TetrisPiece(Vector2 firstSolPos, Vector2 secondSolPos,
	                   Vector2 thirdSolPos, int firstVal, 
	                   int secondVal, int thirdVal) : this() {
		boxes.Add(new Box(transform.MultiplyPoint3x4(firstSolPos),firstVal));
		boxes.Add(new Box(transform.MultiplyPoint3x4(secondSolPos),secondVal));
		boxes.Add(new Box(transform.MultiplyPoint3x4(thirdSolPos),thirdVal));
	}

}
