using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class item_property : base_property {
	
	//public string typeName;
	public List<itemType> iType ;
	public itemType mType;
	public string itemName;
	public int mID;
	public int ID { get { return mID; } set { mID = value; }}
	// Use this for initialization
	void Awake() {
		this.gameObject.tag = "Item";
	}
	void Start () {
		iType = new List<itemType> () ; 
		
		if(transform.gameObject.GetComponent<recover_Property>()){
			Debug.Log("item has recover property!");
			mType = itemType.recover;
			iType.Add(mType);
		}
		if(transform.gameObject.GetComponent<enforce_Property>()){
			Debug.Log("item has enforce property!");
			mType = itemType.enforce;
			iType.Add(mType);
		}
		if(transform.gameObject.GetComponent<equipItem_Property>()){
			Debug.Log("item has equip property!");
			mType = itemType.equipment;
			iType.Add(mType);
		}

		mBaseMoveSpeed = 15.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
}

