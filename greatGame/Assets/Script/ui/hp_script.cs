using UnityEngine;
using System.Collections;

public class hp_script : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject obj = GameObject.FindGameObjectWithTag ("Player");

		if (obj == null) {
			return;
		}


		//Component charProperty = obj.GetComponent<"char_property">();
		//int hp = charProperty.getHp();

		char_property charProperty = obj.GetComponent<char_property>();
		int hp = charProperty.Hp;

		int totalHp = 5;
		for(int i = 0; i < 5; ++i){
			GameObject hpSprObj = GameObject.Find("hp_child" + (i+1));
			tk2dSprite spr = hpSprObj.GetComponentInChildren<tk2dSprite>();
			if(i < hp){
				spr.SetSprite(spr.GetSpriteIdByName("heart_full"));
			}else{
				spr.SetSprite(spr.GetSpriteIdByName("heart_empty"));
			}
		}
	}
}
