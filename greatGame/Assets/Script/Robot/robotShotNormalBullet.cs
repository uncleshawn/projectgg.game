using UnityEngine;
using System.Collections;

public class robotShotNormalBullet : MonoBehaviour {


		string  bulletPath = "Prefabs/bullets/normalBullet";	//子弹prefab
		weaponType weapontype;

		bulletGetSpeed shotScript;				//子弹用script(设定速度)
		Direction bulletDirection;				//子弹的射出方向

		//子弹的属性
		public float baseBulletRate;			//子弹origin间隔时间
		float mbulletRate;
		public float baseBulletSpeed;				//子弹的origin速度
		float mbulletSpeed;
		public float baseBulletDistance;			//子弹的origin距离
		float mbulletDistance;
		public int baseBulletDamage;				//bullet damage
		int mbulletDamage;

		//子弹的击退效果
		public int mknockBack;
		public float mdamageRate;



		float delayTime=0;	

		GameObject player;						//获得玩家gameobject
		Vector3 playerVelocity;					//玩家的移动速度
		public float velocityTransfer;			//玩家给子弹的动量转化率

		public Vector3 shotPosition;			//射出子弹位置(不是同一个位置)
		public float posDevi = 0.1f;					//位置偏差值

		//射击的位置
		GameObject posUp;
		GameObject posDown;
		GameObject posLeft;
		GameObject posRight;


		public bulletSpeStruct bulletSpe;
		//bulletSpeStruct里包括以下：
		//bool pierceBullet 是否穿透攻击
		//ElementType 子弹元素属性


		// Use this for initialization
		void Awake(){
				weapontype = weaponType.bulletNormal;

		}
				
		void Start () {

				checkForget();
				bulletSpe.element = ElementType.normal;
				bulletSpe.knockType = KnockType.none;
				bulletSpe.pierceBullet = false;

				player = this.transform.parent.gameObject;
				mbulletRate = baseBulletRate;
				mbulletSpeed = baseBulletSpeed;
				mbulletDistance = baseBulletDistance;
				mbulletDamage = baseBulletDamage;

				posUp = transform.FindChild("up").gameObject;
				posDown = transform.FindChild("down").gameObject;
				posLeft = transform.FindChild("left").gameObject;
				posRight = transform.FindChild("right").gameObject;

		}

		// Update is called once per frame
		void FixedUpdate () {

				upgradeBulletProperties();

				//时间间隔
				delayTime+=Time.deltaTime;
				if( Mathf.Abs(Input.GetAxis("shotHorizontal")) == 1 ||  Mathf.Abs(Input.GetAxis("shotVertical")) == 1  ){
						if(delayTime>mbulletRate)
						{
								delayTime = 0;
								shootBullet();
						}
				}

		}


		public void checkForget(){
				if(baseBulletDamage==0){
						baseBulletDamage = 1;
				}
				if(baseBulletSpeed==0){
						baseBulletSpeed = 10;
				}
				if(baseBulletDistance==0){
						baseBulletDistance = 10;
				}
				if(baseBulletRate==0){
						baseBulletRate = 0.5f;
				}
		}

		public void upgradeBulletProperties(){

		}

		void shootBullet()
		{
				getBulletDirection();			//按键设定子弹的飞行方向
				setShotPosition();				//设置子弹的发射位置
				GameObject bulletClone = (GameObject)Instantiate(Resources.Load(bulletPath),shotPosition,Quaternion.identity);
				//set bullet end distance
				bulletClone.GetComponent<bulletCheckDistance>().setDistance(mbulletDistance);
				//子弹的damage + 速度 +方向
				setBulletProperty(bulletClone);
				shotScript = bulletClone.GetComponent<bulletGetSpeed>();
				setBulletSpeed(shotScript,mbulletSpeed);



		}



		void getBulletDirection()
		{
				if(Input.GetAxis ("shotHorizontal")==1)
				{
						bulletDirection = Direction.right;
				}		
				if(Input.GetAxis ("shotHorizontal")==-1)
				{
						bulletDirection = Direction.left;
				}
				if(Input.GetAxis ("shotVertical")==1)
				{
						bulletDirection = Direction.up;
				}
				if(Input.GetAxis ("shotVertical")==-1)
				{
						bulletDirection = Direction.down;
				}

		}

		void setBulletSpeed(bulletGetSpeed anyScript,float Speed){
				Vector3 speed = new Vector3(0,0,0);
				if(player.rigidbody){
						playerVelocity = player.rigidbody.velocity;
				}
				else{
						playerVelocity = new Vector3(0,0,0);
				}
				//Debug.Log("人物移动速度: " +playerVelocity );
				getBulletDirection();
				switch(bulletDirection) 
				{ 
				default: 
						break; 
				case Direction.up:	 	speed = new Vector3(0,Speed,0);		speed += new Vector3(playerVelocity.x,0,0)*velocityTransfer;
						break;
				case Direction.down:	speed = new Vector3(0,Speed*-1,0);	speed += new Vector3(playerVelocity.x,0,0)*velocityTransfer;	
						break;			
				case Direction.left: 	speed = new Vector3(Speed*-1,0,0); 	speed += new Vector3(0,playerVelocity.y,0)*velocityTransfer;
						break;				
				case Direction.right: 	speed = new Vector3(Speed,0,0);	 	speed += new Vector3(0,playerVelocity.y,0)*velocityTransfer;
						break;			

						break; 
				}
				anyScript.shotBullet(speed);
		}



		void setShotPosition(){
				switch(bulletDirection) 
				{ 
				default: 
						break; 
				case Direction.up:	 	shotPosition = posUp.transform.position;	
						break;
				case Direction.down:	shotPosition = posDown.transform.position;	
						break;			
				case Direction.left: 	shotPosition = posLeft.transform.position;	 	
						break;				
				case Direction.right: 	shotPosition = posRight.transform.position;	
						break;			

						break; 
				}
		}

		//only being used by other scripts
		public void setEnabled(select_name_bool other){
				if(other.name == this.GetType().ToString()  ){
						this.enabled = other.choose;
				}
		}
		//only being used by other scripts
		public void disableAll(){
				this.enabled = false;
		}



		//only being used by other scripts
		public void upgradeProperties(char_property property){
				mbulletSpeed = baseBulletSpeed + property.AttackSpeed;
				mbulletRate = baseBulletRate - (baseBulletRate-0.1f)*property.AttackRate/10;
				mbulletDistance = baseBulletDistance + property.AttackDistance;
				mbulletDamage = baseBulletDamage + property.Damage*5;

		}

		public void upgradeFollowProperties(follower_property property){
				mbulletSpeed = baseBulletSpeed + property.AttackSpeed;
				mbulletRate = baseBulletRate - (baseBulletRate-0.1f)*property.AttackRate/10;
				mbulletDistance = baseBulletDistance + property.AttackDistance;
				mbulletDamage = baseBulletDamage + property.Damage*5;
		}



		//using when shoot a bullet
		public void setBulletProperty(GameObject bulletClone){
				//如果忘记输入子弹射击频率,默认为0.5秒间隔
				if(mdamageRate == 0) {mdamageRate = 0.5f;}
				bulletClone.GetComponent<bullet_property>().setProperty(weapontype,mbulletDamage,mknockBack,mdamageRate,bulletSpe, constant.getBattleType(player));
		}


		//捡到道具后更新属性
		public void setBaseProperty(weaponItem_property weapon){
				if (weapon.mType == weaponType.bulletNormal) {
						baseBulletDamage = weapon.baseBulletDamage;
						if (weapon.baseBulletDistance != 0) {
								baseBulletDistance = weapon.baseBulletDistance;
						}
						if (weapon.baseBulletRate != 0) {
								baseBulletRate = weapon.baseBulletRate;
						}
						if (weapon.baseBulletSpeed != 0) {
								baseBulletSpeed = weapon.baseBulletSpeed;
						}
						mknockBack =	weapon.mknockBack;
						mdamageRate = weapon.baseDamageRate;
						bulletSpe = weapon.bulletSpe;
						Debug.Log ("子弹武器更新 基础属性" + "\r\n" +
								"子弹伤害: " + baseBulletDamage + "\r\n" +
								"子弹伤害间隔: " + mdamageRate + "\r\n" +
								"击退效果: " + mknockBack + "\r\n" +
								"距离: " + baseBulletDistance + "\r\n" +
								"间隔: " + baseBulletRate + "\r\n" +
								"速度: " + baseBulletSpeed + "\r\n" +
								"----------特殊效果:--------: " + "\r\n" +
								"穿透性: " + bulletSpe.pierceBullet + "\r\n" +
								"恐惧效果: " + bulletSpe.scaredBullet.scaredEffect + "\r\n" +
								"减速效果: " + bulletSpe.slowBullet.slowEffect + "\r\n" +
								"元素类型: " + bulletSpe.element); 
				}
		}


		//修改bullet的special属性
		public void setBulletSpecial(bulletSpeStruct bullet){
				bulletSpe = bullet;
		}
		public void setScaredBullet(ScaredBullet scaredBullet){
				bulletSpe.scaredBullet = scaredBullet;
				Debug.Log ( "子弹特效更新:" + "\r\n" +
				"恐惧属性: " + bulletSpe.scaredBullet.scaredEffect + "\r\n" +
				"恐惧概率: " + bulletSpe.scaredBullet.sacredPercent);
		}
		public void setSlowBullet(SlowBullet slowBullet){
				bulletSpe.slowBullet = slowBullet;
				Debug.Log ( "子弹特效更新:" + "\r\n" +
						"减速属性: " + bulletSpe.slowBullet.slowEffect + "\r\n" +
						"减速概率: " + bulletSpe.slowBullet.slowPercent + "\r\n" +
						"减速至百分比: " + bulletSpe.slowBullet.slowLevel
				);
		}
}


