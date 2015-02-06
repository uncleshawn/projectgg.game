using UnityEngine;
using System.Collections;

public class camera_follow_script : MonoBehaviour {

	GameObject mLeftDownPoint;
	GameObject mRightUpPoint;
	float mW = 30;
	float mH = 20;
	// Use this for initialization
	void Start () {
		mLeftDownPoint = constant.getLeftDownPoint ();
		mRightUpPoint = constant.getRightUpPoint ();
	}
	
	// Update is called once per frame
	void Update () {
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
		this.gameObject.transform.position = v;
	}
}
