using UnityEngine;
using System.Collections;

public static class constant {

	public enum Direction
	{
		west = 1,
		east = 2,
		north = 3,
		south = 4,
	}

	public enum BattleType
	{
		Player = 1,
		Enemy = 2,
		Other = 3,
	}

	public static string TAG_ENEMY = "Enemy";
	public static string TAG_PLAYER = "Player";
	public static string TAG_BULLET = "Bullet";
	public static string TAG_WALL = "Wall";
	public static string TAG_ITEM = "Item";

	public static Direction getOppsiteDir(Direction dir){
		switch (dir) {
		case Direction.east:
			return Direction.west;
		case Direction.west:
			return Direction.east;
		case Direction.south:
			return Direction.north;
		case Direction.north:
			return Direction.south;
		default:
			Debug.Log("eror dir:" + dir);
			return Direction.east;
		}
	}

	public static GameObject getGameLogicObj(){
		return GameObject.FindGameObjectWithTag("GameLogic");
	}

	public static gamelogic getGameLogic(){
		GameObject obj = constant.getGameLogicObj ();
		return obj.GetComponent<gamelogic>();
	}

	public static maplogic getMapLogic(){
		return GameObject.FindGameObjectWithTag("GameLogic").GetComponent<maplogic>();
	}

	public static uilogic getUiLogic(){
		return GameObject.FindGameObjectWithTag("GameLogic").GetComponent<uilogic>();
	}

	public static GameObject getMainCamera(){
		return GameObject.FindGameObjectWithTag("MainCamera");
	}

	public static GameObject getPlayer(){
		return GameObject.FindGameObjectWithTag("Player");
	}

	public static GameObject getLeftDownPoint(){
		return GameObject.FindGameObjectWithTag("LeftDownPoint");
	}

	public static GameObject getRightUpPoint(){
		return GameObject.FindGameObjectWithTag("RightUpPoint");
	}

	public static BattleType getBattleType(GameObject obj){
		if (obj.tag.Equals (constant.TAG_PLAYER)) {
			return BattleType.Player;
		}
		if (obj.tag.Equals (constant.TAG_ENEMY)) {
			return BattleType.Enemy;
		}
		return BattleType.Other;
	}

	public static bool isConflict(GameObject obj1, GameObject obj2){
		base_property pro1 = obj1.GetComponent<base_property> ();
		base_property pro2 = obj2.GetComponent<base_property> ();
		return isConflict (pro1, pro2);
	}

	private static bool isConflict(base_property pro1, base_property pro2){
		return pro1.isConflict (pro2);
	}
			
}
