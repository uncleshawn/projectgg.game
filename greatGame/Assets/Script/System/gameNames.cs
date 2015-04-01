using UnityEngine;
using System.Collections;


public enum AniDimension{
		two		= 1,
		four	= 2
}



public enum Direction
{ 
		none 	= 0,
		up 		= 1,
		down 	= 2,
		left	= 3,
		right	= 4
}

public enum weaponType
{
		bulletNormal = 1,
		laserNormal	 = 2
}

public struct select_name_bool{
		public string name;
		public bool choose;
}

public struct recoverStruct{
		public int recoverHp;
		public int recoverNp;
}

public enum itemType
{	
		none 		= 1,
		recover 	= 2,
		enforce		= 3,
		equipment   = 4,
		treasure	= 5,
		weapon 		= 6,
		bulletEnforce = 7,
		special = 8
}

public enum ElementType
{
		normal		= 1,
		fire		= 2
}

public enum KnockType
{
		none		= 0,
		normal		= 1,
		explode		= 2
}

public struct ScaredBullet
{
		public bool scaredEffect;
		public int sacredPercent;
}

public struct SlowBullet
{
		public bool slowEffect;
		public int slowPercent;
		public float slowLevel;
}

public struct bulletSpeStruct
{
		public bool pierceBullet;
		public ElementType element;
		public KnockType knockType;
		public ScaredBullet scaredBullet;
		public SlowBullet slowBullet;
}

public struct StateColor
{
		public static Color normal = new Color(255/255,255/255,255/255);
		public static Color scared = new Color (230/255, 0, 255/255);
		public static Color hurt = new Color (255/255,60/255,60/255);
		public static Color slowDown = new Color (223 / 255, 255 / 255, 255 / 255);
}

public enum EnemyShotType
{
		//指向玩家
		directPlayer		 	= 1,
		//随机方向及角度
		random		 			= 2,
		//某方向随机
		directRandom			= 3,
		//方向发散型
		directDiverging			= 4,
}

public enum BattleStates
{
		normal		= 1,
		poisoned 	= 2, 
		slowDown 	= 3,
		dizzy		= 4,
		scared		= 5
}

public class gameNames : MonoBehaviour {

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

}
