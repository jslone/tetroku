using UnityEngine;
using System.Collections;

public class SubBoard : MonoBehaviour {
	public Field[,] fields = new Field[3,3];

	// Use this for initialization
	void Awake () {
		Field[] fs = gameObject.GetComponentsInChildren<Field>();
		foreach(Field field in fs) {
			int x = field.name[0].ParseInt32();
			int y = field.name[1].ParseInt32();
			fields[x,y] = field;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
