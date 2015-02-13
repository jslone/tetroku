using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour {
	public int col;
	public int row;									// row number
	public int _value = 0;							// field value
	public int value {
		get {
			return _value;
		}
		set {
			_value = value;
			sprite.sprite = game.num[value];
			valid = game.CheckBoard(row,col,value);
			if(!canPlace) {
				sprite.color = game.LockColor;
			} else if(!valid) {
				sprite.color = game.BadColor;
			} else {
				sprite.color = game.NeutralColor;
			}
		}
	}
	public bool valid = true;
	public bool canPlace = false;			// can number be placed on this field
	public Game game;										// game script reference
	Touch[] touch;
	SpriteRenderer sprite;
	

	void Awake(){
		sprite = GetComponent<SpriteRenderer>();
	}
}
