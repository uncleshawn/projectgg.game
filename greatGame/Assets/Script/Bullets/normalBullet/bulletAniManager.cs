using UnityEngine;
using System.Collections;

public class bulletAniManager : MonoBehaviour {


		tk2dSprite bulletSprite;				//子弹的精灵图	
		tk2dSpriteAnimator bulletAni;			//子弹动画

		Direction bulletDirection;

		public Direction BulletDirection { get { return bulletDirection; } set { bulletDirection = value; }}

		bool bulletDie;
		// Use this for initialization
		void Awake(){
				bulletDie = false;
				bulletSprite = transform.FindChild("ui").FindChild("bulletPic").GetComponent<tk2dSprite>();
				bulletAni = transform.FindChild("ui").FindChild("bulletPic").GetComponent<tk2dSpriteAnimator>();
		}
		void Start () {
				if(bulletDie == false) {
						bulletDirection = getDirection();
						setBulletDireciton(bulletSprite,bulletAni,bulletDirection);
				}
		}

		// Update is called once per frame
		void Update () {

		}

		Direction getDirection() {
				Vector3 bulletSpeed = rigidbody.velocity;
				if( bulletSpeed.x > 0 && Mathf.Abs(bulletSpeed.x) >= Mathf.Abs(bulletSpeed.y) ){
						return Direction.right;
				}
				if( bulletSpeed.x < 0 && Mathf.Abs(bulletSpeed.x) >= Mathf.Abs(bulletSpeed.y) ){
						return Direction.left;
				}
				if( bulletSpeed.y > 0 && Mathf.Abs(bulletSpeed.y) > Mathf.Abs(bulletSpeed.x) ){
						return Direction.up;
				}
				if( bulletSpeed.y < 0 && Mathf.Abs(bulletSpeed.y) > Mathf.Abs(bulletSpeed.x) ){
						return Direction.down;
				}
				return Direction.left;

		}


		//子弹击中墙壁
		public void hitWall() {
				bulletStop();
				//Debug.Log ("collision: bullet hit wall.");
				destroyAfterAni("hit");
		}

		//子弹击中敌人
		public void hitEnemies() {

				//动画结束后子弹消失
				//Debug.Log ("bulletAnimation: bullet hit enemy.");
				if (checkBulletPierce ()) {
						bulletStop();
						destroyAfterAni ("hit");
				}
		}

		//子弹达到最远距离
		public void distanceEnd() {
				destroyAfterAni("end");
		}

		//子弹飞行过程
		public void flying()
		{
				bulletAni.Play("flying");
		}

		//设置子弹的方向
		void setBulletDireciton(tk2dSprite bulletSprite,tk2dSpriteAnimator bulletAni , Direction bulletDirection)
		{
				//Debug.Log ("setting bullet direction!: " + bulletDirection);
				switch(bulletDirection) 
				{ 
				default: 
						break; 
				case Direction.up:	 	setSpriteDirection(bulletSprite,bulletAni,0);		bulletSprite.scale = new Vector3(Mathf.Abs(bulletSprite.scale.x),bulletSprite.scale.y,bulletSprite.scale.z);	  break;	
				case Direction.down:	setSpriteDirection(bulletSprite,bulletAni,0);		bulletSprite.scale = new Vector3(Mathf.Abs(bulletSprite.scale.x)*-1,bulletSprite.scale.y,bulletSprite.scale.z);   break;	
				case Direction.left: 	setSpriteDirection(bulletSprite,bulletAni,1);		bulletSprite.scale = new Vector3(Mathf.Abs(bulletSprite.scale.x)*-1,bulletSprite.scale.y,bulletSprite.scale.z);	  break;		
				case Direction.right: 	setSpriteDirection(bulletSprite,bulletAni,1);		bulletSprite.scale = new Vector3(Mathf.Abs(bulletSprite.scale.x),bulletSprite.scale.y,bulletSprite.scale.z);	  break;	

						break; 
				} 

		}

		//实现子弹的方向
		void setSpriteDirection(tk2dSprite bulletSprite,tk2dSpriteAnimator bulletAni , int direction){
				if(direction == 1){
						//Debug.Log ("横向子弹");
						transform.rotation = Quaternion.identity;
				}
				else{
						//Debug.Log ("纵向子弹");
						transform.rotation = Quaternion.identity;
						transform.Rotate(new Vector3(0,0,90));
				}
				bulletSprite.SetSprite(bulletSprite.GetSpriteIdByName("flying"));
				flying();
		}



		public void afterAni(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip){
				GameObject.Destroy(this.gameObject);
		}

		public void destroyAfterAni(string aniName){
				if(bulletAni){
						bulletAni.Play(aniName);
						bulletAni.AnimationCompleted = afterAni;
				}
		}

		void bulletStop(){
				bulletDie = true;
				rigidbody.velocity = new Vector3(0,0,0);
		}

		bool checkBulletPierce(){
				GameObject bullet = this.gameObject;
				bullet_property bulletPro =  bullet.GetComponent<bullet_property> ();
				bool canPierce;
				if (bulletPro) {
						canPierce = bulletPro.bulletSpe.pierceBullet;
				} else {
						return false;
				}

				//如果子弹有穿透属性，则返回false 不做动画处理
				return !canPierce;

		}

		void OnTriggerEnter(Collider other){
				if(!bulletDie){
						if(other.gameObject.layer == 8){
								hitWall();
								return ;
						}
						enemy_property enemyPro = other.gameObject.GetComponent<enemy_property>();
						if(enemyPro){
								hitEnemies();
						}
				}
		}
}
