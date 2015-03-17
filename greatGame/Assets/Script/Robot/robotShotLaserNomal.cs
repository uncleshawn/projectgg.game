using UnityEngine;
using System.Collections;

public class robotShotLaserNomal : MonoBehaviour {
		//子弹类型
		weaponType weapontype;
		string laserPath; 
		float laserRate; 					//laser shoot rate, rateTime > laser fullAni time
		float delayTime;	


		public float preTime;
		public float flyingTime;
		public float coldDown;
		float mcoldDown;

		public int laserDamage;
		int mbulletDamage;

		public float mdamageRate; 
		public int mknockBack;


		//----bulletSpeStruct里包括以下：
		//---是否穿透攻击
		//---子弹元素属性
		bulletSpeStruct bulletSpe;


		Direction bulletDirection;

		void Awake(){
				coldDown = 1;
				weapontype = weaponType.laserNormal;
				laserRate = preTime + flyingTime + mcoldDown;
				laserPath = "Prefabs/bullets/normalLaser";

		}

		// Use this for initialization
		void Start () {
				delayTime = laserRate;
				bulletSpe.element = ElementType.normal;
				bulletSpe.knockType = KnockType.none;
				bulletSpe.pierceBullet = false;

		}
		// Update is called once per frame
		void FixedUpdate () {
				delayTime+=Time.deltaTime;
				if( Mathf.Abs(Input.GetAxis("shotHorizontal")) == 1 ||  Mathf.Abs(Input.GetAxis("shotVertical")) == 1  ){
						if(delayTime>laserRate)
						{
								delayTime = 0;
								getBulletDirection();
								shootLaser();
						}
				}
		}

		void shootLaser(){

				GameObject laserClone = (GameObject)Instantiate(Resources.Load(laserPath),transform.position,Quaternion.identity);
				laserClone.GetComponent<laserAniManager>().setShooter(this.transform.parent.gameObject,preTime,flyingTime);

				//SET BULLET PROPERTY
				setBulletProperty(laserClone);
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




		public void setEnabled(select_name_bool other){
				if(other.name == this.GetType().ToString() ){
						this.enabled = other.choose;
				}
		}
		public void disableAll(){
				this.enabled = false;
		}

		//UPGRADE SHOOTER PROPERTY FROM CHARACTER PROPERTY 更新武器的属性 当人物属性变化时
		public void upgradeProperties(char_property property){
				mcoldDown = coldDown - property.AttackRate*0.1f;
				laserRate = preTime + flyingTime + mcoldDown;
				mbulletDamage = laserDamage + property.Damage*5;

		}

		public void upgradeFollowProperties(follower_property property){
				mcoldDown = coldDown - property.AttackRate*0.1f;
				laserRate = preTime + flyingTime + mcoldDown;
				mbulletDamage = laserDamage + property.Damage*5;
		}


		//SEND PROPERTY TO BULLETS
		public void setBulletProperty(GameObject bulletClone){
				if(mdamageRate == 0) {mdamageRate = 0.5f;}
				bulletClone.GetComponent<bullet_property>().setProperty(weapontype ,mbulletDamage,mknockBack,mdamageRate, bulletSpe , constant.getBattleType(this.gameObject));
		}


		//捡到道具后更新属性
		public void setBaseProperty(weaponItem_property weapon){
				if (weapon.mType == weaponType.laserNormal) {
						laserDamage = weapon.baseBulletDamage;
						mknockBack = weapon.mknockBack;
						if (weapon.baseBulletRate != 0) {
								mcoldDown = weapon.baseBulletRate;
								laserRate = preTime + flyingTime + mcoldDown;
						}
						if (weapon.baseDamageRate != 0) {
								mdamageRate = weapon.baseDamageRate;
						}
						bulletSpe = weapon.bulletSpe;
						Debug.Log ("激光武器更新 基础属性" + "\r\n" +
								"激光伤害: " + laserDamage + "\r\n" +
								"伤害间隔: " + mdamageRate + "\r\n" +
								"激光冷却: " + laserRate + "\r\n" +
								"击退效果: " + mknockBack + "\r\n" +
								"----------特殊效果:--------: " + "\r\n" +
								"穿透性: " + bulletSpe.pierceBullet + "\r\n" +
								"恐惧效果: " + bulletSpe.scaredBullet.scaredEffect + "\r\n" +
								"元素类型: " + bulletSpe.element); 
				}

		}


		//修改bullet的special属性
		public void setBulletSpecial(bulletSpeStruct bullet){
				bulletSpe = bullet;
		}

		public void setScaredBullet(ScaredBullet scaredBullet){
				bulletSpe.scaredBullet = scaredBullet;
				Debug.Log ("子弹特效更新:" + "\r\n" +
						"恐惧属性: " + bulletSpe.scaredBullet.scaredEffect + "\r\n" +
						"恐惧概率: " + bulletSpe.scaredBullet.sacredPercent);
		}
}
