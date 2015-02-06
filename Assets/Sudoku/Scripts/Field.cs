﻿using UnityEngine;
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
			renderer.material.mainTexture = game.num[value];
		}
	}
	public bool valid = true;
	public bool canPlace = false;			// can number be placed on this field
	Game game;										// game script reference
	Touch[] touch;


	void Awake(){
		string temp;												// temp string
		temp = gameObject.name;					// get object name
		col = int.Parse(temp);
		temp = temp + row.ToString();			// create name
		gameObject.name = temp;					// set name
		game = GameObject.Find("Main Camera").GetComponent<Game>();		// get game reference
	}

	void Update() {
		if(value > 0) {
			valid = game.CheckCR(col,row,value) && game.CheckBox(col,row,value);
			/*if(valid) {
				renderer.material.color = Color.white;
			} else {
				renderer.material.color = Color.red;
			}*/
		}
	}
/*
#if UNITY_STANDALONE || UNITY_WEBPLAYER
	void OnMouseUp(){
		if(game.solved == false){					// puzzle not solved
			if(canPlace){										// can place number
				if(game.selected != null){			// field is selected
					game.selected.renderer.material.mainTexture = game.num[0];			// set texture
					game.selected.GetComponent<Field>().value = 0;									// set value
					game.selected = gameObject;																			// select field
				}else{
					renderer.material.mainTexture = game.num[0];										// set texture
					value = 0;																													// set value
					game.selected = gameObject;																			// select field
				}
			}
		}
	}
#endif
#if UNITY_ANDROID
	void Update(){
		touch = Input.touches;
		if(touch.Length > 0){
			if(touch[0].phase == TouchPhase.Ended){
				Ray ray = Camera.main.ScreenPointToRay (touch[0].position);								// create ray
				RaycastHit hit;																										// hit info
				
				if(Physics.Raycast (ray,out hit, 1000)) {																// cast a ray
					if(hit.collider.gameObject == gameObject){
						if(game.solved == false){																				// puzzle not solved
							if(canPlace){																								// can place number
								if(game.selected != null){																		// field is selected
									game.selected.renderer.material.mainTexture = game.num[0];			// set texture
									game.selected.GetComponent<Field>().value = 0;									// set value
									game.selected = gameObject;																			// select field
								}else{
									renderer.material.mainTexture = game.num[0];										// set texture
									value = 0;																													// set value
									game.selected = gameObject;																			// select field
								}
							}
						}
					}
				}	
			}
		}
	}
#endif
*/
}
