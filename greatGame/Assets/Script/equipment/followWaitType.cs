using UnityEngine;
using System.Collections;

public class followWaitType : MonoBehaviour {

	public iTween iTween;
	// Use this for initialization
	void Start () {
		startMove();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void startMove(){
		iTween.MoveBy(gameObject, iTween.Hash("y", 0.5,  "easeType", "linear", "loopType", "pingpong" , "time" , 0.7 ));
	}
}
