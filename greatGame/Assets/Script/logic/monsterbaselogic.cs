using UnityEngine;
using System.Collections;

public class monsterbaselogic : MonoBehaviour {

	//private float mFAcc = 20.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	virtual public Vector3 getMoveAcc(){
		Vector3 v = new Vector3 ();
		v.x = 0;
		v.y = 0;
		v.z = 0;
		return v;
	}

		virtual public Vector3 scaredMovePos(){
				Vector3 v = new Vector3 ();
				v.x = 0;
				v.y = 0;
				v.z = 0;
				return v;
		}

		virtual public void scaredMove(){
				
		}

	virtual public Vector3 getFAcc(){
		Vector3 v = new Vector3 ();

		maplogic logic = constant.getMapLogic ();
		v.x = logic.getRoomInfoFAcc() * this.rigidbody.mass;
		v.y = logic.getRoomInfoFAcc() * this.rigidbody.mass;
		v.z = 0;
		return v;
	}

	virtual public void beAttack(GameObject obj){

	}

	virtual public void destroy(){
		Destroy (this.gameObject);
	}

}
