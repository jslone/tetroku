using UnityEngine;
using System.Collections;

public class TimeScore {

	public static string toString(float t) {
		float ns = t * 10000000.0f;
		System.DateTime dt = new System.DateTime(System.Convert.ToInt64(ns));
		return string.Format("{0:HH:mm:ss}",dt);
	}
}
