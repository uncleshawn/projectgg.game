using UnityEngine;
using System.Collections;

public class monsterbaselogic : MonoBehaviour {

	private float mFAcc = 2.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector3 getMoveAcc(){
		Vector3 v = new Vector3 ();
		v.x = 0;
		v.y = 0;
		v.z = 0;
		return v;
	}

	public Vector3 getFAcc(){
		Vector3 v = new Vector3 ();
		v.x = mFAcc;
		v.y = mFAcc;
		v.z = 0;
		return v;
	}
}
