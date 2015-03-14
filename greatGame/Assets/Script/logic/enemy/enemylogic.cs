using UnityEngine;
using System.Collections;

public class enemylogic : monsterbaselogic {


		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		override public void beAttack(GameObject obj){
				if (obj.tag.Equals ("Bullet")) {
						//Debug.Log("enemy be attacked by bullet.");
						bullet_property bulletProperty = obj.GetComponent<bullet_property>();
						enemy_property enemyProperty = gameObject.GetComponent<enemy_property>();
						getDamage(enemyProperty,bulletProperty);
						if (bulletProperty.bulletknock != 0) {
								Debug.Log (gameObject.name + "被击退.武器类型为:");
								getKnockBack (enemyProperty, bulletProperty);
						}

						if(isDie()){
								GameObject.Destroy(this.gameObject);
								constant.getMapLogic().checkOpenDoor();
						}
				}
		}


		public void getDamage(enemy_property enemyProperty,bullet_property bulletProperty){
				//Debug.Log("enemy get damage: " + bulletProperty.bulletDamage);
				enemyProperty.Hp = enemyProperty.Hp - bulletProperty.bulletDamage;

				Vector3 objectPos = this.transform.position;

				//伤害显示------------

				//调整伤害输出的位置
				objectPos.y += 1;
				objectPos.z = -5;

				string showDamagePath = "Prefabs/ui/UI_showDamage";
				GameObject showDamageClone = (GameObject)Instantiate(Resources.Load(showDamagePath),objectPos,Quaternion.identity);
				if(showDamageClone){

						string damage = "-" + bulletProperty.bulletDamage.ToString();
						showDamageClone.GetComponent<enemy_showDamage>().mNum = damage;
						showDamageClone.GetComponent<enemy_showDamage>().showDamage();
				}

				//伤害显示------------
		}

		public void getKnockBack(enemy_property enemyProperty,bullet_property bulletProperty){
				//子弹的类型
				weaponType weapontype = bulletProperty.WeaponType;

				//击退的力度
				int force = bulletProperty.bulletknock;

				//敌人是否有霸体
				if (enemyProperty.heavyBody) {
						return;
				}

				//击退的类型
				//击退效果分类
				//1:普通击退
				//2:爆炸击退
				//3:

				KnockType knockType = bulletProperty.bulletSpe.knockType;

				//处理laser特殊击退效果
				if (weapontype == weaponType.laserNormal) {
						Debug.Log ("激光击退效果: " + knockType);
						switch (knockType) {
						default:
								break;
						case KnockType.normal:
								normalKnockBack (bulletProperty, force);
								break;
						case KnockType.explode:
								explodeKnockBack (enemyProperty, bulletProperty, force);
								break;
						}


				}
				if (weapontype == weaponType.bulletNormal) {
						Debug.Log ("子弹击退效果 " + knockType);
						switch (knockType) {
						default:
								break;
						case KnockType.normal:
								normalKnockBack (bulletProperty, force);
								break;
						case KnockType.explode:
								explodeKnockBack (enemyProperty, bulletProperty, force);
								break;
						}
				}


		}


		//普通击退效果
		void normalKnockBack(bullet_property bulletProperty,int force){
				Direction bulletDirection = bulletProperty.gameObject.GetComponent<bulletAniManager> ().bulletDirection;
				switch(bulletDirection){
				default:
						break;
				case Direction.up:
						this.gameObject.rigidbody.AddForce (Vector3.up * force);		
						break;
				case Direction.down:
						this.gameObject.rigidbody.AddForce (-1 * Vector3.up * force);	
						break;
				case Direction.left:
						this.gameObject.rigidbody.AddForce (-1 * Vector3.right * force);
						break;
				case Direction.right:
						this.gameObject.rigidbody.AddForce (Vector3.right * force);		
						break;

						break;
				}	
		}

		//爆炸击退效果
		void explodeKnockBack(enemy_property enemyProperty,bullet_property bulletProperty,int force){

				//敌人位置
				Vector3 enemyPos = enemyProperty.transform.position;
				//子弹位置
				Vector3 bulletPos = bulletProperty.transform.position;		
				Vector3 bulletSpeed = bulletProperty.transform.rigidbody.velocity;
				if (bulletSpeed.x!=0 || bulletSpeed.y!=0) {
					this.gameObject.rigidbody.AddForce (new Vector3 (enemyPos.x - bulletPos.x, enemyPos.y - bulletPos.y, 0) * force);	
				}
		}

		public void getEffect(enemy_property enemyProperty){

		}

		public bool isDie(){
				enemy_property enemyProperty = gameObject.GetComponent<enemy_property>();
				if (enemyProperty.Hp <= 0) {
						return true;
				}
				return false;
		}


}
