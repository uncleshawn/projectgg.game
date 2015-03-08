using UnityEngine;
using System.Collections;

public class bullet_property : base_property {

		private weaponType weapontype;
		public int bulletDamage;
		public int bulletknock;
		public float bulletDamageRate;
		public ElementType bulletElement;

		public weaponType WeaponType { get { return weapontype; } set { weapontype = value; }}

		// Use this for initialization
		void Awake(){
				mBattleType = constant.BattleType.Other;
		}
		void Start () {
		}

		// Update is called once per frame
		void Update () {

		}
		public void setProperty(weaponType weapon, int damage, int knock, float damamgeRate, ElementType element, constant.BattleType battleType){
				//Debug.Log ("子弹击退效果: " + knock);
				weapontype = weapon;
				bulletDamage = damage;
				bulletknock = knock ;
				bulletDamageRate = damamgeRate ; 
				bulletElement = element;
				mBattleType = battleType;
		}
}
