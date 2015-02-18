using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PiecePlacer : MonoBehaviour {
	public Game game;
	public GameObject[] pieceMarkers;

	private static Vector3 bbl = new Vector3(0,0,0);
	private static Vector3 bur = new Vector3(8,8,0);

	public Vector3 bl = bbl;
	public Vector3 ur = bur;
	
	public float speed = 1.0f;
	
	public Vector3 position;
	
	public float axisDelay;
	private float lastAxisDown;
	private bool isClick = false;
	private bool axisDown = false;
	
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
				Vector2 lPos = (Vector2)_piece.boxes[i].pos - origin;
				pieceMarkers[i].transform.localPosition = lPos;

				// set child texture
				pieceMarkers[i].GetComponent<SpriteRenderer>().sprite = game.num[_piece.boxes[i].value];

				// expand bounding box
				lbl = Vector2.Min(lbl,lPos);
				lur = Vector2.Max(lur,lPos);
			}
			transform.localPosition = Vector3.zero;
			position = transform.position;
			// transform bounding box
			bl = bbl - (Vector3)lbl;
			ur = bur - (Vector3)lur;
		}
	}

	// Use this for initialization
	void Start () {
		position = transform.position;
		foreach(GameObject pm in pieceMarkers) {
			pm.renderer.material.color = game.GoodColor;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(game.solved) {
			Destroy(gameObject);
		}
		if(Piece == null) {
			GetNextPiece();
		}
		
		if(!game.paused) {
			
			// set mouse position if in boundries and has moved
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if(Mathf.Abs(Input.GetAxis("Mouse X")) > 0 &&
				Mathf.Abs(Input.GetAxis("Mouse Y")) > 0) {
				
				isClick = false;
				if(mousePos.x >= bbl.x - 1 && mousePos.x <= bur.x + 1 &&
					mousePos.y >= bbl.y - 1 && mousePos.y <= bur.y + 1) {
					position = mousePos;
                }
			}
			// set movement based on axis (keyboard / controller)
			float x = Input.GetAxis("Horizontal");
			float y = Input.GetAxis("Vertical");
			Vector3 dp = Vector3.zero;
			
			if(Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0) {
				if(!axisDown) {
					axisDown = true;
					lastAxisDown = Time.time;
					
					if(x < 0) x = Mathf.Floor(x);
					if(x > 0) x = Mathf.Ceil(x);
					if(y < 0) y = Mathf.Floor(y);
					if(y > 0) y = Mathf.Ceil(y);
					
					dp.x = x;
					dp.y = y;
				} else if(Time.time > lastAxisDown + axisDelay) {
					dp.x = x * speed * Time.deltaTime;
					dp.y = y * speed * Time.deltaTime;
				}
			} else {
				axisDown = false;
			}
			
			position += dp;
			
			// clamp piece inside board
			position = Vector3.Max(bl,Vector3.Min(ur,position));
			
			Vector3 roundedPos = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), Mathf.Round(position.z));
			transform.position = roundedPos;
			
			// try to place the piece
			if(Input.GetButtonDown("Submit")) {
				isClick = true;
			}
			
			if(Input.GetButtonUp("Submit") && isClick) {
				if(Place ()) {
					// move to the next piece
					GetNextPiece();
                } else {
                	// play invalid sound
                	SoundManager.Play(SOUND_EFFECTS.ERROR);
                }
            }
		}
		
		position = Vector3.Lerp(position,transform.position,0.1f);
	}

	// Check if each element can be placed, if so, set board
	bool Place() {
		List<Field> fs = new List<Field>(Piece.boxes.Count);


		// check if each element can be placed
		for(int i = 0; i < Piece.boxes.Count; i++) {
			Vector3 pos = pieceMarkers[i].transform.position;
			// look for piece at location
			Collider2D col;
			if((col = Physics2D.OverlapPoint(pos))) {
				// get field for piece
				Field f = col.GetComponent<Field>();
				if(f && f.canPlace) {

					// store field to modify if we can place
					fs.Add(f);
					continue;
				}
			}
			return false;
		}
		Debug.Log(game.splicer.pieces.Count);
		int anchorIdx = Piece.anchorPiece(transform.position,game.splicer.pieces);
		
		if(anchorIdx >= 0) {
			for(int i = 0; i < Piece.boxes.Count; i++) {
				fs[i].canPlace = false;
				fs[i].SetValue(Piece.boxes[i].value);
			}
			SoundManager.Play(SOUND_EFFECTS.POSITIVE);
			game.splicer.pieces.RemoveAt(anchorIdx);
		} else {
			bool oneRight = false;
			bool allRight = true;
			// place the elements
			for(int i = 0; i < Piece.boxes.Count; i++) {
				fs[i].canPlace = true;
				bool rightNumber = fs[i].SetValue(Piece.boxes[i].value);
				oneRight |= rightNumber;
				allRight &= rightNumber;
			}
			
			if(oneRight) {
				// play neutral sound
				SoundManager.Play(SOUND_EFFECTS.NEUTRAL);
			} else {
				// play bad sound
				SoundManager.Play(SOUND_EFFECTS.NEGATIVE);
			}
		}
		return true;
	}

	void GetNextPiece() {
		if(game.splicer.pieces.Count > 0) {
			PieceIdx = Random.Range(0,game.splicer.pieces.Count);
			Piece = game.splicer.pieces[PieceIdx];
			Debug.Log(Piece.boxes[0].pos);
		}

	}
}
