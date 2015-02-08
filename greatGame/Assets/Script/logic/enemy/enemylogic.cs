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
			bullet_property bulletProperty = obj.GetComponent<bullet_property>();
			enemy_property enemyProperty = gameObject.GetComponent<enemy_property>();

			getDamage(enemyProperty,bulletProperty);
			
			if(isDie()){
				GameObject.Destroy(this.gameObject);
				constant.getMapLogic().checkOpenDoor();
			}
		}
	}


	public void getDamage(enemy_property enemyProperty,bullet_property bulletProperty){
		Debug.Log("enemy get damage: " + bulletProperty.bulletDamage);
		enemyProperty.Hp = enemyProperty.Hp - bulletProperty.bulletDamage;
	}

	public void getKnockBack(enemy_property enemyProperty){

	}

	public void getEffect(enemy_property enemyProperty){

	}

	public bool isDie(){
		enemy_property enemyProperty = gameObject.GetComponent<enemy_property>();
		if (enemyProperty.Hp <= 0) {
			return true;
		}
		return false;
	}
}
