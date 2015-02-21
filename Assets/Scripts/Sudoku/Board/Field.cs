using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour {
	public int col;
	public int row;
	public int value = 0;
	public bool valid = true;
	public bool canPlace = false;
	public Game game;
	SpriteRenderer sprite;
	

	void Awake(){
		sprite = GetComponent<SpriteRenderer>();
	}
	
	public bool SetValue(int _value) {
		value = _value;
		
		sprite.sprite = game.num[value];
		valid = game.CheckBoard(row,col,value);
		if(!canPlace) {
			sprite.color = game.LockColor;
		} else if(!valid) {
			sprite.sprite = game.numWrong[value];
			return false;
		} else {
			sprite.color = game.NeutralColor;
		}
		return true;
	}
}
