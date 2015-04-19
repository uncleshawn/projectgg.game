using UnityEngine;
using System.Collections;

public class enemy4logic : enemylogic {
		
		enemy_property enemySelf;
		enemyAniManager aniManager;
		GameObject player;
		GameObject ani;
		tk2dSpriteAnimator enemyAni;

		int zombieAi_originHp;
		int zombieAi_currentHp;
		bool zombieAi_canShot;
		enemyShotBullet shooter;

		//锁定主角位置
		Vector3 playerPos;

		//活动时间
		public float mIntervalTime = 6;
		//等待时间
		public float mWaitTime = 2;

		Direction aniDir;

		float deltaTime;
		public float aniDelay = 0.2f;
		float aniDelayDelta;

		void Awake(){
				//玩家的位置
				player = constant.getPlayer ();
				//自己的属性
				enemySelf = gameObject.GetComponent<enemy_property>();
				//动画管理器
				aniManager = gameObject.GetComponent<enemyAniManager>();
				//动画
				ani = transform.FindChild ("ui").FindChild ("AnimatedSprite").gameObject;	

				//动画脚本
				enemyAni = ani.GetComponent<tk2dSpriteAnimator> ();

				//射击脚本
				shooter = gameObject.GetComponent<enemyShotBullet> ();
				shooter.upgradeProperties (enemySelf);
				aniDir = Direction.down;
				deltaTime = Random.Range (0, mIntervalTime + mWaitTime);
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
				deltaTime += Time.fixedDeltaTime;
				aniDelayDelta += Time.fixedDeltaTime;
		}
				

		//获得玩家位置
		void lockTarget(){
				playerPos = player.transform.position;	
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
						aniShot ();
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
				lockTarget ();
				v = playerPos - selfPos;


				//播放走路动画
				playWalkAni (v);
				zombieAi_canShot = true;
				return v.normalized * add;
		}

		void getAniDir(){
				lockTarget ();
				Vector3 v = new Vector3 ();
				Vector3 selfPos = this.transform.position;
				v = playerPos - selfPos;
				float disX = v.x;
				float disY = v.y;	
				if (Mathf.Abs (disX) >= Mathf.Abs (disY)) {
						if (disX >= 0) {
								aniDir = Direction.right;

						} else {
								aniDir = Direction.left;
						}
				} else {
						if (disY >= 0) {
								aniDir = Direction.up;	
						} else {
								aniDir = Direction.down;
						}
				}
		}

		void playWalkAni(Vector3 dir){
				float disX = dir.x;
				float disY = dir.y;	
				//Debug.Log ("行走");
				if (Mathf.Abs (disX) >= Mathf.Abs (disY)) {
						if (disX >= 0) {
								//Debug.Log ("右");
								aniDir = Direction.right;
								aniManager.setAniSide (aniDir);
								aniManager.playSameAni ("run_left");


						} else {
								//Debug.Log ("左");
								aniDir = Direction.left;
								aniManager.setAniSide (aniDir);
								aniManager.playSameAni ("run_left");

						}
				} else {
						if (disY >= 0) {
								//Debug.Log ("上");
								aniDir = Direction.up;	
								aniManager.playSameAni ("run_up");
						} else {
								//Debug.Log ("下");
								aniDir = Direction.down;
								aniManager.playSameAni ("run_down");
						}
				}
		}
				

		public void aniShot(){
				if (zombieAi_canShot) {
						//Debug.Log ("射击");

						getAniDir ();
						if (aniDelayDelta >= aniDelay) {
								switch (aniDir) {
								default:
										break;
								case Direction.left:
										aniManager.setAniSide (aniDir);
										aniManager.playSameAni ("attack_left"); 

										break;
								case Direction.right:
										aniManager.setAniSide (aniDir);
										aniManager.playSameAni ("attack_left"); 
										break;
								case Direction.down:
										aniManager.playSameAni ("attack_down"); 
										break;
								case Direction.up:
										aniManager.playSameAni ("attack_up"); 
										break;

										break;	
								}
								aniDelayDelta = 0;
						}

						enemyAni.AnimationCompleted = AnimationCompleteDelegate;
						enemyAni.AnimationEventTriggered = AnimationEventDelegate;

				}

		}
		void AnimationCompleteDelegate(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip){
				aniWait ();
		}

		void AnimationEventDelegate(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip,  int frameNum)  {
				shotBullet ();
		}

		public void aniWait(){
				switch (aniDir) {
				default:
						break;
				case Direction.left:
						enemyAni.Play ("wait_left"); 
						aniManager.setAniSide (aniDir);
						break;
				case Direction.right:
						enemyAni.Play ("wait_left"); 
						aniManager.setAniSide (aniDir);
						break;
				case Direction.down:
						enemyAni.Play ("wait_down"); 
						aniManager.setAniSide (Direction.left);
						break;
				case Direction.up:
						enemyAni.Play ("wait_up"); 
						aniManager.setAniSide (Direction.left);
						break;

						break;	
				}
		}
				

		public void shotBullet(){
				if (zombieAi_canShot) {
						shooter.shootBullet (EnemyShotType.directPlayer);
						zombieAi_canShot = false;
				}
		}


		



}
