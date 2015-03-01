using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class item_property : base_property {
	
	//public string typeName;
	public List<itemType> iType ;
	public itemType mType;
	public string itemName;
	public int itemId;
	
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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void selectItemType(string str){
		switch(str){
		default	: 		mType = itemType.none; 		break;
		case "recover": mType = itemType.recover;	break;
		case "enforce":	mType = itemType.enforce;  	break; 
		}
	}
	
	
}

