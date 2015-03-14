using UnityEngine;
using System.Collections;

public class bulletlogic : MonoBehaviour {

		// Use this for initialization
		//public XInputVibrateTest vibrate;
		float cTime;
		bullet_property bulletPro;

		public bool doDamage = true;


		void aWake(){
				

		}

		void Start () {
				//初始化时间,让子弹第一时间能造成伤害
				cTime = 99;

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
								if (cTime >= bulletPro.bulletDamageRate) {
										//Debug.Log ("子弹造成伤害");
										//Debug.Log("bullet send damage to enemy.");
										//use maplogic to deal damage
										constant.getMapLogic ().triggerEnter (other.gameObject, this.gameObject);
										//vibrate.joystickVibrate(0.2f,1,1);
										cTime = 0;
								} else {
										//Debug.Log ("伤害间隔中,无法造成伤害 debug_cTime = " + cTime);
								}
						}
				}
		}
}
