using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
	public RuntimePlatform[] platforms;
	// Use this for initialization
	void Awake () {
		foreach(RuntimePlatform platform in platforms) {
			if(Application.platform == platform) {
				return;
			}
		}
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
