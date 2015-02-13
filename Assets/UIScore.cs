using UnityEngine;
using System.Collections;

public enum Difficulty {
	EASY,
	MEDIUM,
	HARD
};

public class UIScore : MonoBehaviour {
	public Difficulty difficulty;
	
	// Use this for initialization
	void Start () {
		int time;
		switch(difficulty) {
			case Difficulty.EASY:
			case Difficulty.MEDIUM:
			case Difficulty.HARD:
		}
		int time = PlayerPrefs.GetInt("")
		GetComponent<UI.Text>().text = PlayerPrefs.GetInt()
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
