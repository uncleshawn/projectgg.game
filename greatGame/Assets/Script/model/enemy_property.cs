
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
		public bool invincible;
		public bool heavyBody;			//敌人是否拥有强硬外壳(不能被击退)


		public bool acting;				//敌人是否可以行动
		public float recoverTime;		//敌人在被某些情况行动停止后的反应时间

		public bool scared;				//敌人是否处于恐惧状态
		public float scaredRecoverTime; //敌人从恐惧钟恢复时间

		public int MaxHp;

		public float MaxMoveSpeed;

		int MaxDamage;
		int MaxAttackSpeed;
		int MAxAttackRate;
		float MaxAttackDistance;



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

		float deltaTime;

		void Awake(){
				enemyId = constant.getMonsterFactory ().getMonsterId ();
				this.gameObject.name = this.gameObject.name + enemyId;
				mHp = MaxHp;
				mMoveSpeed = MaxMoveSpeed;
				mDamage = MaxDamage;
				mAttackSpeed = MaxAttackSpeed;
				mAttackRate = MAxAttackRate;
				mAttackDistance = MaxAttackDistance;
				acting = true;
				scared = false;
				invincible = false;
				mBattleType = constant.BattleType.Enemy;
				checkForget ();

				deltaTime = 0;
				//mBaseMoveSpeed = 10.0f;
		}
		// Use this for initialization
		void Start () {
				
		}

		// Update is called once per frame
		void FixedUpdate () {

		}

		void checkForget(){
				if (MaxHp == 0) {
						Debug.Log (gameObject.name + " 怪物物体警告: " + "怪物属性没有初始化,请检查inspector!");
				}
				if (mBaseMoveSpeed == 0) {
						mBaseMoveSpeed = 10;
				}
		}

}
