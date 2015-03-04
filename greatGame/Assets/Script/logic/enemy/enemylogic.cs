using UnityEngine;
using System.Collections;

public class enemylogic : monsterbaselogic {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override public void beAttack(GameObject obj){
		if (obj.tag.Equals ("Bullet")) {
			Debug.Log("enemy be attacked by bullet.");
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
		//Debug.Log("enemy get damage: " + bulletProperty.bulletDamage);
		enemyProperty.Hp = enemyProperty.Hp - bulletProperty.bulletDamage;

		Vector3 objectPos = this.transform.position;
		objectPos.z = -5;

		string showDamagePath = "Prefabs/ui/UI_showDamage";
		GameObject showDamageClone = (GameObject)Instantiate(Resources.Load(showDamagePath),objectPos,Quaternion.identity);
		if(showDamageClone){

			string damage = "-" + bulletProperty.bulletDamage.ToString();
			showDamageClone.GetComponent<enemy_showDamage>().mNum = damage;
			showDamageClone.GetComponent<enemy_showDamage>().showDamage();
		}
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
