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
				objectPos.z = -5;

				string showDamagePath = "Prefabs/ui/UI_showDamage";
				GameObject showDamageClone = (GameObject)Instantiate(Resources.Load(showDamagePath),objectPos,Quaternion.identity);
				if(showDamageClone){

						string damage = "-" + bulletProperty.bulletDamage.ToString();
						showDamageClone.GetComponent<enemy_showDamage>().mNum = damage;
						showDamageClone.GetComponent<enemy_showDamage>().showDamage();
				}
		}

		public void getKnockBack(enemy_property enemyProperty,bullet_property bulletProperty){
				weaponType weapontype = bulletProperty.WeaponType;
				int force = bulletProperty.bulletknock;
				Vector3 enemyPos = enemyProperty.transform.position;
				Vector3 bulletPos = bulletProperty.transform.position;
				if (enemyProperty.heavyBody) {
						return;
				}
				//处理laser特殊击退效果
				if (weapontype == weaponType.laserNormal) {
						Debug.Log ("激光");
						Direction laserDirection = bulletProperty.gameObject.GetComponent<laserAniManager> ().BulletDirection;
						switch(laserDirection){
						default:
								break;
						case Direction.up:
								this.gameObject.rigidbody.AddForce (Vector3.up * force *50 );		
								break;
						case Direction.down:
								this.gameObject.rigidbody.AddForce (-1 * Vector3.up * force*50);	
								break;
						case Direction.left:
								this.gameObject.rigidbody.AddForce (-1 * Vector3.right * force*50);
								break;
						case Direction.right:
								this.gameObject.rigidbody.AddForce (Vector3.right * force*50);		
								break;

								break;
						}

				}
				if (weapontype == weaponType.bulletNormal) {
						Debug.Log ("子弹");
						this.gameObject.rigidbody.AddForce (new Vector3(enemyPos.x - bulletPos.x , enemyPos.y - bulletPos.y , 0) * force *50 );	

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
