using UnityEngine;
using System.Collections;

public class bullet_property : base_property {

	public int bulletDamage;
	public int bulletknock;
	public float bulletDamageRate;
	public ElementType bulletElement;

	// Use this for initialization
	void Awake(){
		mBattleType = constant.BattleType.Other;
	}
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void setProperty(int damage, int knock, float damamgeRate, ElementType element, constant.BattleType battleType){
		bulletDamage = damage;
		bulletknock = knock ;
		bulletDamageRate = damamgeRate ; 
		bulletElement = element;
		mBattleType = battleType;
	}
}
