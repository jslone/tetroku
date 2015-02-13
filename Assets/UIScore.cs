using UnityEngine;
using UnityEngine.UI;
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
				break;
			case Difficulty.MEDIUM:
				break;
			case Difficulty.HARD:
				break;
		}
		time = PlayerPrefs.GetInt("");
		GetComponent<Text>().text = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
