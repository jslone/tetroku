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

	void Update() {
		if(value > 0) {
			bool cr = game.CheckCR(row,col,value);
			bool cb = game.CheckBox(row,col,value);
			valid = cr && cb;
			if(!valid) {
				sprite.color = Color.red;
			} else if(canPlace) {
				sprite.color = Color.white;
			} else {
				sprite.color = Color.grey;
			}
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
