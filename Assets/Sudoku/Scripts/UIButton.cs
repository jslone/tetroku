using UnityEngine;
using System.Collections;

public enum ButtonAction {
	GAME_EASY,
	GAME_MED,
	GAME_HARD,
	GAME_NEW,
	MENU_MAIN,
	MENU_GAME_NEW,
	MENU_OPTIONS,
	MENU_SCORES,
	QUIT
}

public class UIButton : MonoBehaviour {
	public ButtonAction action;
	
	public void OnClick() {
		switch(action) {
			case ButtonAction.GAME_EASY:
				PlayerPrefs.SetString("gamelevel", "easy");
				goto case ButtonAction.GAME_NEW;
			case ButtonAction.GAME_MED:
				if(PlayerPrefs.GetInt("levelUnlocked", 0) > 0) {
					PlayerPrefs.SetString("gamelevel", "medium");
					goto case ButtonAction.GAME_NEW;
				} 
				break;
			case ButtonAction.GAME_HARD:
				if(PlayerPrefs.GetInt("levelUnlocked", 0) > 1) {
					PlayerPrefs.SetString("gamelevel", "hard");
					goto case ButtonAction.GAME_NEW;
				}
				break;
			case ButtonAction.GAME_NEW:
				Application.LoadLevel(4); // game scene
				break;
			
			case ButtonAction.MENU_MAIN:
				Application.LoadLevel(0);	// menu scene
				break;
			case ButtonAction.MENU_GAME_NEW:
				if(PlayerPrefs.HasKey("played")) {
					Application.LoadLevel(1);	// new game scene
                } else {
                	PlayerPrefs.SetInt("played",1);
					Application.LoadLevel(5);	// tutorial scene
                }
				
				break;
			case ButtonAction.MENU_OPTIONS:
				Application.LoadLevel(2);	// options scene
				break;
			case ButtonAction.MENU_SCORES:
				Application.LoadLevel(3);	// scores scene
				break;
			case ButtonAction.QUIT:
				Application.Quit();
				break;
		}
	}
}
