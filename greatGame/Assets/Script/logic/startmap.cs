using UnityEngine;
using System.Collections;

public class startmap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		maplogic mapLogic = constant.getMapLogic ();
		mapLogic.startRoom ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
