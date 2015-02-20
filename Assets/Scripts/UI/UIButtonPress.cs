using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIButtonPress : MonoBehaviour {
	public string Button;
	
	// Update is called once per frame
	void Update () {
		PointerEventData pointer = new PointerEventData(EventSystem.current);
		if(Input.GetButtonDown(Button)) {
			ExecuteEvents.Execute(gameObject, pointer, ExecuteEvents.pointerDownHandler);
		}
		if(Input.GetButton(Button)) {
			ExecuteEvents.Execute(gameObject, pointer, ExecuteEvents.pointerClickHandler);
		}
		if(Input.GetButtonUp(Button)) {
			ExecuteEvents.Execute(gameObject, pointer, ExecuteEvents.pointerUpHandler);
		}
	}
}
