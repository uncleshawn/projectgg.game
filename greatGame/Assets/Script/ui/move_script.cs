using UnityEngine;
using System.Collections;

public class move_script : MonoBehaviour {

	private float mSpeed = 25.0f;	//最快速度
	private float mFps = 0.02f;

	// Use this for initialization
	void Start () {
		mFps = mFps;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		monsterbaselogic baseLogic = gameObject.GetComponent<monsterbaselogic>();

		Vector3 self_acc = baseLogic.getMoveAcc ();//new Vector3();	//加速度
		Vector3 f_acc = baseLogic.getFAcc ();//new Vector3 ();		//阻力减速度

		bool isXZero = false;
		bool isYZero = false;
		{
			Vector3 speed = rigidbody.velocity;
			Debug.Log("speed:" + speed.x + "," + speed.y + "," + speed.z);
			if (speed.x == 0) {
				f_acc.x = 0;
			} else if (speed.x > 0) {
				f_acc.x = -Mathf.Abs(f_acc.x);
			}else{
				f_acc.x = Mathf.Abs(f_acc.x);
			}

			if (speed.y == 0) {
				f_acc.y = 0;
			} else if (speed.y > 0) {
				f_acc.y = -Mathf.Abs(f_acc.y);
			}else{
				f_acc.y = Mathf.Abs(f_acc.y);
			}
			
			Vector3 actual_acc = self_acc + f_acc;
			if( (actual_acc.x*mFps+speed.x)*speed.x < 0 ){
				isXZero = true;
				f_acc.x = 0;
			}
			if( (actual_acc.y*mFps+speed.y)*speed.y < 0 ){
				isYZero = true;
				f_acc.y = 0;
			}
			actual_acc = self_acc + f_acc;

			Debug.Log("actual_acc:" + actual_acc.x + "," + actual_acc.y + "," + actual_acc.z);
			rigidbody.AddForce(actual_acc);
		}

		{
			//速度上限
			Vector3 speed = new Vector3 (isXZero?0:rigidbody.velocity.x, isYZero?0:rigidbody.velocity.y, rigidbody.velocity.z);
			if (speed.x > mSpeed) {
				speed.x = mSpeed;
			}else if(speed.x < -mSpeed){
				speed.x = -mSpeed; 
			}
			
			if (speed.y > mSpeed) {
				speed.y = mSpeed;
			}else if(speed.y < -mSpeed){
				speed.y = -mSpeed;
			}
			rigidbody.velocity = speed;
		}

	}
}
