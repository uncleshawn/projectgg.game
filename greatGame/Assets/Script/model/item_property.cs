using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class item_property : base_property {

		//public string typeName;
		public List<itemType> iType ;

		public string itemName;
		public int mID;
		itemType mType;

		public int ID { get { return mID; } set { mID = value; }}

		// Use this for initialization
		void Awake() {
				this.gameObject.tag = "Item";
				iType = new List<itemType> () ; 

				if(transform.gameObject.GetComponent<recover_Property>()){
						//Debug.Log("item has recover property!");
						mType = itemType.recover;
						iType.Add(mType);
				}
				if(transform.gameObject.GetComponent<enforce_Property>()){
						//Debug.Log("item has enforce property!");
						mType = itemType.enforce;
						iType.Add(mType);
				}
				if(transform.gameObject.GetComponent<equipItem_Property>()){
						//Debug.Log("item has equip property!");
						mType = itemType.equipment;
						iType.Add(mType);
				}
				if(transform.gameObject.GetComponent<treasure_property>()){
						//Debug.Log("item has treasure property!");
						mType = itemType.treasure;
						iType.Add(mType);
				}
				if(transform.gameObject.GetComponent<weaponItem_property>()){
						//Debug.Log("item has weapon property!");
						mType = itemType.weapon;
						iType.Add(mType);
				}
				if(transform.gameObject.GetComponent<bulletEnforce_Property>()){
						//Debug.Log("item has special bullet property!");
						mType = itemType.bulletEnforce;
						iType.Add(mType);
				}


				mBaseMoveSpeed = 15.0f;
		}
		void Start () {
				
		}

		// Update is called once per frame
		void Update () {

		}

}

