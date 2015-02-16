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
	
	private int type;
	
	public Point origin;
	private static Point[,] offsets = new Point[,] {
		{new Point(0,1), new Point (0,2)},
		{new Point(1,0), new Point (2,0)},
		{new Point(1,0), new Point (1,1)},
		{new Point(0,1), new Point (1,0)},
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
		
		boxes.Add(new Box(first,puzzle[first.x,first.y].value));
		boxes.Add(new Box(second,puzzle[second.x,second.y].value));
		boxes.Add(new Box(third,puzzle[third.x,third.y].value));
		
		puzzle[first.x,first.y].isSpliced = true;
		puzzle[second.x,second.y].isSpliced = true;
		puzzle[third.x,third.y].isSpliced = true;
	}
	
	public static bool Equal(TetrisPiece p, TetrisPiece q) {
		if(Object.Equals(p,null) || Object.Equals(q,null) || Object.Equals(p,q)) {
			return Object.Equals(p,q);
		}
		
		if(p.type != q.type) return false;
		for(int i = 0; i < p.boxes.Count; i++) {
			if(p.boxes[i].value != q.boxes[i].value) return false;
		}
		return true;
	}
	
	public static bool operator ==(TetrisPiece p, TetrisPiece q) {
		return Equal(p,q);
	}
	
	public static bool operator !=(TetrisPiece p, TetrisPiece q) {
		return !Equal(p,q);
	}
	
	public bool Fits(Point p) {
		return p == boxes[0].pos;
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
