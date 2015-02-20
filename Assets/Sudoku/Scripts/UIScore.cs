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
		float time = float.PositiveInfinity;
		switch(difficulty) {
			case Difficulty.EASY:
				time = PlayerPrefs.GetFloat("easyscore",float.PositiveInfinity);
				break;
			case Difficulty.MEDIUM:
				time = PlayerPrefs.GetFloat("mediumscore",float.PositiveInfinity);
				break;
			case Difficulty.HARD:
				time = PlayerPrefs.GetFloat("hardscore",float.PositiveInfinity);
				break;
		}
		if(time > 90000) {
			GetComponent<Text>().text = "-";
		} else {
			GetComponent<Text>().text = TimeScore.toString(time);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
