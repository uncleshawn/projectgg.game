using UnityEngine;
using System.Collections;

public class enemy3logic : enemylogic {

		enemy_property enemySelf;

		string spaceSearcherPath;
		GameObject ani;
		tk2dSpriteAnimator enemyAni;
		enemyShotBullet shooter;



		//等下来攻击等待时间
		public float attackWaitTime;

		//锁定主角位置的时间
		public float lockTargetTime;
		float lockTempTime;
		Vector3 playerPos;

		float attackTempTime;
		bool completeMove;

		float deltaTime;
		//活动时间
		float mIntervalTime = 2;
		//等待时间
		float mWaitTime = 2;

		GameObject player;


		// Use this for initialization
		void Awake(){

				//玩家的位置
				player = constant.getPlayer ();
				playerPos = player.transform.position;	
				//自己的属性
				enemySelf = gameObject.GetComponent<enemy_property>();



				spaceSearcherPath = "Prefabs/logic/spaceSearch";


				//动画
				ani = transform.FindChild ("ui").FindChild ("enemySprite").gameObject;	
				//寻路工具

				//动画脚本
				enemyAni = ani.GetComponent<tk2dSpriteAnimator> ();
				//攻击射击
				shooter = gameObject.GetComponent<enemyShotBullet> ();
				shooter.upgradeProperties (enemySelf);
				//判断是否开始重复行动
				completeMove = true;


				attackTempTime = 0;
				lockTempTime = 0;
		}

		void Start () {
			

		}

		// Update is called once per frame
		void FixedUpdate () {
				deltaTime +=Time.fixedDeltaTime;
				lockTempTime += Time.fixedDeltaTime;
				if (lockTempTime >= lockTargetTime) {
						lockTarget ();
						lockTempTime = 0;
				}
				//Debug.Log ("deltaTime:mWaitTime= " + deltaTime + ":" + mWaitTime);
				//deltaTime = deltaTime % (mIntervalTime + mWaitTime);
				if (deltaTime > mWaitTime) {
						attackTempTime += Time.fixedDeltaTime;
						//Debug.Log ("attackTempTime:attackWaitTime= " + attackTempTime + ":" + attackWaitTime);
						if (attackTempTime > attackWaitTime) {
								
								stoneAttack ();
								attackTempTime = 0;

						}

				}
				if (deltaTime > mWaitTime+mIntervalTime) {
						deltaTime = 0;
				}

		}

		//获得玩家位置
		void lockTarget(){
				playerPos = player.transform.position;	
		}

		//射出3个石头
		void stoneAttack(){
				shooter.shootMultiBullets (EnemyShotType.directDiverging, 3 , 25);
		}



//		override public Vector3 getMoveAcc(){
//				Vector3 v = new Vector3 ();
//				//Debug.Log ("bat getMoveAcc");
//
//				Vector3 pos = playerPos;
//
//				if (player == null) {
//						return v;
//				}
//
////				//attackTempTime = 0;
////				deltaTime = deltaTime % (mIntervalTime + mWaitTime);
////				if (deltaTime <= mWaitTime) {
////						attackTempTime += Time.fixedDeltaTime;
////						if (attackTempTime > attackWaitTime) {
////								stoneAttack ();
////								attackTempTime = 0;
////						}
////						return v;
////				}
//
//				if (enemySelf.acting == false) {
//						return v;
//				}
//
//				if (enemySelf.scared == true) {
//						pos = scaredMovePos();
//				}
//
//				float add = 160;
//				Vector3 selfPos = this.transform.position;
//				if (selfPos.x > pos.x) {
//						v.x = -add;
//				} else if (selfPos.x < pos.x) {
//						v.x = add;
//				}
//				if (selfPos.y > pos.y) {
//						v.y = -add;
//				}else if (selfPos.y < pos.y) {
//						v.y = add;
//				}
//				return v;
//		}

		override public Vector3 scaredMovePos(){
				Vector3 pos;
				pos = this.transform.position;
				pos.x += Random.Range (-20, 20);
				pos.y += Random.Range (-20, 20);
				return pos;
		}

}
