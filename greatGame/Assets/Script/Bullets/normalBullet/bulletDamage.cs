using UnityEngine;
using System.Collections;

public class bulletDamage : MonoBehaviour {

	// Use this for initialization
	//float damage;
	public XInputVibrateTest vibrate;
	void Start () {
		//damage = 50;
		if(vibrate = GameObject.Find("preLoad").GetComponent<XInputVibrateTest>()){
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("碰撞到的物体的名字是：" + other.gameObject.name);
		enemylogic logic = other.gameObject.GetComponent<enemylogic>();
		if(logic){
			//Debug.Log("enmey get damage: " + damage);
			//enemyProperty enemy = other.gameObject.GetComponent<enemyProperty>();
			//enemy.getDamage(damage);
			//pro
			logic.beAttack(this.gameObject);
			//vibrate.joystickVibrate(0.2f,1,1);

		}
	}
}
