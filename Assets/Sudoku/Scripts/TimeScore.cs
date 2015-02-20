using UnityEngine;
using System.Collections;

public class TimeScore {

	public static string toString(float t) {
		System.DateTime dt = new System.DateTime(Mathf.RoundToInt(t * 10000000.0f));
		return string.Format("{0:HH:mm:ss}",dt);
	}
}
