using UnityEngine;
using System.Collections;

public class enemy4logic : enemylogic {
		
		enemy_property enemySelf;
		enemyAniManager enemyAniManager;
		GameObject player;
		GameObject ani;
		tk2dSpriteAnimator enemyAni;

		//锁定主角位置的时间
		public float lockTargetTime;
		float lockTempTime;
		Vector3 playerPos;

		//活动时间
		public float mIntervalTime = 6;
		//等待时间
		public float mWaitTime = 0.5f;

		float deltaTime;

		void Awake(){
				//玩家的位置
				player = constant.getPlayer ();
				//自己的属性
				enemySelf = gameObject.GetComponent<enemy_property>();
				//动画管理器
				enemyAniManager = gameObject.GetComponent<enemyAniManager>();
				//动画
				ani = transform.FindChild ("ui").FindChild ("AnimatedSprite").gameObject;	

				//动画脚本
				enemyAni = ani.GetComponent<tk2dSpriteAnimator> ();


				lockTempTime = 0;
		}



		void Start () {
				lockTarget ();
		}



		// Update is called once per frame
		void FixedUpdate () {
				deltaTime +=Time.fixedDeltaTime;
				lockTempTime += Time.fixedDeltaTime;
				if (lockTempTime >= lockTargetTime) {
						lockTarget ();
						lockTempTime = 0;
				}
		}
				

		//获得玩家位置
		void lockTarget(){
				playerPos = player.transform.position;	
				Vector3 aniSide = playerPos - transform.position;
				if(aniSide.x >= 0){
						enemyAniManager.setAniSide (Direction.right);	
				}
				if(aniSide.x < 0){
						enemyAniManager.setAniSide (Direction.left);	
				}


		}

		override public Vector3 getMoveAcc(){
				Vector3 v = new Vector3 ();
				//Debug.Log ("bat getMoveAcc");
				Vector3 pos = playerPos;

				if (player == null) {
						return v;
				}

				deltaTime = deltaTime % (mIntervalTime + mWaitTime);
				if (deltaTime <= mWaitTime) {
						return v;
				}

				if (enemySelf.acting == false) {
						return v;
				}

				if (enemySelf.scared == true) {
						pos = scaredMovePos();
				}

				float add = 160;
				Vector3 selfPos = this.transform.position;
				v = playerPos - selfPos;
				return v.normalized * add;
		}



}
