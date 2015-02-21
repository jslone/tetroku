using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratePuzzle {
	public static readonly string testPuzzle = "999999999999999999999999999999999999999999999999999999999999999999999999999999999";
	public static string Generate() {
		SudokuModel.Sudoku sudoku = new SudokuModel.Sudoku();
		sudoku.Data = new byte[,]{
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
		
		while(!(sudoku.Generate(30).second && sudoku.Solve())) {}
		char[] rawData = new char[81];
		for(int i = 0; i < 9; i++) {
			for(int j = 0; j < 9; j++) {
				rawData[i*9 + j] = sudoku.Data[i,j].ToString()[0];
			}
		}
		
		return new string(rawData);
	}
	
	
}
