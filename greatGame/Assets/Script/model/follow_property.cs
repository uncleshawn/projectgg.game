using UnityEngine;
using System.Collections;

public class follow_property : char_property {

	public weaponType bulletType;
	
	// Use this for initialization

	void Start () {
		upgradeShootProperties();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void upgradeShootProperties(){
		GameObject shoot = this.transform.FindChild("shoot").gameObject;
		if(shoot){
			shoot.SendMessage("upgradeProperties",this);
		}
	}
}
