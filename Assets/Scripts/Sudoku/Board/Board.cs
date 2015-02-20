using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {
	public Field[,] fields = new Field[9,9];

	// Use this for initialization
	void Start () {
		SubBoard[] sbs = gameObject.GetComponentsInChildren<SubBoard>();
		foreach(SubBoard sb in sbs) {
			int X = sb.name[0].ParseInt32();
			int Y = sb.name[1].ParseInt32();
			for(int x = 0; x < 3; x++) {
				for(int y = 0; y < 3; y++) {
					int i = 3*X+x;
					int j = 3*Y+y;
					fields[i,j] = sb.fields[x,y];
					fields[i,j].row = i;
					fields[i,j].col = j;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
