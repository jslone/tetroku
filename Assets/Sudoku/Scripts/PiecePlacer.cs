using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PiecePlacer : MonoBehaviour {
	public Game game;
	public GameObject[] pieceMarkers;

	private static Vector3 bbl = new Vector3(-4,-4,0);
	private static Vector3 bur = new Vector3(4,4,0);

	private Vector3 bl = bbl;
	private Vector3 ur = bur;

	private TetrisPiece _piece;
	private TetrisPiece Piece {
		get {
			return _piece;
		}
		set {
			_piece = value;
			Vector2 origin = _piece.boxes[0].pos;
			Vector2 lbl = Vector2.zero;
			Vector2 lur = Vector2.zero;
			// set each child to display correctly
			for(int i = 0; i < pieceMarkers.Length; i++) {
				// set child position
				Vector2 lPos = _piece.boxes[i].pos - origin;
				pieceMarkers[i].transform.localPosition = lPos;

				// set child texture
				pieceMarkers[i].renderer.material.mainTexture = game.num[_piece.boxes[i].value];

				// expand bounding box
				lbl = Vector2.Min(lbl,lPos);
				lur = Vector2.Max(lur,lPos);
			}
			transform.position = Vector3.zero;
			// transform bounding box
			bl = bbl - (Vector3)lbl;
			ur = bur - (Vector3)lur;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// move the piece around
		Vector3 pos = transform.position;
		pos.x -= Convert.ToInt32(Input.GetKeyDown(KeyCode.LeftArrow));
		pos.x += Convert.ToInt32(Input.GetKeyDown(KeyCode.RightArrow));
		pos.y += Convert.ToInt32(Input.GetKeyDown(KeyCode.UpArrow));
		pos.y -= Convert.ToInt32(Input.GetKeyDown(KeyCode.DownArrow));

		// clamp piece inside board
		transform.position = Vector3.Max(bl,Vector3.Min(ur,pos));

		// try to place the piece
		if(Input.GetKeyDown(KeyCode.Space) && Place ()) {
			// get next piece
			transform.position = Vector3.zero;
		}
	}

	// Check if each element can be placed, if so, set board
	bool Place() {
		List<Field> fs = new List<Field>(Piece.boxes.Count);


		// check if each element can be placed
		bool anchor = true;
		foreach(Box b in Piece.boxes) {

			// look for piece at location
			RaycastHit info;
			if(Physics.Raycast(transform.position + (Vector3)b.pos,Vector3.forward,out info)) {

				// get field for piece
				Field f = info.collider.gameObject.GetComponent<Field>();
				if(f && f.canPlace) {

					// store field to modify if we can place
					fs.Add(f);

					// anchor only if every piece can be anchored
					anchor &= (b.pos - (Vector2)info.collider.gameObject.transform.position).sqrMagnitude < 0.5*0.5;

					break;
				}
			}
			return false;
		}

		// place the elements
		for(int i = 0; i < Piece.boxes.Count; i++) {
			fs[i].value = Piece.boxes[i].value;
			fs[i].canPlace = !anchor;
		}

		return true;
	}
}
