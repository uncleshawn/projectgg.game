using UnityEngine;
using System.Collections;

public class enemy3logic : enemylogic {

		bool enemy3Work;

		enemy_property enemySelf;
		enemyAniManager aniManager;
		GameObject ani;
		tk2dSpriteAnimator enemyAni;
	
	
		//锁定主角位置的时间
		Vector3 playerPos;
		public float lockTargetTime;
		float lockTempTime;

		float deltaTime;

		GameObject player;

		Direction aniDir;

		int zombieAi_originHp;
		int zombieAi_currentHp;

		string aniName;
		bool enemyCrawl;

		float damageDeltaTime;
		// Use this for initialization
		void Awake(){

				//玩家的位置
				player = constant.getPlayer ();
				playerPos = transform.position;	
				//自己的属性
				enemySelf = gameObject.GetComponent<enemy_property>();
				//动画管理器
				aniManager = gameObject.GetComponent<enemyAniManager>();
				//动画
				ani = transform.FindChild ("ui").FindChild ("AnimatedSprite").gameObject;	
				//寻路工具

				//动画脚本
				enemyAni = ani.GetComponent<tk2dSpriteAnimator> ();

				aniName = "walk_";

				lockTempTime = 10;
				
		}

		void Start () {
				enemy3Work = true;
		}

		// Update is called once per frame
		void FixedUpdate () {
				deltaTime += Time.fixedDeltaTime;
				damageDeltaTime += Time.fixedDeltaTime;
				//变换位置
				lockTempTime += Time.fixedDeltaTime;
				if (lockTempTime >= lockTargetTime && enemyCrawl == false) {
						//Debug.Log ("随机移动");
						getAniDir ();
						lockTempTime = 0;
				}

				//僵尸变形
				if (enemy3Work) {
						zombieAi_currentHp = enemySelf.Hp;
						zombieAi_originHp = enemySelf.MaxHp;
						if (zombieAi_currentHp <= zombieAi_originHp * 0.3f) {
								enemyTurn ();	
						}
				}

		}


		override public Vector3 getMoveAcc(){
				Vector3 v = new Vector3 ();
				//Debug.Log ("bat getMoveAcc");

				Vector3 pos = playerPos;

				if (player == null) {
						return v;
				}
						
				if (enemySelf.acting == false) {
						return v;
				}

				if (enemySelf.scared == true) {
						pos = scaredMovePos();
						return pos;
				}

				float add = 160;

				if (enemyCrawl) {
						getAniDir ();
				}

				Vector3 selfPos = this.transform.position;
				v =  playerPos - selfPos;

				if (Mathf.Abs (v.x) <= 0.8f && Mathf.Abs (v.y) <= 0.8f) {
						//非常接近主角 不再移动
						//Debug.Log("非常接近主角 不再移动");
						aniWait();
						return new Vector3 ();
				}

				playWalkAni (v);
				return v.normalized * add;
		}

		override public Vector3 scaredMovePos(){
				Vector3 pos;
				pos = this.transform.position;
				pos.x += Random.Range (-100, 100);
				pos.y += Random.Range (-100, 100);
				return pos;
		}

		//获得玩家位置
		void lockTarget(){
				//Debug.Log("锁定玩家");
				playerPos = player.transform.position;	
		}

		//随机位置
		void getRandomPos(){
				Vector3 pos;
				pos = this.transform.position;
				int x = Random.Range (-10, 10);
				int y = Random.Range (-10, 10);
				if (Mathf.Abs (x) >= Mathf.Abs (y)) {
						pos.x += x;	
				} else {
						pos.y += y;
				}

				playerPos = pos;
				//Debug.Log (pos);

		}

		void getAniDir(){
				if (!enemyCrawl) {
						getRandomPos ();
				} else {
						lockTarget ();
				}
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

		void playWalkAni(Vector3 dir){
				float disX = dir.x;
				float disY = dir.y;	
				//Debug.Log ("行走");
				if (Mathf.Abs (disX) >= Mathf.Abs (disY)) {
						if (disX >= 0) {
								//Debug.Log ("右");
								aniDir = Direction.right;
								aniManager.setAniSide (aniDir);
								aniManager.playSameAni (aniName + "up");


						} else {
								//Debug.Log ("左");
								aniDir = Direction.left;
								aniManager.setAniSide (aniDir);
								aniManager.playSameAni (aniName + "up");

						}
				} else {
						if (disY >= 0) {
								//Debug.Log ("上");
								aniDir = Direction.up;	
								aniManager.playSameAni (aniName + "up");
						} else {
								//Debug.Log ("下");
								aniDir = Direction.down;
								aniManager.playSameAni (aniName + "up");
						}
				}
		}

		void enemyTurn(){
				enemyCrawl = true;
				aniName = "crawl_";
				if (!enemySelf.slowDown) {
						enemySelf.upgradeMoveSpeed (3);
				}
		}

		void hitPlayer(GameObject obj){
				constant.getMapLogic ().triggerEnter (obj, this.gameObject);
		}

		void hitWall(GameObject wall){
				Direction wallDir;
				Vector3 wallPos = wall.transform.position;
				Debug.Log ("墙体位置: " + wallPos);
				Vector3 selfPos = transform.position;
				if (Mathf.Abs (wallPos.x - selfPos.x) >= Mathf.Abs (wallPos.y - selfPos.y)) {
						if (selfPos.x >= wallPos.x) {
								wallDir = Direction.left;	
						} else {
								wallDir = Direction.right;	
						}
				} else {
						if (selfPos.y >= wallPos.y) {
								wallDir = Direction.down;	
						} else {
								wallDir = Direction.up;	
						}
				}

				switch (aniDir) {
				default:
						break;

				case Direction.left:
						Debug.Log ("开始撞墙移动 右");
						int x1 = Random.Range (2, 8);
						Vector3 moveRight = new Vector3 (x1, 0, 0);
						playerPos = enemySelf.transform.position + moveRight;
						break;
				case Direction.right:
						Debug.Log ("开始撞墙移动 左");
						int x2 = Random.Range (2, 8);
						Vector3 moveLeft = new Vector3 (x2, 0, 0);
						playerPos = enemySelf.transform.position - moveLeft;
						break;
				case Direction.up:
						Debug.Log ("开始撞墙移动 下");
						int y1 = Random.Range (2, 8);
						Vector3 moveDown = new Vector3 (0, y1, 0);
						playerPos = enemySelf.transform.position - moveDown;
						break;
				case Direction.down:
						Debug.Log ("开始撞墙移动 上");
						int y2 = Random.Range (2, 8);
						Vector3 moveUp = new Vector3 (0, y2, 0);
						playerPos = enemySelf.transform.position + moveUp;
						break;

						break;
				}
				lockTempTime = 0;

		}

		void hitEnemy(GameObject enemy){
				Direction wallDir;
				Vector3 wallPos = enemy.transform.position;
				Debug.Log ("墙体位置: " + wallPos);
				Vector3 selfPos = transform.position;
				if (Mathf.Abs (wallPos.x - selfPos.x) >= Mathf.Abs (wallPos.y - selfPos.y)) {
						if (selfPos.x >= wallPos.x) {
								wallDir = Direction.left;	
						} else {
								wallDir = Direction.right;	
						}
				} else {
						if (selfPos.y >= wallPos.y) {
								wallDir = Direction.down;	
						} else {
								wallDir = Direction.up;	
						}
				}

				switch (wallDir) {
				default:
						break;

				case Direction.left:
						Debug.Log ("开始撞墙移动 右");
						int x1 = Random.Range (1, 6);
						Vector3 moveRight = new Vector3 (x1, 0, 0);
						playerPos = enemySelf.transform.position + moveRight;
						break;
				case Direction.right:
						Debug.Log ("开始撞墙移动 左");
						int x2 = Random.Range (1, 6);
						Vector3 moveLeft = new Vector3 (x2, 0, 0);
						playerPos = enemySelf.transform.position - moveLeft;
						break;
				case Direction.up:
						Debug.Log ("开始撞墙移动 下");
						int y1 = Random.Range (1, 6);
						Vector3 moveDown = new Vector3 (0, y1, 0);
						playerPos = enemySelf.transform.position - moveDown;
						break;
				case Direction.down:
						Debug.Log ("开始撞墙移动 上");
						int y2 = Random.Range (1, 6);
						Vector3 moveUp = new Vector3 (0, y2, 0);
						playerPos = enemySelf.transform.position + moveUp;
						break;

						break;
				}
				lockTempTime = 0;

		}

		void OnCollisionStay(Collision other){
				
				//Debug.Log ("enemy3碰撞: " + other.collider.gameObject.tag);
				//撞到主角
				if (other.collider.gameObject.tag == "Player") {
						//Debug.Log ("碰到主角");
						if (damageDeltaTime >= 2) {

								hitPlayer (other.gameObject);
								damageDeltaTime = 0;
						}
				}



		}

		void OnCollisionEnter(Collision other){
				//撞到墙壁
				if (other.collider.gameObject.layer == 8 || other.collider.tag == "Wall" ) {
						//Debug.Log ("碰到墙壁");
						if (!enemyCrawl) {
								hitWall (other.gameObject);

						}
				}
				//撞到其他怪物
				if (other.collider.tag == "Enemy") {
						if (!enemyCrawl) {
								hitEnemy (other.gameObject);
						}
				}
		}


}
