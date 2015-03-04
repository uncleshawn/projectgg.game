using UnityEngine;
using System.Collections;

public class move_script : MonoBehaviour {

	private float mSpeed = 10.0f;	//最快速度
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
		//Debug.Log("self_acc:" + self_acc.x + "," + self_acc.y + "," + self_acc.z);
		bool isXZero = false;
		bool isYZero = false;
		Vector3 actual_acc = new Vector3 ();
		{
			Vector3 speed = rigidbody.velocity;
			//Debug.Log("speed:" + speed.x + "," + speed.y + "," + speed.z);
			if (speed.x == 0) {
				f_acc.x = 0;
			} else if (speed.x > 0) {
				f_acc.x = -Mathf.Abs(f_acc.x);
			}else {
				f_acc.x = Mathf.Abs(f_acc.x);
			}

			if (speed.y == 0) {
				f_acc.y = 0;
			} else if (speed.y > 0) {
				f_acc.y = -Mathf.Abs(f_acc.y);
			}else{
				f_acc.y = Mathf.Abs(f_acc.y);
			}
			
			actual_acc = self_acc + f_acc;
			if( (actual_acc.x*mFps+speed.x)*speed.x < 0 ){
				isXZero = true;
				f_acc.x = 0;
			}
			if( (actual_acc.y*mFps+speed.y)*speed.y < 0 ){
				isYZero = true;
				f_acc.y = 0;
			}
			actual_acc = self_acc + f_acc;

			//Debug.Log("actual_acc:" + actual_acc.x + "," + actual_acc.y + "," + actual_acc.z);
			rigidbody.AddForce(actual_acc);
		}

		{
			float maxSpeed = mSpeed;
			if(Mathf.Abs(self_acc.x)>0 && Mathf.Abs(self_acc.y)>0){
				maxSpeed = mSpeed*0.7f;
			}
			//速度上限
			Vector3 speed = new Vector3 (isXZero?0:rigidbody.velocity.x, isYZero?0:rigidbody.velocity.y, rigidbody.velocity.z);
			if (speed.x > maxSpeed) {
				speed.x = maxSpeed;
			}else if(speed.x < -maxSpeed){
				speed.x = -maxSpeed; 
			}
			
			if (speed.y > maxSpeed) {
				speed.y = maxSpeed;
			}else if(speed.y < -maxSpeed){
				speed.y = -maxSpeed;
			}

			rigidbody.velocity = speed;
		}

	}
}
