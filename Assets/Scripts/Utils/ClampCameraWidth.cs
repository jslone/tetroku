using UnityEngine;
using System.Collections;

public class ClampCameraWidth : MonoBehaviour {
	public float minWidth;
	public float maxSize;
	
	// Use this for initialization
	void Start () {
		maxSize = camera.orthographicSize;
		Resize();
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
		Resize();
		#endif
	}
	
	void Resize() {
		float adjSize = minWidth / camera.aspect;
		
		camera.orthographicSize = Mathf.Max (maxSize,adjSize);
		/*if(width < minWidth) {
			camera.orthographicSize = minWidth / camera.aspect;
		}*/
	}
}
