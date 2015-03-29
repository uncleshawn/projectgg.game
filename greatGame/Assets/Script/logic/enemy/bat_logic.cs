using UnityEngine;
using System.Collections;

public class bat_logic : enemylogic {
		
		//父类参数
		//public bool working;
		//
		//
		//

		float deltaTime;
		float mIntervalTime = 1;
		float mWaitTime = 1;
		enemy_property enemySelf;

		// Use this for initialization
		void Awake(){
				enemySelf = gameObject.GetComponent<enemy_property>();
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
				if (selfPos.x > pos.x) {
						v.x = -add;
				} else if (selfPos.x < pos.x) {
						v.x = add;
				}
				if (selfPos.y > pos.y) {
						v.y = -add;
				}else if (selfPos.y < pos.y) {
						v.y = add;
				}
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
