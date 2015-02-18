using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hints : MonoBehaviour {

	public int numHints; // number of remaining hints
	public Text hintText;
	public Button hintButton;
	public PiecePlacer piecePlacer;

	// Use this for initialization
	void Start () {
		hintText.text = "Hints: " + numHints;
	}

	public void UseHint(){
		if (numHints > 0) {
			// Highlight the appropriate 3x3 subboard based on first field
			Vector2 pos = piecePlacer.Piece.boxes[0].pos;

			// Get upper-left position of subboard
			// leave out the / 3 if you want position in {0, 1, 2}
			Vector2 subboard = new Vector2(((int) pos.x) / 3 * 3, ((int) pos.y) / 3 * 3);
			Debug.Log(subboard);

			// TODO: fade in some glowing outline sprite over subboard
			numHints--;
			hintText.text = "Hints: " + numHints;

			if (numHints == 0) {
				hintButton.enabled = false;
				hintText.color = hintButton.colors.disabledColor;
			}
		} else {
			// TODO: play sound effect when disabled
		}
	}
}

