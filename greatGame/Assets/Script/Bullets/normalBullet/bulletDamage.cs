using UnityEngine;
using System.Collections;

public class bulletDamage : MonoBehaviour {

	// Use this for initialization
	float cTime;
	bullet_property bulletPro;
	public XInputVibrateTest vibrate;


	void aWake(){

	}
	void Start () {
		cTime = 99;
		bulletPro = gameObject.GetComponent<bullet_property>();
		if(vibrate = GameObject.Find("preLoad").GetComponent<XInputVibrateTest>()){
		}

	}
	
	// Update is called once per frame
	void Update () {
		cTime += Time.deltaTime;
	}

	void OnTriggerStay(Collider other)
	{
		enemylogic logic = other.gameObject.GetComponent<enemylogic>();
		if(logic){
			if(cTime >= bulletPro.bulletDamageRate) {
				Debug.Log("bullet send damage to enemy.");
				logic.beAttack(this.gameObject);
				//vibrate.joystickVibrate(0.2f,1,1);
				cTime = 0;
			}
		}
	}
}
