using System;
using System.Collections;
using System.Collections.Generic;

public static class ListExtension {
	public static void Shuffle<T>(this IList<T> list) {
		int n = list.Count;
		Random rnd = new Random(Convert.ToInt32(DateTime.Now.Ticks));
		while (n > 1) {
			int k = (rnd.Next(0, n) % n);
			n--;
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}
}

public class Sudoku {
	static List<byte> range = new List<byte>() {
		1,2,3,4,5,6,7,8,9
	};
	Random rand = new Random(Convert.ToInt32(DateTime.Now.Ticks));
	byte[,] puzzle;
	
	public string GeneratePuzzle() {
		// find a solvable puzzle
		do {
			// clear the current puzzle
			Clear ();
			// randomly seed 1-9
			RandomSeed();
		} while(!Solve());
		
		// convert puzzle to string
		char[] rawPuzzle = new char[81];
		for(int i = 0; i < 9; i++) {
			for(int j = 0; j < 9; j++) {
				rawPuzzle[i*9 + j] = puzzle[i,j].ToString()[0];
			}
		}
		
		return new string(rawPuzzle);
	}
	
	void RandomSeed() {
		for(int i = 1; i <= 9; i++) {
			
			while(true) {
				int row = (int)(rand.NextDouble() * 9);
				int col = (int)(rand.NextDouble() * 9);
				
				if(puzzle[row,col] == 0) {
					puzzle[row,col] = Convert.ToByte(i);
					break;
				}
			}
		}
	}
	
	bool Solve(int row = 0, int col = 0) {
		if(puzzle[row,col] == 0) {
			List<byte> posibilites = new List<byte>(range);
			FilterRange(posibilites,row,col);
			posibilites.Shuffle();
			foreach(byte pos in posibilites) {
				puzzle[row,col] = pos;
				if(Solve (row + (col + 1)/9, (col + 1) % 9)) {
					return true;
				} else {
					puzzle[row,col] = 0;
				}
			}
			return false;
		} else {
			return Solve (row + (col + 1)/9, (col + 1) % 9);
		}
	}
	
	void FilterRange(List<byte> range, int row, int col) {
		FilterRow(range,row);
		FilterBox(range,row,col);
		FilterCol(range,col);
	}
	
	void FilterRow(List<byte> range, int row) {
		for(int i = 0; i < 9; i++) {
			range.Remove(puzzle[row,i]);
		}
	}
	
	void FilterCol(List<byte> range, int col) {
		for(int i = 0; i < 9; i++) {
			range.Remove(puzzle[i,col]);
		}
	}
	
	void FilterBox(List<byte> range, int row, int col) {
		int mRow = row / 3;
		int mCol = col / 3;
		for(int i = 0; i < 3; i++) {
			for(int j = 0; j < 3; j++) {
				range.Remove(puzzle[mRow + i, mCol + j]);
			}
		}
	}
	
	void Clear() {
		puzzle = new byte[9,9] {
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0}
		};
	}
	
	public static void Main() {
		while(true) {
			Sudoku s = new Sudoku();
			Console.WriteLine("\"" + s.GeneratePuzzle() + "\",");
		}
	}
	
}
