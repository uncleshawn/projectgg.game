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

		private int mExtarLifes;


		//tempProperties
		private int mHp;
		private int mNp;
		private float mMoveSpeed;
		private int mDamage;
		private int mAttackSpeed;
		private int mAttackRate;
		private float mAttackDistance;

		public bool heavyBody;			//是否不被击退
		public bool scared;				//敌人是否处于恐惧状态
		public float scaredRecoverTime; //敌人从恐惧钟恢复时间


		public int MaxHp { get { return mMaxHp; } set { mMaxHp = value; }}
		public int MaxNp { get { return mMaxNp; } set { mMaxNp = value; }}
		public float MaxMoveSpeed { get { return mMaxMoveSpeed; } set { mMaxMoveSpeed = value; }}
		public int MaxDamage { get { return mMaxDamage; } set { mMaxDamage = value; }}
		public int MaxAttackSpeed { get { return mMaxAttackSpeed; } set { mMaxAttackSpeed = value; }}
		public int MaxAttackRate { get { return mMaxAttackRate; } set { mMaxAttackRate = value; }}
		public float MaxAttackDistance { get { return mMaxAttackDistance; } set { mMaxAttackDistance = value; }}
		public float HurtTime { get { return mHurtTime; } set { mHurtTime = value; }}
		public int ExtarLifes { get { return mExtarLifes; } set { mExtarLifes = value; }}


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
				mHp = 3;
				mNp = 2;
				mMoveSpeed = 1;
				mDamage = 1;
				mAttackSpeed = 1;
				mAttackRate = 1;
				mAttackDistance = 0;
				upgradeShootProperties();

				mMaxHp = 3;
				mMaxNp = 2;
				mMaxMoveSpeed = 1;
				mMaxDamage = 1;
				mMaxAttackSpeed = 1;
				mMaxAttackRate = 1;
				mMaxAttackDistance = 0;

				mHurtTime = 1;
				scaredRecoverTime = 1.5f;

				mBattleType = constant.BattleType.Player;

				mItems = new List<itemtemplate> ();
				mBaseMoveSpeed = 6.0f;
		}
		// Use this for initialization
		void Start () {
				upgradeShootProperties();
		}

		// Update is called once per frame
		void Update () {

		}

		public void upgradePlayerProperty(enforce_Property enforceProperty){
				//Debug.Log ("人物强化");
				MaxHp += enforceProperty.MaxHp;
				Hp += enforceProperty.MaxHp;
				if (Hp > MaxHp) {
						Hp = MaxHp;
				}
				MaxNp += enforceProperty.MaxNp;
				Np += enforceProperty.MaxNp;
				if (Np > MaxNp) {
						Np = MaxNp;
				}
				MaxMoveSpeed += enforceProperty.MaxMoveSpeed;
				MoveSpeed += enforceProperty.MaxMoveSpeed;
				MaxDamage += enforceProperty.MaxDamage;
				Damage += enforceProperty.MaxDamage;
				MaxAttackSpeed += enforceProperty.MaxAttackSpeed;
				AttackSpeed += enforceProperty.MaxAttackSpeed;
				MaxAttackRate += enforceProperty.MaxAttackRate;
				AttackRate += enforceProperty.MaxAttackRate;
				MaxAttackDistance += enforceProperty.MaxAttackDistance; 
				AttackDistance += enforceProperty.MaxAttackDistance; 

				mBaseMoveSpeed = 5 + MaxMoveSpeed;

				//更新子弹的武器伤害
				upgradeShootProperties ();
		}

		public void upgradeShootProperties(){
				GameObject shoot = this.transform.FindChild("shoot").gameObject;
				if(shoot){
						shoot.SendMessage("upgradeProperties",this);
				}
		}

		public void enforceBullet(bulletSpeStruct bulletSpe){
				GameObject shoot = this.transform.FindChild("shoot").gameObject;
				if(shoot){
						shoot.SendMessage("setBulletSpecial",bulletSpe);
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
