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
		// Use this for initialization
		void Start () {
				deltaTime = 0;
		}

		// Update is called once per frame
		void FixedUpdate () {
				GameObject obj  = constant.getPlayer ();
				//		enemy_property pro = this.gameObject.GetComponent<>enemy_property();

				//		pro.MoveSpeed;
				deltaTime = deltaTime + Time.fixedDeltaTime;
		}

		override public Vector3 getMoveAcc(){
				Vector3 v = new Vector3 ();
				//Debug.Log ("bat getMoveAcc");
				GameObject obj  = constant.getPlayer ();
				if (obj == null) {
						return v;
				}

				deltaTime = deltaTime % (mIntervalTime + mWaitTime);
				if (deltaTime <= mWaitTime) {
						return v;
				}

				Vector3 pos = obj.transform.position;

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
}
