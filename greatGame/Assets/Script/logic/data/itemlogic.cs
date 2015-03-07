using UnityEngine;
using System.Collections;

public class itemlogic : monsterbaselogic {

	// Use this for initialization
	void Start () {
		debugShowName();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	//debug mode use
	public void debugShowName() {

		if(GameObject.FindGameObjectWithTag("GameLogic").GetComponent<uilogic>().debugText == true){
			string itemName = this.gameObject.GetComponent<item_property>().itemName;
			GameObject debugText = transform.FindChild("debugMode_itemName").gameObject;
			if(debugText){
				debugText.GetComponent<tk2dTextMesh>().text = itemName;
			}
		}
	}
}
