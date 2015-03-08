using UnityEngine;
using System.Collections;

public class startmap : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		maplogic mapLogic = constant.getMapLogic ();
		mapLogic.startRoom ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
