using UnityEngine;
using System.Collections;


//恢复之石 在间隔一段时间后恢复一定量的生命值


public class recoverHeartStone : MonoBehaviour {

		public float recoverTime;
		float mRecoverTime;
		float tempTimer;

		public string explain;
		char_property player;

		// Use this for initialization
		void Awake(){
				recoverTime = 10;
				mRecoverTime = recoverTime + Random.Range (0, 10);
				explain = "恢复之心:在间隔时间内补充血量";
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
				if (tempTimer >= recoverTime) {
						recoverHp ();
						constant.getSoundLogic ().playEffect ();
						mRecoverTime = recoverTime + Random.Range (0, 10);
						tempTimer = 0;
				}
		}

		void recoverHp(){
				
				if (player.Hp < player.MaxHp) {
						player.Hp = player.Hp + 1;
				}
		}
}
