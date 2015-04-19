using UnityEngine;
using System.Collections;

public class Boss2Logic : enemylogic {

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
		public float mIntervalTime = 2;
		//等待时间
		public float mWaitTime = 2;

		GameObject player;

		int excurAngle;

		// Use this for initialization
		void Awake(){

				//玩家的位置
				player = constant.getPlayer ();
				playerPos = player.transform.position;	
				//自己的属性
				enemySelf = gameObject.GetComponent<enemy_property>();



				spaceSearcherPath = "Prefabs/logic/spaceSearch";


				//动画
				ani = transform.FindChild ("ui").FindChild ("AnimatedSprite").gameObject;	
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
						//lockTarget ();
						lockTempTime = 0;
				}

		}

		//获得玩家位置
		void lockTarget(){
				playerPos = player.transform.position;	
		}

		//射出3个石头
		void stoneAttack(){
				shooter.shootMultiBulletsOnePos (playerPos, 7, 15 , excurAngle);
		}



		override public Vector3 getMoveAcc(){
				deltaTime = deltaTime % (mIntervalTime + mWaitTime);

				if (deltaTime <= mWaitTime) {
						attackTempTime += Time.fixedDeltaTime;
						if (attackTempTime > attackWaitTime) {
								stoneAttack ();
								attackTempTime = 0;
						}
				} else {
						lockTarget ();
						excurAngle = Random.Range (-15, 16);
				}

				return new Vector3 (0, 0, 0);
		}

		override public Vector3 scaredMovePos(){
				return new Vector3 (0, 0, 0);
		}
}
