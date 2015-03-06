using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class charlogic : monsterbaselogic {

	private float mHurtTime = 0;

	private float mAccSpeed = 160.0f;
	// Use this for initialization
	void Start () {
		Debug.Log ("charlogic start");
	}
	
	// Update is called once per frame
	void Update () {
		if (mHurtTime > 0) {
			mHurtTime = mHurtTime - Time.deltaTime ;
		}
		if (mHurtTime < 0) {
			mHurtTime = 0;
		}
	}


	public bool isWUDI(){
		return mHurtTime > 0;
	}

	public void setWUDI(){
		char_property pro = this.gameObject.GetComponent<char_property> ();
		mHurtTime = pro.HurtTime;
	}

	//player beattacked must edit in next 玩家收到伤害 将要修改
	override public void beAttack(GameObject obj){
		Debug.Log ("char beAttack");
		if (isWUDI ()) {
			return;
		}
		enemy_property enemyProperty = obj.GetComponent<enemy_property>();
		if(enemyProperty != null){
			char_property charProperty = gameObject.GetComponent<char_property>();
			charProperty.Hp = charProperty.Hp - 1;

			if(isDie()){
				constant.getGameLogic().Die();
			}

			setWUDI();
		}
	}


	//玩家是否捡起道具
	public bool grapItem(GameObject item){
		bool boolGrap = false;
		bool inbagAlright = false;
		item_property itemProperty = item.GetComponent<item_property>();
		
		if(itemProperty){
			List<itemType> itype = itemProperty.iType;
			
			if(itype.Count == 0){
				//Debug.Log ("item has no property!");
				return false;
			}
			
			//find each tpye property 
			for(int index = 0; index < itype.Count ; index++){
				
				if(itemProperty.iType[index] == itemType.recover){
					if(recoverChar(item)){
						boolGrap = true;
					}
				}
				if(itemProperty.iType[index] == itemType.enforce){
					if(enforceChar(item)){
						putInBag(item,inbagAlright);
						inbagAlright = true;
						boolGrap = true;
					}
				}
				if(itemProperty.iType[index] == itemType.equipment){
					if(equipItem(item)){
						putInBag(item,inbagAlright);
						inbagAlright = true;
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



	//恢复玩家生命或者能量
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

	//强加玩家属性
	public bool enforceChar(GameObject obj){
		int graped = 0;
		enforce_Property enforceProperty = obj.GetComponent<enforce_Property>();
		char_property charProperty = gameObject.GetComponent<char_property>();
		if(enforceProperty){
			charProperty.MaxHp += enforceProperty.MaxHp;
			charProperty.Hp += enforceProperty.MaxHp;
			charProperty.MaxNp += enforceProperty.MaxNp;
			charProperty.Np += enforceProperty.MaxNp;
			charProperty.MaxMoveSpeed += enforceProperty.MaxMoveSpeed;
			charProperty.MoveSpeed += enforceProperty.MaxMoveSpeed;
			charProperty.MaxDamage += enforceProperty.MaxDamage;
			charProperty.Damage += enforceProperty.MaxDamage;
			charProperty.MaxAttackSpeed += enforceProperty.MaxAttackSpeed;
			charProperty.AttackSpeed += enforceProperty.MaxAttackSpeed;
			charProperty.MaxAttackRate += enforceProperty.MaxAttackRate;
			charProperty.AttackRate += enforceProperty.MaxAttackRate;
			charProperty.MaxAttackDistance += enforceProperty.MaxAttackDistance; 
			charProperty.AttackDistance += enforceProperty.MaxAttackDistance; 

			charProperty.upgradeShootProperties();
			graped += 1; 
		}
		
		if(graped > 0){
			return true;
		}
		
		return false;

	}




	public bool equipItem(GameObject obj){
		string equipmentPath = obj.GetComponent<equipItem_Property>().EquipPath;
		Debug.Log("玩家获得装备: " + equipmentPath);
		//GameObject equipmentClone = (GameObject)Instantiate(Resources.Load(equipmentPath),this.transform.position,Quaternion.identity);
		GameObject prefab = Resources . Load < GameObject > ( equipmentPath ) ;
		GameObject equipmentClone = Instantiate ( prefab ) as GameObject ;

		if(equipmentClone){
			GameObject player = this.gameObject;

			
			equipmentClone.transform.parent = player.gameObject.transform;
			equipmentClone.transform.localPosition = new Vector3(0,0,0);
			equipmentClone.GetComponent<followerlogic>().startWork();
			return true;
		}
		return false;
	}

	//在玩家背包里留下物品的图标
	public bool putInBag(GameObject obj, bool checkBag){
		if(checkBag){
			return false;
		}
		int itemId = obj.GetComponent<item_property>().ID;
		constant.getMapLogic().bagAddIcon(itemId);
		return true;
	}


	//玩家是否失败
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
		v.x = x*mAccSpeed;
		v.y = y*mAccSpeed;
		return v;
	}



}
