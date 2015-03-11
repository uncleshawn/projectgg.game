using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class hp_script : MonoBehaviour {

	List<GameObject> mHps;

	string  hpPath = "Prefabs/ui/hp_child";	//子弹prefab
	// Use this for initialization
	void Start () {
		mHps = new List<GameObject> ();
	}

	private Vector3 getPosition(int i){
		Vector3 v = new Vector3();
		v.x = 1.667429f * i;
		v.y = 0;
		v.z = 0;
		return v;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject obj = GameObject.FindGameObjectWithTag (constant.TAG_PLAYER);

		if (obj == null) {
			return;
		}

		char_property pro = obj.GetComponent<char_property> ();
		//Debug.Log ("pro hp:" + pro.MaxHp + "," + pro.Hp);

		int totalHp = pro.MaxHp;
		if (mHps.Count > pro.MaxHp) {
			for(int i = mHps.Count-1; i >= pro.MaxHp; --i){
				GameObject hpObj = mHps[i];
				mHps.Remove(hpObj);
				Destroy(hpObj);
			}
		}else if (mHps.Count < pro.MaxHp){
			int oldnum = mHps.Count;
			int num = pro.MaxHp - mHps.Count;
			for(int i = 0 ; i < num; ++i){
				Vector3 v = getPosition(i+oldnum);
				//GameObject hpObj = (GameObject)Instantiate(Resources.Load(hpPath),v,Quaternion.identity);
				GameObject prefab = Resources . Load < GameObject > ( hpPath ) ;
				GameObject hpObj = Instantiate ( prefab ) as GameObject ;

				hpObj.transform.parent = this.gameObject.transform;
				hpObj.transform.localPosition = v;
				mHps.Add(hpObj);
			}
		}

		//Component charProperty = obj.GetComponent<"char_property">();
		//int hp = charProperty.getHp();

		int hp = pro.Hp;

		for(int i = 0; i < totalHp; ++i){
			GameObject hpSprObj = mHps[i]; // GameObject.Find("hp_child" + (i+1));
			tk2dSprite spr = hpSprObj.GetComponentInChildren<tk2dSprite>();
			if(i < hp){
				spr.SetSprite(spr.GetSpriteIdByName("heart_full"));
			}else{
				spr.SetSprite(spr.GetSpriteIdByName("heart_empty"));
			}
		}
	}
}
