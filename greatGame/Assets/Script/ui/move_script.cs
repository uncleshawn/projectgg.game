﻿using UnityEngine;
using System.Collections;

public class move_script : MonoBehaviour {

	//private float mSpeed = 10.0f;	//最快速度
	protected float mFps = 0.02f;

	// Use this for initialization
	void Start () {
		mFps = mFps;
	}

	public void stopMove(){
		rigidbody.velocity = new Vector3 (0,0,0);
	}
	// Update is called once per frame
	void FixedUpdate () {

		monsterbaselogic baseLogic = gameObject.GetComponent<monsterbaselogic>();
		if (baseLogic == null) {
			return;
		}

		base_property pro = gameObject.GetComponent<base_property> ();
		if (pro == null) {
			return;
		}

                Vector3 speed = rigidbody.velocity;

		Vector3 self_acc = baseLogic.getMoveAcc ();//new Vector3();	//加速度
		//Vector3 f_acc = baseLogic.getFAcc ();//new Vector3 ();		//阻力减速度
                Vector3 f_acc = baseLogic.getFAcc();
                float fAcc = f_acc.x;
                float speedD = Mathf.Sqrt(speed.x * speed.x + speed.y * speed.y);
                if (speedD > 0) {
                        f_acc = new Vector3(speed.x / speedD * fAcc, speed.y / speedD * fAcc, 0);
                }

		//Debug.Log("self_acc:" + self_acc.x + "," + self_acc.y + "," + self_acc.z);
		bool isXZero = false;
		bool isYZero = false;
		Vector3 actual_acc = new Vector3 ();
		{
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
			//rigidbody.AddForce(actual_acc);
		}

                //float x = actual_acc.x * mFps + speed.x;
                //float y = actual_acc.y * mFps + speed.y;
                //if (speed.x * speed.x + speed.y * speed.y < pro.BaseMoveSpeed * pro.BaseMoveSpeed) {
                //        rigidbody.AddForce(actual_acc);
                //} else if (x * x + y * y < pro.BaseMoveSpeed * pro.BaseMoveSpeed) {
                //        rigidbody.AddForce(actual_acc);
                //}


                rigidbody.AddForce(actual_acc);
                ////速度上限
                speed = new Vector3(isXZero ? 0 : rigidbody.velocity.x, isYZero ? 0 : rigidbody.velocity.y, rigidbody.velocity.z);
                if (speed.x * speed.x + speed.y * speed.y > pro.BaseMoveSpeed * pro.BaseMoveSpeed) {

                        float s = Mathf.Sqrt(speed.x * speed.x + speed.y * speed.y);
                        float x = pro.BaseMoveSpeed/s * speed.x;
                        float y = pro.BaseMoveSpeed/s * speed.y;

                        speed = new Vector3(x,y, speed.z);
                }
                rigidbody.velocity = speed;

                //float maxSpeed = pro.BaseMoveSpeed;
                //if (Mathf.Abs(self_acc.x) > 0 && Mathf.Abs(self_acc.y) > 0) {
                //        maxSpeed = pro.BaseMoveSpeed * 0.7f;
                //}
                ////速度上限
                //speed = new Vector3(isXZero ? 0 : rigidbody.velocity.x, isYZero ? 0 : rigidbody.velocity.y, rigidbody.velocity.z);
                ////Debug.Log(this.gameObject.tag + " speedx:" + speed.x + "," + maxSpeed);

                //bool isMaxX = false;
                //if (speed.x > maxSpeed) {
                //        speed.x = maxSpeed;
                //        isMaxX = true;
                //} else if (speed.x < -maxSpeed) {
                //        speed.x = -maxSpeed;
                //        isMaxX = true;
                //}

                //bool isMaxY = false;
                //if (speed.y > maxSpeed) {
                //        speed.y = maxSpeed;
                //        isMaxY = true;
                //} else if (speed.y < -maxSpeed) {
                //        speed.y = -maxSpeed;
                //        isMaxY = true;
                //}

                //if (isMaxX && actual_acc.x * speed.x > 0) {
                //        actual_acc.x = 0;
                //}
                //if (isMaxY && actual_acc.y * speed.y > 0) {
                //        actual_acc.y = 0;
                //}
                //rigidbody.AddForce(actual_acc);

                //rigidbody.velocity = speed;

	}
}
