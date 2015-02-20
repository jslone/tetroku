using UnityEngine;
using System.Collections;

public class Screenshot : MonoBehaviour {
	static int count = 0;
	static Screenshot _instance = null;
	// Use this for initialization
	void Awake () {
		if(_instance == null) {
			_instance = this;
			DontDestroyOnLoad(gameObject);
			#if UNITY_EDITOR
			InvokeRepeating("Take",3.0f,3.0f);
			#endif
		} else {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Take() {
		Debug.Log("called");
		Application.CaptureScreenshot("Screenshots/screenshopt" + count++ + ".png");
	}
}
