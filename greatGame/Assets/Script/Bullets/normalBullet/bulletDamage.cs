using UnityEngine;
using System.Collections;

public class bulletDamage : MonoBehaviour {

	// Use this for initialization
	float cTime;
	bullet_property bulletPro;
	public XInputVibrateTest vibrate;
	public bool doDamage = true;


	void aWake(){

	}
	void Start () {
		cTime = 99;
		if(vibrate = GameObject.Find("preLoad").GetComponent<XInputVibrateTest>()){
		}

	}
	
	// Update is called once per frame
	void Update () {
		cTime += Time.deltaTime;
	}

	void OnTriggerStay(Collider other)
	{
		if(doDamage){
			//Debug.Log("bullet: touch object= " + other.name);
			bulletPro = gameObject.GetComponent<bullet_property>();
			enemylogic logic = other.gameObject.GetComponent<enemylogic>();
			if(logic){ 
				//Debug.Log("CurrentTime : damageRate = " + cTime +" : "+bulletPro.bulletDamageRate);
				if(cTime >= bulletPro.bulletDamageRate) {
					//Debug.Log("bullet send damage to enemy.");

					//use maplogic to deal damage
					constant.getMapLogic ().triggerEnter (other.gameObject, this.gameObject);
					//vibrate.joystickVibrate(0.2f,1,1);
					cTime = 0;
				}
			}
		}
	}
}
