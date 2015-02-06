using UnityEngine;
using System.Collections;

public class gamelogic : MonoBehaviour {

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Die(){
		constant.getUiLogic ().Die ();
	}
}
