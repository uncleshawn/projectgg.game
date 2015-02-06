using UnityEngine;
using System.Collections;

public class bulletGetSpeed : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void setBulletSpeed(Vector3 speed){

	}

	public void shotBullet(Vector3 bulletSpeed){
		this.rigidbody.velocity = bulletSpeed;
	}
}
