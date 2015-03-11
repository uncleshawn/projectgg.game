using UnityEngine;
using System.Collections;

public class startmap : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		if (Application.loadedLevel == 0) {
			return;
		}
		maplogic mapLogic = constant.getMapLogic ();
		mapLogic.startRoom ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
