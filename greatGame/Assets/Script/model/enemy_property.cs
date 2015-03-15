
using UnityEngine;
using System.Collections;

public class enemy_property : base_property {

	public bool mIsBoss;


		/// <summary>
		/// 父类参数
		/// protected float mBaseMoveSpeed = 10.0f;	//最快移动速度
		/// </summary>


		public string enemyName;
		public int enemyId;
		public bool heavyBody;			//敌人是否拥有强硬外壳(不能被击退)
		public float recoverTime;		//敌人在被某些情况行动停止后的反应时间
		public bool acting;				//敌人是否可以行动

		public int MaxHp;
		public float MaxMoveSpeed;
		public int MaxDamage;
		public int MaxAttackSpeed;
		public int MAxAttackRate;
		public float MaxAttackDistance;



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

		void Awake(){
				
		}
		// Use this for initialization
		void Start () {
				checkForget ();
				mHp = MaxHp;
				mMoveSpeed = MaxMoveSpeed;
				mDamage = MaxDamage;
				mAttackSpeed = MaxAttackSpeed;
				mAttackRate = MAxAttackRate;
				mAttackDistance = MaxAttackDistance;

				mBattleType = constant.BattleType.Enemy;

				mBaseMoveSpeed = 5.0f;
		}

		// Update is called once per frame
		void Update () {

		}

		void checkForget(){
				if (MaxHp == 0) {
						Debug.Log (gameObject.name + " 怪物物体警告: " + "怪物属性没有初始化,请检查inspector!");
				}
		}
}
