using UnityEngine;
using System.Collections;

public class enemy4logic : enemylogic {
		
		enemy_property enemySelf;
		GameObject player;
		GameObject ani;
		tk2dSpriteAnimator enemyAni;

		//锁定主角位置的时间
		public float lockTargetTime;
		float lockTempTime;
		Vector3 playerPos;

		float deltaTime;

		void Awake(){
				//玩家的位置
				player = constant.getPlayer ();
				playerPos = player.transform.position;	
				//自己的属性
				enemySelf = gameObject.GetComponent<enemy_property>();

				//动画
				ani = transform.FindChild ("ui").FindChild ("enemySprite").gameObject;	

				//动画脚本
				enemyAni = ani.GetComponent<tk2dSpriteAnimator> ();

				lockTempTime = 0;
		}



		void Start () {

		}



		// Update is called once per frame
		void FixedUpdate () {
				deltaTime +=Time.fixedDeltaTime;
				lockTempTime += Time.fixedDeltaTime;
				if (lockTempTime >= lockTargetTime) {
						lockTarget ();
						lockTempTime = 0;
				}
		}
				

		//获得玩家位置
		void lockTarget(){
				playerPos = player.transform.position;	
		}


}
