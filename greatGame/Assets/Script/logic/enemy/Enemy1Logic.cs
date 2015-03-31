using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy1Logic : enemylogic {


	private constant.Direction mDir;
	private float mAddX = 0;
	private float mAddY = 0;

	private float mAcc = 20;

	private bool mHasDes = false;
	private float mEndX = 0;
	private float mEndY = 0;

	private float mDeltaDistance = 0.4f;

	private Dictionary<constant.Direction, string> mDics;

	void Start () {
		mDir = constant.Direction.east;

		mDics = new Dictionary<constant.Direction, string> ();
		mDics.Add (constant.Direction.east, "enemy1_right");
		mDics.Add (constant.Direction.west, "enemy1_right");
		mDics.Add (constant.Direction.north, "enemy1_up");
		mDics.Add (constant.Direction.south, "enemy1_down");

		changeDir ();
	}

	void FixedUpdate () {
		bool needChangeDir = false;
		if (mHasDes) {
			Debug.Log("update：" + this.gameObject.transform.position.x + "," + mEndX);
			switch(mDir){
			case constant.Direction.east:
				if(this.gameObject.transform.position.x > mEndX){
					needChangeDir = true;
				}
				break;
			case constant.Direction.north:
				if(this.gameObject.transform.position.y > mEndY){
					needChangeDir = true;
				}
				break;
			case constant.Direction.south:
				if(this.gameObject.transform.position.y < mEndY){
					needChangeDir = true;
				}
				break;
			case constant.Direction.west:
				if(this.gameObject.transform.position.x < mEndX){
					needChangeDir = true;
				}
				break;
			}
		}

		if (needChangeDir) {
			changeDir();
		}
	}

	override public Vector3 getMoveAcc(){
		return new Vector3 (mAddX,mAddY,0);
	}

	void OnCollisionStay(Collision collision){
		 onCollision (collision);
	}

	void OnCollisionEnter(Collision collision){
		onCollision (collision);
	}

	void onCollision(Collision collision){
		constant.getMapLogic ().colliderEnter (this.gameObject, collision.gameObject);
		//deleteDistance ();
		changeDir ();
	}

	void deleteDistance(Collision collision){
		float x;
		float y;
		if (collision.gameObject.transform.position.x > this.transform.position.x) {
			x = this.transform.position.x - mDeltaDistance;
		} else {
			x = this.transform.position.x + mDeltaDistance;
		}
		if (collision.gameObject.transform.position.y > this.transform.position.y) {
			y = this.transform.position.y - mDeltaDistance;
		} else {
			y = this.transform.position.y + mDeltaDistance;
		}
		this.transform.position = new Vector3 (x,y, this.transform.position.z);
	}

	void deleteDistance(){
		switch (mDir) {
		case constant.Direction.east:
			this.transform.position = new Vector3(this.transform.position.x-mDeltaDistance, this.transform.position.y,
			                                      this.transform.position.z);
			break;
		case constant.Direction.north:
			this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y-mDeltaDistance,
			                                      this.transform.position.z);
			break;
		case constant.Direction.south:
			this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+mDeltaDistance,
			                                      this.transform.position.z);
			break;
		case constant.Direction.west:
			this.transform.position = new Vector3(this.transform.position.x+mDeltaDistance, this.transform.position.y,
			                                      this.transform.position.z);
			break;
		}
	}

	void changeDir(){
		//防止再次碰撞
		deleteDistance ();

		constant.Direction dir = getNextDir();
		if (dir == mDir) {
			mDir = getRandomDir ();
			mHasDes = false;
		} else {
			mDir = dir;
			mHasDes = true;
			GameObject obj = constant.getPlayer ();
			mEndX = obj.transform.position.x;
			mEndY = obj.transform.position.y;
		}

		move_script moveScript = this.gameObject.GetComponent<move_script> ();
		moveScript.stopMove ();

		Debug.Log ("chagneDir:" + mDir);
		playDirAni ();
		setDirAcc ();
	}

	private void setDirAcc(){
		mAddX = 0;
		mAddY = 0;
		switch (mDir) {
		case constant.Direction.east:
			mAddX = mAcc;
			break;
		case constant.Direction.west:
			mAddX = -mAcc;
			break;
		case constant.Direction.north:
			mAddY = mAcc;
			break;
		case constant.Direction.south:
			mAddY = -mAcc;
			break;
		}
	}

	private constant.Direction getRandomDir(){
		if (mDir == constant.Direction.east || mDir == constant.Direction.west) {
			return Random.Range (0, 1) == 1 ? constant.Direction.south : constant.Direction.north;
		} else {
			return Random.Range (0, 1) == 1 ? constant.Direction.east : constant.Direction.west;
		}
	}

	private constant.Direction getNextDir(){
		GameObject obj = constant.getPlayer ();
		float deltaX = obj.transform.position.x - this.gameObject.transform.position.x;
		float deltaY = obj.transform.position.y - this.gameObject.transform.position.y;

		if (Mathf.Abs (deltaX) > Mathf.Abs (deltaY)) {
			if (deltaX > 0) {
					return constant.Direction.east;
			} else {
					return constant.Direction.west;
			}
		} else {
			if(deltaY > 0){
				return constant.Direction.north;
			}else{
				return constant.Direction.south;
			}
		}

	}

	public void playDirAni(){
		GameObject aniSpr = constant.getChildGameObject(this.gameObject, "AnimatedSprite");
		tk2dSprite spr = aniSpr.GetComponent<tk2dSprite> ();
		tk2dSpriteAnimator animator = aniSpr.GetComponent<tk2dSpriteAnimator> ();

		string aniStr = mDics[mDir];
		animator.Play (aniStr);
		if (mDir == constant.Direction.west) {
			aniSpr.transform.localScale = new Vector3(-1,1,1);
		}else{
			aniSpr.transform.localScale = new Vector3(1,1,1);
		}

	}
}
