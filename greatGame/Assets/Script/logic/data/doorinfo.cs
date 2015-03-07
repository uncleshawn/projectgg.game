using UnityEngine;
using System.Collections;

public class doorinfo {

	public constant.Direction mDir;	//门的方向
	public int mId;
	public int mNextRoomId;
	public int mNextDoorId;

	public float mX;
	public float mY;

	public doorinfo(){
		mDir = constant.Direction.east;
		mId = 0;
		mNextRoomId = 0;
	}

	public override string ToString ()
	{
		return string.Format ("[doorinfo]");
	}

	public bool hasNext(){
		return mNextRoomId != 0 && mNextDoorId != 0;
	}

}
