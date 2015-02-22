using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hints : MonoBehaviour {

	public int numHints; // number of remaining hints
	public Text hintText;
	public Button hintButton;
	public PiecePlacer piecePlacer;
	public Animator[] borders;
	public Game game;
	public StandAloneInputModuleClearable input;

	// Use this for initialization
	void Start () {
		hintText.text = "Hints: " + numHints;
	}

	void Update() {
		if (game.solved && hintButton.enabled) {
			disableHint();
		}
	}

	private void disableHint() {
		hintButton.enabled = false;
		hintText.color = hintButton.colors.disabledColor;
	}

	public void UseHint(){
		input.Clear();
		if (hintButton.enabled) {
			// Highlight the appropriate 3x3 subboard based on first field
			Point pos = piecePlacer.Piece.origin;

			// Get upper-left position of subboard
			// leave out the / 3 if you want position in {0, 1, 2}
			

			// TODO: fade in some glowing outline sprite over subboard
			numHints--;
			hintText.text = "Hints: " + numHints;
			
			borders[3*(pos.x/3) + (pos.y/3)].SetTrigger("glow");
			if (numHints == 0) {
				disableHint();
			}
		} else {
			// TODO: play sound effect when disabled
			SoundManager.Play(SOUND_EFFECTS.ERROR);
		}
	}
}

