using UnityEngine;
using System;
using System.Collections;

public class PiecePlacer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// move the piece around
		Vector3 dir = Vector3.zero;
<<<<<<< HEAD
		dir.z += Convert.ToInt32(Input.GetKeyDown(KeyCode.LeftArrow));	// Left is +z
		dir.z -= Convert.ToInt32(Input.GetKeyDown(KeyCode.RightArrow));	// Right is -z
		dir.x += Convert.ToInt32(Input.GetKeyDown(KeyCode.UpArrow));	// Up is +x
		dir.x -= Convert.ToInt32(Input.GetKeyDown(KeyCode.DownArrow));	// Down is -x
=======
		dir.x -= Convert.ToInt32(Input.GetKeyDown(KeyCode.LeftArrow));
		dir.x += Convert.ToInt32(Input.GetKeyDown(KeyCode.RightArrow));
		dir.y += Convert.ToInt32(Input.GetKeyDown(KeyCode.UpArrow));
		dir.y -= Convert.ToInt32(Input.GetKeyDown(KeyCode.DownArrow));
>>>>>>> 934dbb736b525abd53d27083f27b4f2fcefb95ae

		transform.position += dir;

		// try to place the piece
		if(Input.GetKeyDown(KeyCode.Space) && Place ()) {
			// get next piece
			transform.position = Vector3.zero;
		}
	}

	// Check if each element can be placed, if so, set board
	bool Place() {
		return true;
	}
}
