using UnityEngine;
using UnityEditor;
using System.Collections;

[InitializeOnLoad]
public class ClearPrefs {
	private static bool wasPaused = false;
	
	static ClearPrefs() {
		EditorApplication.playmodeStateChanged += OnPlayModeChange;
	}
	
	private static void OnPlayModeChange() {
		if(EditorApplication.isPlaying && !wasPaused) {
			PlayerPrefs.DeleteKey("played");
		}
		else if(EditorApplication.isPaused) {
			wasPaused = true;
		} else {
			wasPaused = false;
		}
	}
}
