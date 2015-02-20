using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public Board board;

	public SpliceBoard splicer;
	
	public Sprite[] num;						// number textures
	public Sprite[] numWrong;
	public Color GoodColor;
	public Color BadColor;
	public Color LockColor;
	public Color NeutralColor;

	public GameObject gameButtons;		// game menu buttons
	public StandAloneInputModuleClearable input;
	public Text time;

	float gameTime = 0.0f;								// game play time
	int hrs = 0;														// hours
	int min = 0;														// minutes
	int sec = 0;														// seconds
	string timeFormat = "";								// play time as string for gui text
	bool countTime = false;								// count game time
	public bool solved = false;							// is puzzle solved
	
	int[,] code = new int[9,9];						// solved puzzle
	
	public GameObject texSolved;				// solved gui texture
	public GameObject texInstructions;
	
	public GameObject gen;							// generating puzzle gui texture
	public Button pauseButton;
	public bool paused { get { return gameButtons.activeSelf; } }

	void Start (){
		gen.SetActive(false);								// disable some objects
		texSolved.SetActive(false);
		LoadPuzzle();
		countTime = true;										// start counting time
	}
	
	void Update(){
		if(countTime){
			CountTime();																					// count time
		}
	}
	
	void LateUpdate(){
		if(!solved){
			CheckSolve();											// check is puzzle solved
		}
	}
	
	// count game time
	void CountTime(){
		gameTime += Time.deltaTime;		// count time
		
		time.text = TimeScore.toString(gameTime);		// set gui text
	}
	
	// check if puzzle is solved
	void CheckSolve(){
		int filled = 0;										// number of filled fields
		Field[] f = FindObjectsOfType(typeof(Field)) as Field[];			// find all fields
		foreach(Field fl in f) {	
			if(fl.value != 0 && fl.valid){			// check if not empty
				filled++;						// add filled
			}
		}
		
		if(filled == 81){										// if all filled
			countTime = false;							// stop counting
			solved = true;										// puzzle is solved
			time.gameObject.SetActive(false);
			texInstructions.SetActive(false);
			pauseButton.gameObject.SetActive(false);
			texSolved.SetActive(true);				// show gui texture
			SaveScore();										// save current score
			SwitchMenu();									// change menu
			SendAnalytics();
		}
	}
	
	// save game score if best
	void SaveScore(){
		string gameLevel = "";							// play level
		bool canSave = false;							// can score be saved
		float lb;											// last best score
		float cs = gameTime;				// get current score
		
		gameLevel = PlayerPrefs.GetString("gamelevel","easy");					// get play level
		
		switch(gameLevel){
			case "easy":																										// if easy
				lb = PlayerPrefs.GetFloat("easyscore",float.PositiveInfinity);					// get score
				canSave = cs < lb;															// compare scores
				if(canSave){																									// if can save
					PlayerPrefs.SetFloat("easyscore",cs);											// save current score
				}
				if(PlayerPrefs.GetInt("levelUnlocked", 0) == 0) {
					PlayerPrefs.SetInt("levelUnlocked", 1);
				}
				break;
			case "medium":																								// if medium
				lb = PlayerPrefs.GetFloat("mediumscore",float.PositiveInfinity);
				canSave = cs < lb;
				if(canSave){
					PlayerPrefs.SetFloat("mediumscore",cs);
				}
				if(PlayerPrefs.GetInt("levelUnlocked", 0) == 1) {
					PlayerPrefs.SetInt("levelUnlocked", 2);
				}
				break;
			case "hard":																										// if hard
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
	
	// load prefab puzzle
	void LoadPuzzle(){
		int i = 0;								// index number
		int no = 0;							// field number
		
		string puzzle = "";																				// puzzle
		
		puzzle = GameObject.FindWithTag("database").GetComponent<PuzzleDatabase>().SelectPuzzle();				// get puzzle
		splicer = new SpliceBoard();
		splicer.splicePuzzle(puzzle);

		for(int x = 0; x < 9; x++){
			for(int y = 0; y < 9; y++){
				no = puzzle[i].ParseInt32();
				code[x,y] = no;							// save field value
				i++;
			}
		}

		foreach(Vector2 extra in splicer.extras) {
			int x = Mathf.RoundToInt(extra.x);
			int y = Mathf.RoundToInt(extra.y);
			no = code[x,y];


			Field f = board.fields[x,y];
			f.canPlace = false;
			f.SetValue(no);
		}

		int noPieces;
		string gameLevel = PlayerPrefs.GetString("gamelevel","easy");
		switch(gameLevel) {
			case "easy":
				noPieces = Random.Range(12,16);
				break;
			case "medium":
				noPieces = Random.Range(8,12);
				break;
			default:
				noPieces = Random.Range(4,8);
				break;
		}

		for(int j = 0; j < noPieces; j++) {
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
	
	public bool CheckBoard(int row, int col, int value) {
		return code[row,col] == value;
	} 
	
	// check column and row
	public bool CheckCR(int x, int y, int val){				// first number, second number, value
		bool cp = true;												// can place
		for(int i = 0; i < 9; i++) {
			cp &= i==y || board.fields[x,i].value != val;
			cp &= i==x || board.fields[i,y].value != val;
		}
		return cp;															// return can place
	}
	
	// check for same numbers in box
	public bool CheckBox(int x, int y, int val){				//
		bool cp = true;
		int sx = (x/3)*3;
		int sy = (y/3)*3;
		for(int i = 0; i < 3; i++) {
			for(int j = 0; j < 3; j++) {
				int ii = sx+i;
				int jj = sy+j;
				cp &= (x==ii && y==jj) || board.fields[ii,jj].value != val;
			}
		}
		return cp;
	}
	
	// change menu
	public void SwitchMenu(){
		gameButtons.SetActive(!gameButtons.activeSelf);		// show game menu
		if(!gameButtons.activeSelf) input.Clear();
	}
}


