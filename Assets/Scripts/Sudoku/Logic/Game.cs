using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public Board board;

	public SpliceBoard splicer;
	
	public Sprite[] num;
	public Sprite[] numWrong;
	public Color GoodColor;
	public Color BadColor;
	public Color LockColor;
	public Color NeutralColor;

	public GameObject gameButtons;
	public StandAloneInputModuleClearable input;
	public Text time;

	float gameTime = 0.0f;
	bool countTime = false;
	public bool solved = false;
	
	int[,] code = new int[9,9];
	
	public GameObject texSolved;
	public GameObject texInstructions;
	
	public Button pauseButton;
	public bool paused { get { return gameButtons.activeSelf; } }

	void Start (){
		texSolved.SetActive(false);
		LoadPuzzle();
		countTime = true;
	}
	
	void Update(){
		if(countTime){
			CountTime();
		}
	}
	
	void LateUpdate(){
		if(!solved){
			CheckSolve();
		}
	}
	
	// increment timer
	void CountTime(){
		gameTime += Time.deltaTime;
		time.text = TimeScore.toString(gameTime);
	}
	
	// check if puzzle is solved
	void CheckSolve(){
		int filled = 0;
		Field[] f = FindObjectsOfType(typeof(Field)) as Field[];
		foreach(Field fl in f) {
			if(!fl.canPlace){
				filled++;
			}
		}
		
		if(filled == 81){
			countTime = false;
			solved = true;
			time.gameObject.SetActive(false);
			texInstructions.SetActive(false);
			pauseButton.gameObject.SetActive(false);
			texSolved.SetActive(true);
			SaveScore();
			SwitchMenu();
			SendAnalytics();
		}
	}
	
	// update high score
	void SaveScore(){
		string gameLevel = "";
		bool canSave = false;
		float lb;
		float cs = gameTime;
		
		gameLevel = PlayerPrefs.GetString("gamelevel","easy");
		
		switch(gameLevel){
			case "easy":
				lb = PlayerPrefs.GetFloat("easyscore",float.PositiveInfinity);
				canSave = cs < lb;
				if(canSave){
					PlayerPrefs.SetFloat("easyscore",cs);
				}
				if(PlayerPrefs.GetInt("levelUnlocked", 0) == 0) {
					PlayerPrefs.SetInt("levelUnlocked", 1);
				}
				break;
			case "medium":
				lb = PlayerPrefs.GetFloat("mediumscore",float.PositiveInfinity);
				canSave = cs < lb;
				if(canSave){
					PlayerPrefs.SetFloat("mediumscore",cs);
				}
				if(PlayerPrefs.GetInt("levelUnlocked", 0) == 1) {
					PlayerPrefs.SetInt("levelUnlocked", 2);
				}
				break;
			case "hard":
				lb = PlayerPrefs.GetFloat("hardscore",float.PositiveInfinity);
				canSave = cs < lb;
				if(canSave){
					PlayerPrefs.SetFloat("hardscore",cs);
				}
				break;
		}
	}

	void SendAnalytics() {
		string gameLevel = PlayerPrefs.GetString("gamelevel","easy");
		GA.API.Design.NewEvent("Time:" + gameLevel, gameTime);
	}
	
	// Generate a puzzle and splice it into tetris pieces
	void LoadPuzzle(){
	
		string puzzle = GeneratePuzzle.Generate();
		splicer = new SpliceBoard();
		splicer.splicePuzzle(puzzle);
		
		int i = 0;
		for(int x = 0; x < 9; x++){
			for(int y = 0; y < 9; y++){
				code[x,y] = puzzle[i].ParseInt32();
				i++;
			}
		}

		foreach(Vector2 extra in splicer.extras) {
			int x = Mathf.RoundToInt(extra.x);
			int y = Mathf.RoundToInt(extra.y);
			
			Field f = board.fields[x,y];
			f.canPlace = false;
			f.SetValue(code[x,y]);
		}

		int numPieces;
		string gameLevel = PlayerPrefs.GetString("gamelevel","easy");
		switch(gameLevel) {
			case "easy":
				numPieces = Random.Range(12,16);
				break;
			case "medium":
				numPieces = Random.Range(8,12);
				break;
			default:
				numPieces = Random.Range(4,8);
				break;
		}

		for(int j = 0; j < numPieces; j++) {
			int index = Random.Range(0,splicer.pieces.Count);
			TetrisPiece t = splicer.pieces[index];
			foreach(Box b in t.boxes) {
				int x = Mathf.RoundToInt(b.pos.x);
				int y = Mathf.RoundToInt(b.pos.y);

				Field f = board.fields[x,y];
				f.canPlace = false;
				f.SetValue(b.value);
			}
			splicer.pieces.RemoveAt(index);
		}
	}
	
	// check if the solution board has value at position row,col
	public bool CheckBoard(int row, int col, int value) {
		return code[row,col] == value;
	}
	
	// toggle menu
	public void SwitchMenu(){
		gameButtons.SetActive(!gameButtons.activeSelf);		// show game menu
		if(!gameButtons.activeSelf) input.Clear();
	}
}


