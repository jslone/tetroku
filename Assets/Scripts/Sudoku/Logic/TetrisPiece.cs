using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Box {
	public Box(Point pos, int value) {
		this.pos = pos;
		this.value = value;
	}
	public Point pos;
	public int value;
}

public class TetrisPiece {
	public List<Box> boxes;
	public Point origin;
	private int type;
	
	private static Point[,] offsets = new Point[,] {
		{new Point(0,1), new Point (0,2)},
		{new Point(1,0), new Point (2,0)},
		{new Point(1,0), new Point (1,1)},
		{new Point(0,-1), new Point (1,-1)},
		{new Point(0,1), new Point (1,1)},
		{new Point(1,0), new Point (1,-1)}
	};
	
	private int[] values;
	private Point[] positions;
	
	public TetrisPiece() {
		boxes = new List<Box>(3);
	}
	
	public TetrisPiece(int row, int col, int type, PieceData[,] puzzle) : this() {
		
		this.type = type;
		
		Point first = new Point(row,col);
		Point second = first + offsets[type,0];
		Point third = first + offsets[type,1];
		
		this.origin = second;
		
		boxes.Add(new Box(first,puzzle[first.x,first.y].value));
		boxes.Add(new Box(second,puzzle[second.x,second.y].value));
		boxes.Add(new Box(third,puzzle[third.x,third.y].value));
		
		puzzle[first.x,first.y].isSpliced = true;
		puzzle[second.x,second.y].isSpliced = true;
		puzzle[third.x,third.y].isSpliced = true;
	}
	
	public override bool Equals(System.Object other) {
		if(other == null) {
			return false;
		}
		
		TetrisPiece p = other as TetrisPiece;
		if((System.Object)p == null) {
			return false;
		}
		
		if(type != p.type) {
			return false;
		}
		
		for(int i = 0; i < boxes.Count; i++) {
			if(boxes[i].value != p.boxes[i].value) return false;
		}
		return true;
	}
	
	public override int GetHashCode() {
		int hash = 13;
		hash = (hash * 7) + type;
		for(int i = 0; i < boxes.Count; i++) {
			hash = (hash * 7) + boxes[i].pos.GetHashCode();
			hash = (hash * 7) + boxes[i].value.GetHashCode();
		}
		return hash;
	}
	
	public static bool operator ==(TetrisPiece p, TetrisPiece q) {
		if(ReferenceEquals(p,q)) {
			return true;
		}
		
		if((object)p == null || (object)q == null) {
			return false;
		}
		
		return p.Equals(q);
	}
	
	public static bool operator !=(TetrisPiece p, TetrisPiece q) {
		return !(p == q);
	}
	
	public bool Fits(Point p) {
		return p == origin;
	}
	
	public int anchorPiece(Point pos, List<TetrisPiece> pieces) {
		for(int i = 0; i < pieces.Count; i++) {
			TetrisPiece other = pieces[i];
			if(other.Fits(pos) && this == other) {
				return i;
			}
		}
		return -1;
	}
	

}
