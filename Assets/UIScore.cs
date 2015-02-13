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
		string time = "";
		switch(difficulty) {
			case Difficulty.EASY:
				time = PlayerPrefs.GetString("easyscore","99:59:59");
				break;
			case Difficulty.MEDIUM:
				time = PlayerPrefs.GetString("mediumscore","99:59:59");
				break;
			case Difficulty.HARD:
				time = PlayerPrefs.GetString("hardscore","99:59:59");
				break;
		}
		GetComponent<Text>().text = time;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
