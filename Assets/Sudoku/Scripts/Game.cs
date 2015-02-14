using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public Board board;

	public SpliceBoard splicer;
	
	public Sprite[] num;						// number textures
	public Color GoodColor;
	public Color BadColor;
	public Color LockColor;
	public Color NeutralColor;

	public GameObject gameButtons;		// game menu buttons

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
	
	public bool paused { get { return gameButtons.activeSelf; } }
	
	private bool pressedButton = false;

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

		//if(Input.GetButtonDown("Pause")) SwitchMenu();
		/*if(Input.GetButtonUp("Pause") || Input.GetButtonUp("Submit")) {
			pressedButton = false;
		}*/
	}
	
	void LateUpdate(){
		if(!solved){
			CheckSolve();											// check is puzzle solved
		}
	}
	
	// count game time
	void CountTime(){
		gameTime += 1*Time.deltaTime;		// count time
		timeFormat = "";										// clear time string
		sec = (int)(gameTime % 60.0f);			// get seconds
		min = (int)(gameTime / 60.0f);				// get minutes
		hrs = (int)(gameTime / 3600.0f);			// get hours
		
		if(hrs < 10){
			timeFormat = timeFormat + "0"+hrs.ToString()+":";		// create hours string
		}else{
			timeFormat = timeFormat +hrs.ToString()+":";
		}
		if(min < 10){
			timeFormat = timeFormat + "0"+min.ToString()+":";		// create minutes string
		}else{
			timeFormat = timeFormat +min.ToString()+":";
		}
		if(sec < 10){
			timeFormat = timeFormat + "0"+sec.ToString();				// create seconds string
		}else{
			timeFormat = timeFormat +sec.ToString();
		}
		time.text = timeFormat;		// set gui text
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
			texInstructions.SetActive(false);
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
		string lb = "";											// last best score
		string cs = time.text;				// get current score
		
		gameLevel = PlayerPrefs.GetString("gamelevel","easy");					// get play level
		
		switch(gameLevel){
			case "easy":																										// if easy
				lb = PlayerPrefs.GetString("easyscore","99:59:59");					// get score
				canSave = CompareScore(cs,lb);															// compare scores
				if(canSave){																									// if can save
					PlayerPrefs.SetString("easyscore",cs);											// save current score
				}
				break;
			case "medium":																								// if medium
				lb = PlayerPrefs.GetString("mediumscore","99:59:59");
				canSave = CompareScore(cs,lb);
				if(canSave){
					PlayerPrefs.SetString("mediumscore",cs);
				}
				break;
			case "hard":																										// if hard
				lb = PlayerPrefs.GetString("hardscore","99:59:59");
				canSave = CompareScore(cs,lb);
				if(canSave){
					PlayerPrefs.SetString("hardscore",cs);
				}
				break;
		}
	}

	void SendAnalytics() {
		string gameLevel = PlayerPrefs.GetString("gamelevel","easy");
		GA.API.Design.NewEvent("Time:" + gameLevel,gameTime);
	}
	
	// compare final score
	bool CompareScore(string ns, string os){									// new score, old score
		bool cs = false;																					// can save
		
		string nhrss = ns[0].ToString()+ns[1].ToString();					// create new hours 
		string nmins = ns[3].ToString()+ns[4].ToString();					// create new minutes
		string nsecs = ns[6].ToString()+ns[7].ToString();					// create new seconds
		
		int nhrsi = int.Parse(nhrss);															// convert all to int
		int nmini = int.Parse(nmins);
		int nseci = int.Parse(nsecs);
		
		string ohrss = os[0].ToString()+os[1].ToString();					// create old hours, minutes and seconds and convert them to int
		string omins = os[3].ToString()+os[4].ToString();
		string osecs = os[6].ToString()+os[7].ToString();
		
		int ohrsi = int.Parse(ohrss);
		int omini = int.Parse(omins);
		int oseci = int.Parse(osecs);
		
		if(nhrsi < ohrsi){						// compare hours, if new better then can save
			cs = true;								
		}
		if(nmini < omini){					// compare minutes
			cs = true;
		}
		if(nseci < oseci){						// compare seconds
			cs = true;
		}
		
		return cs;									// return can save
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
	}
}


