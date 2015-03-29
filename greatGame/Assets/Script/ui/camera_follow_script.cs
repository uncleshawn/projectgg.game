using UnityEngine;
using System.Collections;

public class camera_follow_script : MonoBehaviour {

	GameObject mLeftDownPoint;
	GameObject mRightUpPoint;
	float mW = 30;
	float mH = 20;

        float mShakeTotalTimes = 1.5f;
        float mShakeTime = 0;
        bool mStartShake = false;
        float mShakePer = 0.6f;
        float mShakeMul = 1.08f;
        float mStartShakeTimes = 0.05f;
	// Use this for initialization
	void Start () {
		mLeftDownPoint = constant.getLeftDownPoint ();
		mRightUpPoint = constant.getRightUpPoint ();
	}

        public void shake() {
                mStartShake = true;
                mShakeTime = mStartShakeTimes;
        }

	// Update is called once per frame
        void FixedUpdate() {
		GameObject obj = constant.getPlayer ();
		if (obj != null) {
			this.gameObject.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, this.gameObject.transform.position.z);
		}

		Vector3 v = this.gameObject.transform.position;
		if (v.x > mRightUpPoint.transform.position.x-mW/2) {
			v.x = mRightUpPoint.transform.position.x-mW/2;
		}
		if (v.x < mLeftDownPoint.transform.position.x+mW/2) {
			v.x = mLeftDownPoint.transform.position.x+mW/2;
		}
		if (v.y > mRightUpPoint.transform.position.y-mH/2) {
			v.y = mRightUpPoint.transform.position.y-mH/2;
		}
		if (v.y < mLeftDownPoint.transform.position.y+mH/2) {
			v.y = mLeftDownPoint.transform.position.y+mH/2;
		}

                if (mStartShake) {
                        if (mShakeTime < mShakeTotalTimes) {
                                v.x = v.x + (Random.Range(0, mShakeTime * 2) - mShakeTime) * mShakePer;
                                v.y = v.y + (Random.Range(0, mShakeTime * 2) - mShakeTime) * mShakePer;
                                mShakeTime = mShakeTime * mShakeMul;
                        } else {
                                mStartShake = false;
                        }
                } else {
                        if (mShakeTime > mStartShakeTimes) {
                                //mShakeTime = mShakeTime - Time.deltaTime;
                                v.x = v.x + (Random.Range(0, mShakeTime * 2) - mShakeTime) * mShakePer;
                                v.y = v.y + (Random.Range(0, mShakeTime * 2) - mShakeTime) * mShakePer;
                                mShakeTime = mShakeTime / mShakeMul;
                        }
                }

		this.gameObject.transform.position = v;
	}
}
