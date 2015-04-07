using UnityEngine;
using System.Collections;

public class enemy4logic : enemylogic {
		
		enemy_property enemySelf;
		enemyAniManager enemyAniManager;
		GameObject player;
		GameObject ani;
		tk2dSpriteAnimator enemyAni;

		int zombieAi_originHp;
		int zombieAi_currentHp;
		bool zombieAi_canShot;
		enemyShotBullet shooter;

		//锁定主角位置的时间
		public float lockTargetTime;
		float lockTempTime;
		Vector3 playerPos;
		Direction zombieFace;

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

				//射击脚本
				shooter = gameObject.GetComponent<enemyShotBullet> ();
				shooter.upgradeProperties (enemySelf);

				lockTempTime = 0;
		}



		void Start () {
				lockTarget ();
				zombieAi_originHp = enemySelf.Hp;
				zombieAi_currentHp = enemySelf.Hp;
				zombieAi_canShot = false;
		}



		// Update is called once per frame
		void FixedUpdate () {
				stateFixedUpdate ();
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
						zombieFace = Direction.right;
				}
				if(aniSide.x < 0){
						enemyAniManager.setAniSide (Direction.left);
						zombieFace = Direction.left;
				}


		}

		override public Vector3 getMoveAcc(){
				Vector3 v = new Vector3 ();
				//Debug.Log ("bat getMoveAcc");
				Vector3 pos = playerPos;

				if (player == null) {
						return v;
				}

				//行走时间结束
				deltaTime = deltaTime % (mIntervalTime + mWaitTime);
				if (deltaTime <= mWaitTime) {
						zombieShot ();
						return v;

				}

				if (enemySelf.acting == false) {
						return v;
				}

				if (enemySelf.scared == true) {
						return v;
				}

				float add = 160;
				Vector3 selfPos = this.transform.position;
				v = playerPos - selfPos;
				//播放走路动画
				aniWalk ();
				zombieAi_canShot = true;
				return v.normalized * add;
		}


		public void aniShot(){
				if (enemyAni.IsPlaying ("run")) {
						
						enemyAni.Play ("shot");
						enemyAni.AnimationCompleted = shotAniFinsh;
				}
		}
		void shotAniFinsh(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip){
				if (clip.name == "shot") {
						aniWait ();
				} else {
						Debug.Log ("僵尸动画逻辑错误");
				}

		}
		public void aniWalk(){
				if (!enemyAni.IsPlaying ("run")) {
						//Debug.Log ("僵尸行走");
						enemyAni.Play ("run");
				}
				//Debug.Log ("僵尸不能行走:isPlaying?run" + enemyAni.IsPlaying ("run"));
		}
		public void aniWait(){
				if (!enemyAni.IsPlaying ("wait")) {
						enemyAni.Play ("wait");
				}
		}

		public void zombieShot(){
				aniShot ();
				if (enemyAni.Playing) {
						if (enemyAni.CurrentFrame == 8 && enemyAni.IsPlaying ("shot") && zombieAi_canShot) {
								shotBullet ();
								zombieAi_canShot = false;
						}
				} else {
						aniWait ();
				}
		}

		public void shotBullet(){
				shooter.shootBullet (EnemyShotType.directPlayer);	
		}


		



}
