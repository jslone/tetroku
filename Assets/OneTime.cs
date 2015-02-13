using UnityEngine;
using System.Collections;

public class OneTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("played",0) == 0) {
			PlayerPrefs.SetInt("played",1);
		} else {
			Close();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown) {
			Close ();
		}
	}
	
	void Close() {
		gameObject.SetActive(false);
	}
}
