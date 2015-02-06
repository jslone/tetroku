using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PiecePlacer : MonoBehaviour {
	public Game game;
	public GameObject[] pieceMarkers;
	public Color markerColor;

	private static Vector3 bbl = new Vector3(-4,-4,0);
	private static Vector3 bur = new Vector3(4,4,0);

	public Vector3 bl = bbl;
	public Vector3 ur = bur;

	private int PieceIdx = 0;
	private TetrisPiece _piece = null;
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
			transform.localPosition = Vector3.zero;
			// transform bounding box
			bl = bbl - (Vector3)lbl;
			ur = bur - (Vector3)lur;
		}
	}

	// Use this for initialization
	void Start () {
		foreach(GameObject pm in pieceMarkers) {
			pm.renderer.material.color = markerColor;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Piece == null) {
			GetNextPiece();
		}
		// move the piece around
		Vector3 pos = transform.position;
		pos.x -= System.Convert.ToInt32(Input.GetKeyDown(KeyCode.LeftArrow));
		pos.x += System.Convert.ToInt32(Input.GetKeyDown(KeyCode.RightArrow));
		pos.y += System.Convert.ToInt32(Input.GetKeyDown(KeyCode.UpArrow));
		pos.y -= System.Convert.ToInt32(Input.GetKeyDown(KeyCode.DownArrow));


		// clamp piece inside board
		transform.position = Vector3.Max(bl,Vector3.Min(ur,pos));

		// try to place the piece
		if(Input.GetKeyDown(KeyCode.Space) && Place ()) {
			// move to the next piece
			GetNextPiece();
			transform.localPosition = Vector3.zero;
		}
	}

	// Check if each element can be placed, if so, set board
	bool Place() {
		List<Field> fs = new List<Field>(Piece.boxes.Count);


		// check if each element can be placed
		bool anchor = true;
		for(int i = 0; i < Piece.boxes.Count; i++) {
			Box b = Piece.boxes[i];
			Vector3 pos = pieceMarkers[i].transform.position;
			// look for piece at location
			RaycastHit info;
			if(Physics.Raycast(pos,Vector3.forward,out info)) {
				// get field for piece
				Field f = info.collider.gameObject.GetComponent<Field>();
				if(f && f.canPlace) {

					// store field to modify if we can place
					fs.Add(f);

					// anchor only if every piece can be anchored
					Vector2 opos = info.collider.gameObject.transform.position;
					anchor &= (b.pos - opos).sqrMagnitude < 0.5*0.5;

					continue;
				}
			}
			return false;
		}

		// place the elements
		for(int i = 0; i < Piece.boxes.Count; i++) {
			fs[i].value = Piece.boxes[i].value;
			if(anchor) {
				fs[i].canPlace = false;
				fs[i].renderer.material.mainTexture = game.lockNum[fs[i].value];
			}
		}

		if(anchor) {
			game.splicer.pieces.RemoveAt(PieceIdx);
		}

		return true;
	}

	void GetNextPiece() {
		if(game.splicer.pieces.Count > 0) {
			PieceIdx = Random.Range(0,game.splicer.pieces.Count);
			Piece = game.splicer.pieces[PieceIdx];
			Debug.Log ("start piece");
			Debug.Log(Piece.boxes[0].pos);
		}

	}
}
