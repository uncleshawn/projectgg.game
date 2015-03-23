using UnityEngine;
using System.Collections;

public class enemyShotBullet : MonoBehaviour {


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

		public bulletSpeStruct bulletSpe;

		float delayTime=0;	

		void Awake(){
				weapontype = weaponType.bulletNormal;

		}

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		public void shootBullet()
		{
				GameObject bulletClone = (GameObject)Instantiate(Resources.Load(bulletPath),this.transform.position,Quaternion.identity);
				//set bullet end distance
				bulletClone.GetComponent<bulletCheckDistance>().setDistance(mbulletDistance);
				//子弹的damage + 速度 +方向
				setBulletProperty(bulletClone);
				shotScript = bulletClone.GetComponent<bulletGetSpeed>();
				setBulletSpeed(shotScript,mbulletSpeed);



		}

		void setBulletSpeed(bulletGetSpeed anyScript,float Speed){
				Vector3 speed = new Vector3(0,0,0);
				Vector3 enemyPos = this.transform.position;
				Vector3 playerPos = GameObject.FindWithTag ("Player").transform.position;
				speed = playerPos - enemyPos;
				anyScript.shotBullet(speed);
		}

		public void setBulletProperty(GameObject bulletClone){
				//如果忘记输入子弹射击频率,默认为0.5秒间隔
				if(mdamageRate == 0) {mdamageRate = 0.5f;}
				bulletClone.GetComponent<bullet_property>().setProperty(weapontype,mbulletDamage,mknockBack,mdamageRate,bulletSpe, constant.getBattleType(this.gameObject));
		}

		void checkForget(){
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
}
