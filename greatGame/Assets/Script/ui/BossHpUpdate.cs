using UnityEngine;
using System.Collections;

public class BossHpUpdate : MonoBehaviour {

	GameObject mBoss;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (mBoss == null) {
			GameObject[] objs = GameObject.FindGameObjectsWithTag(constant.TAG_ENEMY);
			foreach(GameObject obj in objs){
				enemy_property pro = obj.GetComponent<enemy_property>();
				if(pro.mIsBoss){
					mBoss = obj;
					break;
				}
			}
		}

		if (mBoss != null) {
			this.gameObject.SetActive(true);
				GameObject bossHpObj = GameObject.FindGameObjectWithTag ("BossHp");
				GameObject hpObj = constant.getChildGameObject (bossHpObj, "hp");
				tk2dClippedSprite spr = hpObj.GetComponent<tk2dClippedSprite> ();

				enemy_property pro = mBoss.GetComponent<enemy_property> ();
				float w = ((float)pro.Hp) / pro.MaxHp;
				Debug.Log ("hp:" + pro.Hp + "," + pro.MaxHp + "," + w);
				//spr.clipTopRight = new Vector2(0, w);
				spr.ClipRect = new Rect (0, 0, w, 1);
		} else {
			this.gameObject.SetActive(false);
		}
	}
}
