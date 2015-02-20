using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class StandAloneInputModuleClearable : StandaloneInputModule {

	public void Clear() {
		ClearSelection();
	}
}
