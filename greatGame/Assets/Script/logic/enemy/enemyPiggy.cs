using UnityEngine;
using System.Collections;

public class enemyPiggy : enemylogic  {


		bool enemy3Work;
		enemy_property enemySelf;
		enemyAniManager aniManager;
		GameObject ani;
		tk2dSpriteAnimator enemyAni;

		Vector3 playerPos;
		public float lockTargetTime;
		float lockTempTime;

		float deltaTime;

		GameObject player;

		Direction aniDir;

		float damageDeltaTime;

		void Awake(){
				
				player = constant.getPlayer ();
				playerPos = transform.position;	
				//自己的属性
				enemySelf = gameObject.GetComponent<enemy_property>();
				//动画管理器
				aniManager = gameObject.GetComponent<enemyAniManager>();
				//动画
				ani = transform.FindChild ("ui").FindChild ("AnimatedSprite").gameObject;

				enemyAni = ani.GetComponent<tk2dSpriteAnimator> ();
		}

		// Use this for initialization
		void Start () {
				enemy3Work = true;
				lockTempTime = lockTargetTime;
		}

		// Update is called once per frame
		void Update () {
				
		}

		void FixedUpdate () {
				if (enemy3Work) {
						deltaTime += Time.fixedDeltaTime;
						damageDeltaTime += Time.fixedDeltaTime;
						//变换位置
						lockTempTime += Time.fixedDeltaTime;

						if (lockTempTime >= lockTargetTime) {
								//Debug.Log ("随机移动");
								getAniDir ();
								lockTempTime = 0;
						}
				}

				if (playerPos == transform.position) {
						lockTempTime = lockTargetTime;
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


				Vector3 selfPos = this.transform.position;
				v =  playerPos - selfPos;

				//接近单位不移动
				if (Mathf.Abs (v.x) <= 0.8f && Mathf.Abs (v.y) <= 0.8f) {
						//非常接近主角 不再移动
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

		void lockTarget(){
				
				Vector3 pos;
				pos = this.transform.position;
				pos.x += Random.Range (-100, 100);
				pos.y += Random.Range (-100, 100);
				playerPos = pos;
				Debug.Log("锁定位置: " + playerPos);
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
								aniManager.playAni("run_left");


						} else {
								//Debug.Log ("左");
								aniDir = Direction.left;
								aniManager.setAniSide (aniDir);
								aniManager.playAni ("run_left");

						}
				} else {
						if (disY >= 0) {
								//Debug.Log ("上");
								aniDir = Direction.up;	
								aniManager.playAni ("run_up");
						} else {
								//Debug.Log ("下");
								aniDir = Direction.down;
								aniManager.playAni ("run_down");
						}
				}
		}


//-----------------------------------------------------------------------碰撞
//-----------------------------------------------------------------------碰撞
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
						hitWall (other.gameObject);
				}
				//撞到其他怪物
				if (other.collider.tag == "Enemy") {
						hitWall (other.gameObject);
				}
		}

		void hitPlayer(GameObject obj){
				constant.getMapLogic ().triggerEnter (obj, this.gameObject);
		}


		//
		void hitWall(GameObject wall){

				switch (aniDir) {
				default:
						break;

				case Direction.left:
						Debug.Log ("开始撞墙移动 右");
						int x1 = Random.Range (12, 16);
						int y1 = Random.Range (-2, 3);
						Vector3 moveRight = new Vector3 (x1, y1, 0);
						playerPos = enemySelf.transform.position + moveRight;
						break;
				case Direction.right:
						Debug.Log ("开始撞墙移动 左");
						int x2 = Random.Range (12, 16);
						int y2 = Random.Range (-2, 3);
						Vector3 moveLeft = new Vector3 (x2, y2, 0);
						playerPos = enemySelf.transform.position - moveLeft;
						break;
				case Direction.up:
						Debug.Log ("开始撞墙移动 下");
						int x3 = Random.Range (-2, 3);
						int y3 = Random.Range (12, 16);
						Vector3 moveDown = new Vector3 (x3, y3, 0);
						playerPos = enemySelf.transform.position - moveDown;
						break;
				case Direction.down:
						Debug.Log ("开始撞墙移动 上");
						int x4 = Random.Range (-2, 3);
						int y4 = Random.Range (12, 16);
						Vector3 moveUp = new Vector3 (x4, y4, 0);
						playerPos = enemySelf.transform.position + moveUp;
						break;

						break;
				}
				lockTempTime = lockTargetTime - 2f;

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
						int x1 = Random.Range (12, 16);
						int y1 = Random.Range (-2, 3);
						Vector3 moveRight = new Vector3 (x1, y1, 0);
						playerPos = enemySelf.transform.position + moveRight;
						break;
				case Direction.right:
						Debug.Log ("开始撞墙移动 左");
						int x2 = Random.Range (12, 16);
						int y2 = Random.Range (-2, 3);
						Vector3 moveLeft = new Vector3 (x2, y2, 0);
						playerPos = enemySelf.transform.position - moveLeft;
						break;
				case Direction.up:
						Debug.Log ("开始撞墙移动 下");
						int x3 = Random.Range (-2, 3);
						int y3 = Random.Range (12, 16);
						Vector3 moveDown = new Vector3 (x3, y3, 0);
						playerPos = enemySelf.transform.position - moveDown;
						break;
				case Direction.down:
						Debug.Log ("开始撞墙移动 上");
						int x4 = Random.Range (-2, 3);
						int y4 = Random.Range (12, 16);
						Vector3 moveUp = new Vector3 (x4, y4, 0);
						playerPos = enemySelf.transform.position + moveUp;
						break;

						break;
				}
				lockTempTime = lockTargetTime - 2f;

		}
}
