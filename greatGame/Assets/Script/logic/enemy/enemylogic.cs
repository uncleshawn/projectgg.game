using UnityEngine;
using System.Collections;

public class enemylogic : monsterbaselogic {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void beAttack(GameObject obj){
		if (obj.tag.Equals ("Bullet")) {
			bullet_property pro = obj.GetComponent<bullet_property>();

			enemy_property enemyProperty = gameObject.GetComponent<enemy_property>();
			enemyProperty.Hp = enemyProperty.Hp - pro.mDamage;
			
			if(isDie()){
				GameObject.Destroy(this.gameObject);
				Debug.Log("enemy die");
				constant.getMapLogic().checkOpenDoor();
			}
		}
	}

	public bool isDie(){
		enemy_property enemyProperty = gameObject.GetComponent<enemy_property>();
		if (enemyProperty.Hp <= 0) {
			return true;
		}
		return false;
	}
}
