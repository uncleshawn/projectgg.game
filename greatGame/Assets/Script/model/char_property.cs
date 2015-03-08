using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class char_property : base_property {
	
	//originProperties
	private int mMaxHp;
	private int mMaxNp;
	private float mMaxMoveSpeed;
	private int mMaxDamage;
	private int mMaxAttackSpeed;
	private int mMaxAttackRate;
	private float mMaxAttackDistance;
	private float mHurtTime;	//被攻击后的无敌时间


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
	public float HurtTime { get { return mHurtTime; } set { mHurtTime = value; }}

	public int Hp { get { return mHp; } set { mHp = value; }}
	public int Np { get { return mNp; } set { mNp = value; }}
	public float MoveSpeed { get { return mMoveSpeed; } set { mMoveSpeed = value; }}
	public int Damage { get { return mDamage; } set { mDamage = value; }}
	public int AttackSpeed { get { return mAttackSpeed; } set { mAttackSpeed = value; }}
	public int AttackRate { get { return mAttackRate; } set { mAttackRate = value; }}
	public float AttackDistance { get { return mAttackDistance; } set { mAttackDistance = value; }}


	private List<itemtemplate> mItems;
	public List<itemtemplate> Items{ get { return mItems; } set { mItems = value;} }

	private int mGold;
	public int Gold { get { return mGold;} set {mGold = value; } }

	private int mKeys;
	public int Keys { get { return mKeys;} set { mKeys = value; } }

	private itemtemplate mWeapon;
	public itemtemplate Weapon { get { return mWeapon; } set { mWeapon = value;} }


	void Awake() {
		mHp = 4;
		mNp = 2;
		mMoveSpeed = 10;
		mDamage = 1;
		mAttackSpeed = 1;
		mAttackRate = 1;
		mAttackDistance = 0;
		upgradeShootProperties();
		
		mMaxHp = 5;
		mMaxNp = 2;
		mMaxMoveSpeed = 10;
		mMaxDamage = 1;
		mMaxAttackSpeed = 1;
		mMaxAttackRate = 1;
		mMaxAttackDistance = 0;

		mHurtTime = 2;

		mBattleType = constant.BattleType.Player;

		mBaseMoveSpeed = 10.0f;

		mItems = new List<itemtemplate> ();
	}
	// Use this for initialization
	void Start () {



		upgradeShootProperties();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void upgradeShootProperties(){
		GameObject shoot = this.transform.FindChild("shoot").gameObject;
		if(shoot){
			shoot.SendMessage("upgradeProperties",this);
		}
	}

	public void addItem(itemtemplate item){
		mItems.Add (item);
	}

	public void addGold(int value){
		mGold += value;
	}

	public void addKey(int value){
		mKeys += value;
	}

}
