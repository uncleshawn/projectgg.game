using UnityEngine;
using System.Collections;

public class enemylogic : monsterbaselogic {

		float scaredTime;

		// Use this for initialization
		void Awake(){
				scaredTime = 0;
		}
		void Start () {

		}

		// Update is called once per frame
		void FixedUpdate () {
				stateFixedUpdate ();
		}

		//处理怪物状态刷新
		protected void stateFixedUpdate(){
				scaredTime += Time.fixedDeltaTime;
				checkScaredRecover (scaredTime);
		}




		//被 -攻击- 逻辑处理
		override public void beAttack(GameObject obj){
				if (obj.tag.Equals ("Bullet")) {
						//Debug.Log("enemy be attacked by bullet.");
						bullet_property bulletProperty = obj.GetComponent<bullet_property>();
						enemy_property enemyProperty = gameObject.GetComponent<enemy_property>();
						if(enemyProperty.invincible == false){
							getDamage(enemyProperty,bulletProperty);
						}

						//判断击退类型
						if (bulletProperty.bulletknock != 0) {
								Debug.Log (gameObject.name + "被击退.武器类型为:");
								getKnockBack (enemyProperty, bulletProperty);
						}

						if(isDie()){
								GameObject.Destroy(this.gameObject);
								constant.getMapLogic().checkOpenDoor();
						}

						//判断攻击特效附加效果
						checkBulletEffect(enemyProperty,obj);
				}
		}



		//获得伤害
		public void getDamage(enemy_property enemyProperty,bullet_property bulletProperty){
				//Debug.Log("enemy get damage: " + bulletProperty.bulletDamage);
				enemyProperty.Hp = enemyProperty.Hp - bulletProperty.bulletDamage;

				Vector3 objectPos = this.transform.position;




				//伤害显示-------------

				//调整伤害输出的位置
				objectPos.y += 1;
				objectPos.z = -5;

				string showDamagePath = "Prefabs/ui/UI_showDamage";
				GameObject showDamageClone = (GameObject)Instantiate(Resources.Load(showDamagePath),objectPos,Quaternion.identity);
				if(showDamageClone){
						string damage = "-" + bulletProperty.bulletDamage.ToString();
						showDamageClone.GetComponent<enemy_showDamage>().showDamage(damage);

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
						normalKnockBack (bulletProperty, force);
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
				Direction bulletDirection;
				bulletDirection = Direction.none;
				if (bulletProperty.WeaponType == weaponType.laserNormal) {
						bulletDirection = bulletProperty.gameObject.GetComponent<laserAniManager> ().BulletDirection;
				}
				if (bulletProperty.WeaponType == weaponType.bulletNormal) {
						bulletDirection = bulletProperty.gameObject.GetComponent<bulletAniManager> ().BulletDirection;
				}

				if (bulletDirection == Direction.none) {
						Debug.Log("击退时:获取武器类型出错");
						return;
				}


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

		void checkBulletEffect(enemy_property enemyProperty , GameObject bullet){
				bullet_property bulletProperty = bullet.GetComponent<bullet_property> ();
				if(bulletProperty){
						//是否具有恐惧效果
						if (bulletProperty.bulletSpe.scaredBullet.scaredEffect) {
								ScaredBullet scaredBullet = bulletProperty.bulletSpe.scaredBullet;
								getScared (enemyProperty , scaredBullet);
						}
				}
				
		}


		//判断恐惧效果
		public void getScared(enemy_property enemyProperty  , ScaredBullet scaredBullet){
				int num = Random.Range (1, 101);
				if (num <= scaredBullet.sacredPercent) {
						Debug.Log ("敌人被恐惧");
						enemyProperty.scared = true;
				}
		}

		//判断解除恐惧效果
		public void checkScaredRecover(float deltaTime){
				enemy_property enemyProperty = gameObject.GetComponent<enemy_property> ();
				if (enemyProperty.scared == true) {
						float scaredRecoverTime = enemyProperty.scaredRecoverTime;
						if (deltaTime >= scaredRecoverTime) {
								//Debug.Log (deltaTime + "--" + scaredRecoverTime + ": " + this.name + "解除恐惧");
								enemyProperty.scared = false;
								scaredTime = 0;
						}
				} else {
						scaredTime = 0;
				}

		}


		//判断玩家是否死亡
		public bool isDie(){
				enemy_property enemyProperty = gameObject.GetComponent<enemy_property>();
				if (enemyProperty.Hp <= 0) {
						return true;
				}
				return false;
		}


		void OnTriggerEnter(Collider obj){
				
		}

		void OnCollisionEnter (Collision obj){
				
		}




}
