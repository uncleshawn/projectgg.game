//
// Enemy2logic.包含Enemy2蚯蚓逻辑的动画处理
//

using UnityEngine;
using System.Collections;

public class enemy2logic : enemylogic {

		enemy_property enemySelf;
		GameObject ani;
		string spaceSearcherPath;
		tk2dSpriteAnimator enemyAni;
		enemyShotBullet shooter;
		public int waitTime;

		//蚯蚓是否重新开始做AI行为
		bool completeMove;
		bool halfComplete;

		// Use this for initialization
		void Awake(){
				//自己的属性
				enemySelf = gameObject.GetComponent<enemy_property>();
				//动画控制
				ani = transform.FindChild ("ui").FindChild ("AnimatedSprite").gameObject;
				//寻路工具
				spaceSearcherPath = "Prefabs/logic/spaceSearch";
				//动画脚本
				enemyAni = ani.GetComponent<tk2dSpriteAnimator> ();
				//攻击射击脚本
				shooter = gameObject.GetComponent<enemyShotBullet> ();
				//判断是否开始重复行动
				completeMove = true;

				shooter.upgradeProperties (enemySelf);
		}

		// Use this for initialization

		void Start () {

		}

		// Update is called once per frame
		void FixedUpdate () {
				if (completeMove == true) {
						completeMove = false;
						attackMove ();
				}

		}



		void attackMove(){

				enemyAni.Play ("comeOut");
				enemyAni.AnimationCompleted = shotBullet;
		}

		void shotBullet(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip){
				enemyAni.Play ("attack");
				int notShoot = Random.Range (0, 4);
				if (notShoot != 0) {
						//shooter.shootBullet ();
						if (enemySelf.scared) {
								shooter.shootBullet (EnemyShotType.random);
						} else {
								//Debug.Log ("敌人射击");
								shooter.shootBullet (EnemyShotType.directPlayer);
						}

						StartCoroutine (waitMove (waitTime));
						defendMove ();

				} else {
						StartCoroutine (waitMove (1));
						defendMove ();
				}
		}

		IEnumerator waitMove(int waitTime){
				//Debug.Log ("敌人射击");
				if (enemyAni.IsPlaying ("wait")) {
						enemyAni.Play ("wait");
				}

				if (waitTime == 0) {
						waitTime = 3;
				}
				waitTime = Random.Range (waitTime-1, waitTime + 2);
				yield return new WaitForSeconds(waitTime);
		}

		void defendMove(){
				enemyAni.Play ("comeIn");
				enemyAni.AnimationCompleted = disappearAfter;

		}

		//隐藏地鼠图片 并 开始寻路
		void disappearAfter(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip){

				BoxCollider box = ani.transform.parent.parent.gameObject.GetComponent<BoxCollider> ();
				if (box) {
						box.isTrigger = true;
				} else {
						Debug.Log ("出错请检查");
				}
				ani.GetComponent<MeshRenderer> ().enabled = false;
				GameObject searcherClone = (GameObject)Instantiate (Resources.Load (spaceSearcherPath), this.transform.position, Quaternion.identity);
				enemySelf.invincible = true;
				searcherClone.GetComponent<spaceSearcherlogic> ().startWork (this.gameObject);
				//然后spaceSearch会调本object的moveNearByPos
				//moveNearByPos ();

		}


		public void moveNearByPos(Vector3 pos){
				enemySelf.invincible = true;
				iTween.MoveTo(gameObject, iTween.Hash("position", pos,  "easeType", "linear", "loopType", "none" , "time" , 2 , "oncomplete" , "finishMove" , "oncompletetarget" , this.gameObject ));
		}

		void finishMove(){
				enemySelf.invincible = false;
				BoxCollider box =  ani.transform.parent.parent.gameObject.GetComponent<BoxCollider> ();
				box.isTrigger = false;
				ani.GetComponent<MeshRenderer> ().enabled = true;
				completeMove = true;
		}


		void OnTriggerEnter(Collider other){
		}
}
