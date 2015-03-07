using UnityEngine;
using System.Collections;
enum Direction
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

public struct name_bool{
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
	special = 6
}

public enum ElementType
{
	normal		= 1,
	fire		= 2
}

public class gameNames : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		
	}
}
