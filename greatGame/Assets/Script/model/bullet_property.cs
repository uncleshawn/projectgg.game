using UnityEngine;
using System.Collections;

public class bullet_property : MonoBehaviour {

	public int mDamage;
	public int mNoknock;
	public int mRate;

	// Use this for initialization
	void Awake(){
		mDamage = 50;
		mNoknock = 1;
		mRate = 1;
	}
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void setProperty(GameObject player){

	}
}
