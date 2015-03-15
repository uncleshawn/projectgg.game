using UnityEngine;
using System.Collections;

public class follower_property : base_property {

		public Vector3 favoritePos;
		public weaponType bulletType;

		public int mDamage;
		public int mAttackSpeed;
		public int mAttackRate;
		public float mAttackDistance;


		public int Damage { get { return mDamage; } set { mDamage = value; }}
		public int AttackSpeed { get { return mAttackSpeed; } set { mAttackSpeed = value; }}
		public int AttackRate { get { return mAttackRate; } set { mAttackRate = value; }}
		public float AttackDistance { get { return mAttackDistance; } set { mAttackDistance = value; }}

		// Use this for initialization

		void Start () {
				upgradeShootProperties();
		}

		// Update is called once per frame
		void Update () {

		}

		//更新属性
		public void upgradeShootProperties(){
				GameObject shoot = this.transform.FindChild("shoot").gameObject;
				if(shoot){
						shoot.GetComponent<shootlogic>().selectWeapon(bulletType);
						shoot.SendMessage("upgradeFollowProperties",this);
				}
		}


}
