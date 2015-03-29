using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class bulletlogic : MonoBehaviour {

		// Use this for initialization
		//public XInputVibrateTest vibrate;
		float cTime;
		bullet_property bulletPro;
		Dictionary<string,float> enemyDictionary= new Dictionary<string,float>();


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

				string[] keys = new string[enemyDictionary.Count];
				int i = 0;
				foreach (string key in enemyDictionary.Keys) keys[i++] = key;
				for (i = 0; i < keys.Length; i++)
				{
						enemyDictionary [keys [i]] += Time.deltaTime;
						//Debug.Log ("enemyDictionary.key: " + keys [i] + " Value: " + enemyDictionary [keys [i]] );
				}
		}

		void OnTriggerStay(Collider other)
		{		//判断是的是否开始工作
				if(doDamage){

						bulletPro = gameObject.GetComponent<bullet_property>();
						//主角发出的子弹
						if (bulletPro.BattleType == constant.BattleType.Player) {
								enemylogic logic = other.gameObject.GetComponent<enemylogic> ();
								if (logic) { 
										if (!enemyDictionary.ContainsKey(other.name))
										{
												enemyDictionary.Add (other.name, 99);
										}


										if (enemyDictionary[other.name] >= bulletPro.bulletDamageRate) {

												constant.getMapLogic ().triggerEnter (other.gameObject, this.gameObject);
												//vibrate.joystickVibrate(0.2f,1,1);
												enemyDictionary[other.name] = 0;
										} else {
												//Debug.Log ("伤害间隔中,无法造成伤害 debug_cTime = " + cTime);
										}
								}
						}

						if (bulletPro.BattleType == constant.BattleType.Enemy) {
								char_property logic = other.gameObject.GetComponent<char_property> ();
								if (logic) { 

										if (cTime >= bulletPro.bulletDamageRate) {
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
}
