﻿//这个道具将会在被玩家拾取后显示在玩家身上
//这个道具将会在背包里留下图标


using UnityEngine;
using System.Collections;

public class equipItem_Property : MonoBehaviour {

	private int mID;
	private string mEquipPath;
	public int ID { get { return mID; } set { mID = value; }}
	public string EquipPath { get { return mEquipPath;}  set { mEquipPath = value; }}
	// Use this for initialization
	void Awake(){
		item_property itemProperty = gameObject.GetComponent<item_property>();
		mID = itemProperty.ID;

	}
	void Start () {
		if(mID!=0){
			mEquipPath = "Prefabs/equipment/equipment"+mID;
		}
		else{
			Debug.Log("error: item do not have ID, please give an ID!");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
