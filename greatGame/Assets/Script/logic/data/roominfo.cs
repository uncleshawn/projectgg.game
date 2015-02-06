using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class roominfo  {

	public int mId;
	public int mSceneIndex;

	public int roomX;
	public int roomY;

	public int roomW; 
	public int roomH;
	
	public int mEnterDoorId;

	public List<doorinfo> mDoorInfos;

	public roominfo(){
		mDoorInfos = new List<doorinfo> ();

		doorinfo doorInfo = new doorinfo ();
		doorInfo.mId = 1;
		doorInfo.mNextRoomId = 1;
		mDoorInfos.Add (doorInfo);

		mSceneIndex = 0;
	}

	public doorinfo getDoorInfo(int doorId){
		//foreach (doorinfo doorInfo in mDoorInfos) {
		for(int i = 0; i < mDoorInfos.Count; ++i){
			doorinfo doorInfo = mDoorInfos[i];
			if(doorInfo.mId == doorId)
			{
				return doorInfo;
			}
		}
		return null;
	}

	public int getNextRoomId(int doorId){
		foreach (doorinfo doorInfo in mDoorInfos) {
			if(doorInfo.mId == doorId)
			{
				return doorInfo.mNextRoomId;
			}
		}
		return 0;
	}
}
