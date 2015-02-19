using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICopyText : MonoBehaviour {
	public Text dest;
	public Text src;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		dest.text = src.text;
	}
}
