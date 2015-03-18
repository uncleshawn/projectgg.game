using UnityEngine;
using System.Collections;

public class base_property : MonoBehaviour {

	//private float mFAcc = 60.0f; //摩擦力
	//public float FAcc { get { return mFAcc; } set { mFAcc = value; }}

	protected float mBaseMoveSpeed = 15.0f;	//最快移动速度
	public float BaseMoveSpeed { get { return mBaseMoveSpeed; } set { mBaseMoveSpeed = value; }}

	protected constant.BattleType mBattleType;
	public constant.BattleType BattleType { get {return mBattleType; } set { mBattleType = value; } }

	void Awake(){
	}
	// Use this for initialization
	void Start () {
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
