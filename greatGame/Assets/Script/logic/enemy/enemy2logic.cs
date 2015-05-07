//
// Enemy2logic.包含Enemy2蚯蚓逻辑的动画处理
//

using UnityEngine;
using System.Collections;

public class enemy2logic : enemylogic {

		enemy_property enemySelf;
		tk2dSpriteAnimator enemyAni;
		GameObject aniObject;
		enemyAniManager aniManager;
		string spaceSearcherPath;
		GameObject shadowObject;
		enemyShotBullet shooter;
		public int waitTime;

		//蚯蚓是否重新开始做AI行为
		bool completeMove;
		bool halfComplete;

		//动画方向
		Direction aniDir;

		// Use this for initialization
		void Awake(){
				//自己的属性
				enemySelf = gameObject.GetComponent<enemy_property>();
				//动画控制
				aniObject = transform.FindChild ("ui").FindChild ("AnimatedSprite").gameObject;
				//寻路工具
				spaceSearcherPath = "Prefabs/logic/spaceSearch";
				//动画脚本
				enemyAni = aniObject.GetComponent<tk2dSpriteAnimator> ();
				//enemyAniManager
				aniManager = GetComponent<enemyAniManager>();
				//攻击射击脚本
				shooter = gameObject.GetComponent<enemyShotBullet> ();
				//判断是否开始重复行动
				completeMove = true;

				shooter.upgradeProperties (enemySelf);
		}

		// Use this for initialization

		void Start () {
				shadowObject = aniManager.ShadowObject;
		}

		// Update is called once per frame
		void FixedUpdate () {
				if (completeMove == true) {
						completeMove = false;
						attackMove ();
				}

		}

		void getDirection(){
				Vector3 playerPos = constant.getPlayer ().transform.position;
				Vector3 dirPos = playerPos - transform.position;
				if (Mathf.Abs (dirPos.x) >= Mathf.Abs (dirPos.y)) {
						if (dirPos.x >= 0) {
								//Debug.Log ("右");
								aniDir = Direction.right;
								aniManager.setAniSide(aniDir);

						} else {
								//Debug.Log ("左");
								aniDir = Direction.left;
								aniManager.setAniSide(aniDir);

						}
				} else {
						if (dirPos.y >= 0) {
								//Debug.Log ("上");
								aniDir = Direction.up;	

						} else {
								//Debug.Log ("下");
								aniDir = Direction.down;
						}
				}
		}

		void attackMove(){
				getDirection ();
				switch (aniDir) {
				default:
						break;
				case Direction.left:
						enemyAni.Play ("comeOut_left");
						break;
				case Direction.right:
						enemyAni.Play ("comeOut_left");
						break;
				case Direction.down:
						enemyAni.Play ("comeOut_down");
						break;
				case Direction.up:
						enemyAni.Play ("comeOut_up");
						break;

						break;
				}
						
				enemyAni.AnimationEventTriggered = shotBullet;
				enemyAni.AnimationCompleted = enemyWait;
		}





		void comInMove(){
				switch (aniDir) {
				default:
						break;
				case Direction.left:
						enemyAni.Play ("comeIn_left");
						break;
				case Direction.right:
						enemyAni.Play ("comeIn_left");
						break;
				case Direction.down:
						enemyAni.Play ("comeIn_down");
						break;
				case Direction.up:
						enemyAni.Play ("comeIn_up");
						break;

						break;
				}

				enemyAni.AnimationCompleted = disappearAfter;

		}

		//隐藏地鼠图片 并 开始寻路
		void disappearAfter(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip){

				BoxCollider box = aniObject.transform.parent.parent.gameObject.GetComponent<BoxCollider> ();
				if (box) {
						box.enabled = false;
				} else {
						Debug.Log ("出错请检查");
				}
				shadowObject.GetComponent<MeshRenderer> ().enabled = false;
				aniObject.GetComponent<MeshRenderer> ().enabled = false;

				//生成位置搜索器
				GameObject searcherClone = (GameObject)Instantiate (Resources.Load (spaceSearcherPath), this.transform.position, Quaternion.identity);
				enemySelf.invincible = true;
				searcherClone.GetComponent<spaceSearcherlogic> ().startWork (this.gameObject);
				//然后spaceSearch会调本object的moveNearByPos
				//moveNearByPos ();

		}


		public void moveNearByPos(Vector3 pos){
				enemySelf.invincible = true;
				iTween.MoveTo(gameObject, iTween.Hash("position", pos,  "easeType", "linear", "loopType", "none" , "time" , 1 , "oncomplete" , "finishMove" , "oncompletetarget" , this.gameObject ));
		}

		void finishMove(){
				enemySelf.invincible = false;
				BoxCollider box =  aniObject.transform.parent.parent.gameObject.GetComponent<BoxCollider> ();
				box.enabled = true;
				shadowObject.GetComponent<MeshRenderer> ().enabled = true;
				aniObject.GetComponent<MeshRenderer> ().enabled = true;

				completeMove = true;
		}



		//完成动画
		void enemyWait(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip){
				StartCoroutine (waitMove (waitTime));

		}

		//wait
		IEnumerator waitMove(int waitTime){
				
				switch (aniDir) {
				default:
						break;
				case Direction.left:
						enemyAni.Play ("wait_left");
						break;
				case Direction.right:
						enemyAni.Play ("wait_left");
						break;
				case Direction.down:
						enemyAni.Play ("wait_down");
						break;
				case Direction.up:
						enemyAni.Play ("wait_up");
						break;

						break;
				}

				if (waitTime == 0) {
						waitTime = 2;
				}
				waitTime = Random.Range (waitTime-1, waitTime);
				yield return new WaitForSeconds(waitTime);
				comInMove ();
		}

		//中间时间
		void shotBullet(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip,  int frameNum)  {
				//shooter.shootBullet ();
				if (enemySelf.scared) {
						shooter.shootBullet (EnemyShotType.random);
				} else {
						//Debug.Log ("敌人射击");
						shooter.shootBullet (EnemyShotType.directPlayer);
				}
		}



		void OnTriggerEnter(Collider other){
		}
}
