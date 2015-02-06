using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class item_Property : MonoBehaviour {

	public string typeName;
	public List<itemType> iType ;
	public itemType mType;

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

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void selectItemType(string str){
		switch(str){
		default	: 		mType = itemType.none; 		break;
		case "recover": mType = itemType.recover;	break;
		case "inBag":	mType = itemType.inBag;  	break; 
		}
	}
}
