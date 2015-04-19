using UnityEngine;
using System.Collections;

public class bat_logic : enemylogic {
		
		//父类参数
		//public bool working;
		//
		//
		//

		float deltaTime;
		public float mIntervalTime = 1;
		public float mWaitTime = 1;
		enemy_property enemySelf;
		enemyAniManager aniManager;
		Direction aniDir;

		// Use this for initialization
		void Awake(){
				enemySelf = gameObject.GetComponent<enemy_property>();
				aniManager = gameObject.GetComponent<enemyAniManager> ();
				aniDir = Direction.down;
				GameObject obj  = constant.getPlayer ();
		}

		void Start () {

				deltaTime = 0;
		}

		// Update is called once per frame
		void FixedUpdate () {
				stateFixedUpdate ();
				deltaTime = deltaTime + Time.fixedDeltaTime;

		}

		override public Vector3 getMoveAcc(){
				Vector3 v = new Vector3 ();
				//Debug.Log ("bat getMoveAcc");
				GameObject obj  = constant.getPlayer ();
				Vector3 pos = obj.transform.position;

				if (obj == null) {
						return v;
				}

				deltaTime = deltaTime % (mIntervalTime + mWaitTime);
				if (deltaTime <= mWaitTime) {
						return v;
				}

				if (enemySelf.acting == false) {
						return v;
				}
						
				if (enemySelf.scared == true) {
						pos = scaredMovePos();
				}

				float add = 160;
				Vector3 selfPos = this.transform.position;

				float disX = pos.x - selfPos.x;
				float disY = pos.y - selfPos.y;

				if (Mathf.Abs (disX) >= Mathf.Abs (disY)) {
						if (disX >= 0) {
								aniDir = Direction.right;
								aniManager.playSameAni ("fly_left");
								aniManager.setAniSide (aniDir);

						} else {
								aniDir = Direction.left;
								aniManager.playSameAni ("fly_left");
								aniManager.setAniSide (aniDir);
						}
				} else {
						if (disY >= 0) {
								aniDir = Direction.up;	
								aniManager.playSameAni ("fly_up");
						} else {
								aniDir = Direction.down;
								aniManager.playSameAni ("fly_down");
						}
				}



				float dis = Mathf.Sqrt(disX*disX+disY*disY);
				float x = add * disX / dis;
				float y = add * disY / dis;
				v.x = x;
				v.y = y;

				return v;
		}
				

		override public Vector3 scaredMovePos(){
				Vector3 pos;
				pos = this.transform.position;
				pos.x += Random.Range (-20, 20);
				pos.y += Random.Range (-20, 20);
				return pos;
		}
				
}
