using UnityEngine;
using System.Collections;

public class enemy3logic : enemylogic {

		enemy_property enemySelf;
		GameObject ani;
		string spaceSearcherPath;
		tk2dSpriteAnimator enemyAni;
		enemyShotBullet shooter;
		public float waitTime;
		float tempTime;
		bool completeMove;

		// Use this for initialization
		void Awake(){


				//自己的属性
				enemySelf = gameObject.GetComponent<enemy_property>();
				//动画控制
				//ani = transform.FindChild ("ui").FindChild ("ani").gameObject;
				//寻路工具
				spaceSearcherPath = "Prefabs/logic/spaceSearch";
				//动画脚本
				//enemyAni = ani.GetComponent<tk2dSpriteAnimator> ();
				//攻击射击脚本
				shooter = gameObject.GetComponent<enemyShotBullet> ();
				//判断是否开始重复行动
				completeMove = true;

				shooter.upgradeProperties (enemySelf);
				tempTime = 0;
		}

		void Start () {
				
		}

		// Update is called once per frame
		void FixedUpdate () {
				tempTime += Time.deltaTime;
				if (tempTime > waitTime) {
						stoneAttack ();
						tempTime = 0;
				}
		}



		void stoneAttack(){
				shooter.shootMultiBullets (EnemyShotType.directDiverging, 3 , 20);
		}
}
