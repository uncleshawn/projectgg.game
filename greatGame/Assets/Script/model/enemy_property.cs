
using UnityEngine;
using System.Collections;

public class enemy_property : base_property {

	public string enemyName;
	public int enemyId;
	public bool mIsBoss;

	private int mHp;
	private int mMaxHp;
	private float mMoveSpeed;
	private int mDamage;
	private int mAttackSpeed;
	private int mAttackRate;
	private float mAttackDistance;
	
	
	
	public int Hp { get { return mHp; } set { mHp = value; }}
	public int MaxHp { get { return mMaxHp; } set { mMaxHp = value; }}
	public float MoveSpeed { get { return mMoveSpeed; } set { mMoveSpeed = value; }}
	public int Damage { get { return mDamage; } set { mDamage = value; }}
	public int AttackSpeed { get { return mAttackSpeed; } set { mAttackSpeed = value; }}
	public int AttackRate { get { return mAttackRate; } set { mAttackRate = value; }}
	public float AttackDistance { get { return mAttackDistance; } set { mAttackDistance = value; }}
	// Use this for initialization
	void Start () {
		mHp = 22;
		mMaxHp = mHp;
		mMoveSpeed = 10;
		mDamage = 1;
		mAttackSpeed = 1;
		mAttackRate = 1;
		mAttackDistance = 0;

		mBattleType = constant.BattleType.Enemy;

		mBaseMoveSpeed = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
