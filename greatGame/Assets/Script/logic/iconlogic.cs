using UnityEngine;
using System.Collections;

public class iconlogic : MonoBehaviour {

	GameObject obj;
	tk2dSprite icon;
	// Use this for initialization
	void Start () {
		icon = gameObject.GetComponent<tk2dSprite>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void  checkObjectType(){
		obj = transform.parent.gameObject;
		if(obj.GetComponent<item_property>()){
			item_property itemproperty = obj.GetComponent<item_property>();
			int iconId = itemproperty.ID;
			if(iconId!=0){
				icon.Collection = "";
			}
		}
	}
}
