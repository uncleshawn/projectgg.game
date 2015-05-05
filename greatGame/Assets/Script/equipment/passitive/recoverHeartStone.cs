using UnityEngine;
using System.Collections;


//恢复之石 在间隔一段时间后恢复一定量的生命值


public class recoverHeartStone : MonoBehaviour {

		public bool startWork;

		public float recoverTime;
		public int recoverNum;
		float mRecoverTime;
		float tempTimer;

		public string explain;
		char_property player;

		// Use this for initialization
		void Awake(){
				recoverTime = 40;
				mRecoverTime = recoverTime + Random.Range (-5, 6);
				//Debug.Log ("恢复之心下次恢复时间: " + mRecoverTime);
				tempTimer = 0;
				player = gameObject.GetComponentInParent<char_property>();
				if (player) {
						Debug.Log ("恢复之心开始启动");
				} else {
						Debug.Log ("恢复之心不在玩家身上");
				}


		}
		void Start () {

		}

		// Update is called once per frame
		void Update () {
				
		}

		void FixedUpdate(){
				tempTimer += Time.deltaTime;
				//Debug.Log (tempTimer + " ==> " + recoverTime);
				//Debug.Log("tempTimer:recoverTime" + tempTimer + " : " + recoverTime );
				if (tempTimer >= mRecoverTime) {
						recoverHp ();
                                                //constant.getSoundLogic ().playEffect ();
						mRecoverTime = recoverTime + Random.Range (-5, 6);
						//Debug.Log ("恢复之心下次恢复时间: " + mRecoverTime);
						tempTimer = 0;
				}
		}

		void recoverHp(){
				//playEffectAni ();
				if (player.Hp < player.MaxHp) {
						player.Hp = player.Hp + recoverNum;
						playEffectAni ();
				}


		}

		void playEffectAni(){
				GameObject obj = player.gameObject;
				string effectPath = "Prefabs/aniEffect/effect_item";
				string effectName = "item60";
				//string aniLibPath = "Assets/Sprites/animation/item/itemEffectAni.prefab";
				//Vector3 pos = new Vector3 (0, 0, 0);
				constant.getMapLogic ().onceEffectAni (obj, effectPath , effectName);

		}

		void intPassitiveSkill(speItem_property item){
				if (startWork == false) {
						explain = "恢复之心:在间隔时间内补充血量";
						recoverNum = item.parameterInt;
						Debug.Log (explain + recoverNum);
						startWork = true;
				}


		}
}
