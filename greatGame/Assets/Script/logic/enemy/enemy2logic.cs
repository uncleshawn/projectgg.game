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
		public float waitTime;

		//蚯蚓是否重新开始做AI行为
		bool completeMove;

		// Use this for initialization
		void Awake(){
				//自己的属性
				enemySelf = gameObject.GetComponent<enemy_property>();
				//动画控制
				ani = transform.FindChild ("ui").FindChild ("ani").gameObject;
				//寻路工具
				spaceSearcherPath = "Prefabs/logic/spaceSearch";
				//动画脚本
				enemyAni = ani.GetComponent<tk2dSpriteAnimator> ();
				//攻击射击脚本
				shooter = gameObject.GetComponent<enemyShotBullet> ();
				//判断是否开始重复行动
				completeMove = true;
		}

		// Use this for initialization

		void Start () {
				shooter.upgradeProperties (enemySelf);
		}

		// Update is called once per frame
		void Update () {
				if (completeMove == true) {
						completeMove = false;
						attackMove ();
				}

		}

	

		void attackMove(){
				enemyAni.Play ("comeOut");
				enemyAni.AnimationCompleted = shootBullet;
		}

		void shootBullet(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip){
				enemyAni.Play ("attack");
				int notShoot = Random.Range (0, 4);
				if (notShoot != 0) {
						//shooter.shootBullet ();
						shooter.shootBullet ();
						StartCoroutine (waitMove (waitTime));
				} else {
						StartCoroutine (waitMove (0.25f));
				}
		}

		IEnumerator waitMove(float waitTime){
				enemyAni.Play ("wait");
				if (waitTime == 0) {
						waitTime = 3;
				}
				yield return new WaitForSeconds(waitTime);
				defendMove ();
		}

		void defendMove(){
				enemyAni.Play ("comeIn");
				enemyAni.AnimationCompleted = disappearAfter;

		}

		//隐藏地鼠图片
		void disappearAfter(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip){
	
				ani.GetComponent<MeshRenderer> ().enabled = false;
				BoxCollider box = ani.transform.parent.parent.gameObject.GetComponent<BoxCollider> ();
				if(box){
					box.isTrigger = true;
				} else{
						Debug.Log("出错请检查");
				}
				GameObject searcherClone = (GameObject)Instantiate(Resources.Load(spaceSearcherPath),this.transform.position,Quaternion.identity);
				searcherClone.GetComponent<spaceSearcherlogic> ().startWork (this.gameObject);
				//然后spaceSearch会调本object的moveNearByPos
				//moveNearByPos ();

		}


		public void moveNearByPos(Vector3 pos){
				
				iTween.MoveTo(gameObject, iTween.Hash("position", pos,  "easeType", "linear", "loopType", "none" , "time" , 3 , "oncomplete" , "finishMove" , "oncompletetarget" , this.gameObject ));
		}

		void finishMove(){
				BoxCollider box =  ani.transform.parent.parent.gameObject.GetComponent<BoxCollider> ();
				box.isTrigger = false;
				ani.GetComponent<MeshRenderer> ().enabled = true;
				completeMove = true;
		}


		override public void scaredMove(){
				//iTween.MoveBy(gameObject, iTween.Hash("y", Random.Range(-10,11),  "x" , Random.Range(-10,11),  "easeType", "easeInOutQuad", "loopType", "none" , "time" , 3 , "oncomplete" , "finishMove" , "oncompletetarget" , this.gameObject ));
		}

		void OnTriggerEnter(Collider other){
//				BoxCollider box =  ani.transform.parent.parent.gameObject.GetComponent<BoxCollider> ();
//				box.isTrigger = false;
//				Debug.Log ("蚯蚓遇到碰撞: " + .name );
//				iTween.Stop ();
//				finishMove ();
		}
}
