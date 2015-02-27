using UnityEngine;
using System.Collections;

public class accl_script : MonoBehaviour {

	// Use this for initialization
	float x = 0;
	float y = 0;
	float acc = 10;

	// PlayMakerFSM fsm;
	// public GameObject obj;

	private float mSpeed = 25.0f;
	private float mAcc = 50.0f;
	void Start () {

		// fsm = obj.GetComponent<PlayMakerFSM>();

		x = Input.GetAxis("Horizontal");
		y = Input.GetAxis ("Vertical");


	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// x = Input.GetAxis("Horizontal")*acc;
		// y = Input.GetAxis ("Vertical")*acc;
		// Debug.Log ("hor:" + x + ", ver:" + y);

		// fsm.FsmVariables.GetFsmFloat ("acc_x").Value = x;
		// fsm.FsmVariables.GetFsmFloat ("acc_y").Value = y;
		//rigidbody.AddForce(new Vector3(h*mAcc,v*mAcc,0));

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		//Debug.Log("hor:" + h + ",ver:" + v);

		float acc_h = h*mAcc;
		float acc_v = v*mAcc;

		bool isXZero = false;
		bool isYZero = false;
				
		if(h == 0 && rigidbody.velocity.x != 0)
		{
			float oldx = rigidbody.velocity.x;
			if(rigidbody.velocity.x < 0)
			{
				oldx = rigidbody.velocity.x + mAcc*0.02f;
				acc_h = mAcc;
			}else
			{
				oldx = rigidbody.velocity.x - mAcc*0.02f;
				acc_h = -mAcc;
			}
			if(rigidbody.velocity.x*oldx < 0)
			{
				isXZero = true;
				acc_h = 0;
			}
		}

		if(v == 0 && rigidbody.velocity.y != 0)
		{
			float oldy = rigidbody.velocity.y;
			if(rigidbody.velocity.y < 0)
			{
				oldy = rigidbody.velocity.y + mAcc*0.02f;
				acc_v = mAcc;
			}else
			{
				oldy = rigidbody.velocity.y - mAcc*0.02f;
				acc_v = -mAcc;
			}
			if(rigidbody.velocity.y*oldy < 0)
			{
				isYZero = true;
				acc_v = 0;
			}
		}

		//Debug.Log("acc_h:"+acc_h+"acc_v:"+acc_v+",x:"+rigidbody.velocity.x+",y:"+rigidbody.velocity.y);
		rigidbody.AddForce(new Vector3(acc_h,acc_v,0));

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
