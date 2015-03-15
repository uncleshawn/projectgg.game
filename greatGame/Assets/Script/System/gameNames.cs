using UnityEngine;
using System.Collections;

public enum Direction
{ 
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

public struct bulletSpeStruct{
		public bool pierceBullet;
		public ElementType element;
		public KnockType knockType;
}

public class gameNames : MonoBehaviour {

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

}
