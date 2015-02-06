using UnityEngine;
using System.Collections;

public class enemyProperty : MonoBehaviour {

	// Use this for initialization
	float hpmax = 100;
	float hp 	= 100;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		checkHp();
	}

	public void getDamage(float damage){
		this.hp -= damage;
	}

	void checkHp(){
		//Debug.Log("敌人生命为: "+ hp);
		if(hp<=0){
			Destroy(this.gameObject);
		}
		if(hp>=hpmax){
			hp = hpmax;
		}
	}
}
