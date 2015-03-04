using UnityEngine;
using System.Collections;

public class base_property : MonoBehaviour {

	private float mFAcc = 60.0f; //摩擦力
	protected float mMass = 1.0f;
	public float FAcc { get { return mFAcc; } set { mFAcc = value; }}
	public float Mass { get { return mMass; } set { mMass = value; }}

	protected constant.BattleType mBattleType;
	public constant.BattleType BattleType { get {return mBattleType; } set { mBattleType = value; } }

	// Use this for initialization
	void Start () {
		mMass = this.rigidbody.mass;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isConflict(base_property v){
		if (mBattleType == v.BattleType) {
			return false;
		}

		return true;
	}

}
