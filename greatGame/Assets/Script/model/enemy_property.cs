using UnityEngine;
using System.Collections;

public class enemy_property : base_property {

	private int mHp;
	private float mMoveSpeed;
	private int mDamage;
	private int mAttackSpeed;
	private int mAttackRate;
	private float mAttackDistance;
	
	
	
	public int Hp { get { return mHp; } set { mHp = value; }}
	public float MoveSpeed { get { return mMoveSpeed; } set { mMoveSpeed = value; }}
	public int Damage { get { return mDamage; } set { mDamage = value; }}
	public int AttackSpeed { get { return mAttackSpeed; } set { mAttackSpeed = value; }}
	public int AttackRate { get { return mAttackRate; } set { mAttackRate = value; }}
	public float AttackDistance { get { return mAttackDistance; } set { mAttackDistance = value; }}
	// Use this for initialization
	void Start () {
		mHp = 5;
		mMoveSpeed = 10;
		mDamage = 1;
		mAttackSpeed = 1;
		mAttackRate = 1;
		mAttackDistance = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
