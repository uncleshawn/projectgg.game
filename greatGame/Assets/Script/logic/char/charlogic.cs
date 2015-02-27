using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class charlogic : monsterbaselogic {

	// Use this for initialization
	void Start () {
		Debug.Log ("charlogic start");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//player touch "item" logic
	public bool grapItem(GameObject item){
		bool boolGrap = false;
		item_Property itemProperty = item.GetComponent<item_Property>();

		if(itemProperty){
			List<itemType> itype = itemProperty.iType;

			if(itype.Count == 0){
				Debug.Log ("item has no property!");
				return false;
			}

			//find each tpye property 
			for(int index = 0; index < itype.Count ; index++){

				if(itemProperty.iType[index] == itemType.recover){
					if(recoverChar(item)){
						boolGrap = true;
					}
				}
				if(itemProperty.iType[index] == itemType.inBag){
					if(putInBag(item)){
						boolGrap = true;
					}
				}
			}

		}

		//check item function if used
		if(boolGrap) {
			return true;
		}

		//can not grap this item
		return false;

	}

	public void beAttack(GameObject obj){
		Debug.Log ("char beAttack");
		enemy_property enemyProperty = obj.GetComponent<enemy_property>();
		if(enemyProperty != null){
			char_property charProperty = gameObject.GetComponent<char_property>();
			charProperty.Hp = charProperty.Hp - 1;

			if(isDie()){
				constant.getGameLogic().Die();
			}
		}
	}

	public bool recoverChar(GameObject obj){
		int graped = 0;
		recover_Property recoverProperty =  obj.GetComponent<recover_Property>();
		char_property charProperty = gameObject.GetComponent<char_property>();
		if(recoverProperty){

			if(recoverProperty.recoverHp > 0 && charProperty.Hp < charProperty.MaxHp ) {
				int hp = charProperty.Hp + recoverProperty.recoverHp;
				if(hp > charProperty.MaxHp){
					hp = charProperty.MaxHp;
				}
				charProperty.Hp = hp;
				graped += 1;
			}

			if(recoverProperty.recoverNp > 0 && charProperty.Hp < charProperty.MaxNp ) {
				int hp = charProperty.Np + recoverProperty.recoverNp;
				if(hp > charProperty.MaxNp){
					hp = charProperty.MaxNp;
				}
				charProperty.Hp = hp;
				graped += 1;
			}
		}

		if(graped > 0){
			return true;
		}

		return false;

	}

	public bool putInBag(GameObject obj){
		return false;
	}

	public bool isDie(){
		char_property charProperty = gameObject.GetComponent<char_property>();
		if (charProperty.Hp <= 0) {
			return true;
		}
		return false;
	}

	override public Vector3 getMoveAcc(){
		Vector3 v = new Vector3 ();
		v.x = 0;
		v.y = 0;
		v.z = 0;

		float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");
		v.x = x*50.0f;
		v.y = y*50.0f;
		return v;
	}

}
