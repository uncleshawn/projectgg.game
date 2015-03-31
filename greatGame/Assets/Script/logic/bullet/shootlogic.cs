using UnityEngine;
using System.Collections;

public class shootlogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void selectWeapon(weaponType weapontype){
		switch(weapontype){
		default: 
			break;
			
		case weaponType.bulletNormal:	gameObject.AddComponent<robotShotNormalBullet>(); break;
		case weaponType.laserNormal:	break;
			
			
			break;
		}
	}


}
