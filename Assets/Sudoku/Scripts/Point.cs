using UnityEngine;

public class Point {
	public int x,y;
	
	public Point(int x, int y) {
		this.x = x;
		this.y = y;
	}
	
	public static Point Add(Point a, Point b) {
		return new Point(a.x + b.x, a.y + b.y);
	}
	
	public static Point Sub(Point a, Point b) {
		return new Point(a.x - b.x, a.y - b.y);
	}
	
	public static Point Scale(int c, Point a) {
		return new Point(c*a.x, c*a.y);
	}
	
	public static bool Equal(Point p, Point q) {
		return p.x == q.x && p.y == q.y;
	}
	
	public static implicit operator Point(Vector2 v) {
		return new Point(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
	}
	
	public static implicit operator Point(Vector3 v) {
		return new Point(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
	}
	
	public static implicit operator Vector2(Point a) {
		return new Vector2(a.x, a.y);
	}
	
	public static Point operator +(Point a, Point b) {
		return Add(a,b);
	}
	
	public static Point operator -(Point a, Point b) {
		return Sub(a,b);
	}
	
	public static Point operator *(int a, Point b) {
		return Scale (a,b);
	}
	
	public static Point operator *(Point b, int a) {
		return Scale (a,b);
	}
	
	public static bool operator ==(Point p, Point q) {
		return Equal(p,q);
	}
	
	public static bool operator !=(Point p, Point q) {
		return !Equal(p,q);
	}
}
