using UnityEngine;
using System.Collections;

public class itemAniManager : MonoBehaviour {

	tk2dSpriteAnimator itemAni;
	void Awake() {
		itemAni = transform.gameObject.GetComponent<tk2dSpriteAnimator>();
	}
	// Use this for initialization
	void Start () {
		if(itemAni){
			itemAni.Play("wait");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
