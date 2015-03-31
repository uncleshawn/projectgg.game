using UnityEngine;
using System.Collections;

public class bullet_property : base_property {

		private weaponType weapontype;
		public int bulletDamage;
		public int bulletknock;
		public float bulletDamageRate;


		//常规效果
		public bulletSpeStruct bulletSpe;


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

		//被外部调用修改子弹的属性
		public void setProperty(weaponType weapon, int damage, int knock, float damamgeRate, bulletSpeStruct bulletStruct, constant.BattleType battleType){
				//Debug.Log ("子弹击退效果: " + knock);
				weapontype = weapon;
				bulletDamage = damage;
				bulletknock = knock ;
				bulletDamageRate = damamgeRate ; 
				bulletSpe = bulletStruct;
				mBattleType = battleType;
				//Debug.Log ("子弹类型: " + weapontype);
		}
}
