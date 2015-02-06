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

}
