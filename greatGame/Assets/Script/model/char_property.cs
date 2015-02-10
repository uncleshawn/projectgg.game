using UnityEngine;
using System.Collections;

public class char_property : base_property {
	
	//originProperties
	private int mMaxHp;
	private int mMaxNp;
	private float mMaxMoveSpeed;
	private int mMaxDamage;
	private int mMaxAttackSpeed;
	private int mMaxAttackRate;
	private float mMaxAttackDistance;



	//tempProperties
	private int mHp;
	private int mNp;
	private float mMoveSpeed;
	private int mDamage;
	private int mAttackSpeed;
	private int mAttackRate;
	private float mAttackDistance;



	public int MaxHp { get { return mMaxHp; } set { mMaxHp = value; }}
	public int MaxNp { get { return mMaxNp; } set { mMaxNp = value; }}
	public float MaxMoveSpeed { get { return mMaxMoveSpeed; } set { mMaxMoveSpeed = value; }}
	public int MaxDamage { get { return mMaxDamage; } set { mMaxDamage = value; }}
	public int MaxAttackSpeed { get { return mMaxAttackSpeed; } set { mMaxAttackSpeed = value; }}
	public int MaxAttackRate { get { return mMaxAttackRate; } set { mMaxAttackRate = value; }}
	public float MaxAttackDistance { get { return mMaxAttackDistance; } set { mMaxAttackDistance = value; }}

	public int Hp { get { return mHp; } set { mHp = value; }}
	public int Np { get { return mNp; } set { mNp = value; }}
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
		upgradeShootProperties();

		mMaxHp = 5;
		mMaxMoveSpeed = 10;
		mMaxDamage = 1;
		mMaxAttackSpeed = 1;
		mMaxAttackRate = 1;
		mMaxAttackDistance = 0;

		upgradeShootProperties();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void upgradeShootProperties(){
		GameObject shoot = this.transform.FindChild("shoot").gameObject;
		if(shoot){
			shoot.SendMessage("upgradeProperties",this);
		}
	}
	

	
}
