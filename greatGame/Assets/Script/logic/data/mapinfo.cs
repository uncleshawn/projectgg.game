using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mapinfo {

	public List<roominfo> mRoomInfos;

	public int mapW;
	public int mapH;

	public int mCurRoomId;

	public mapinfo(){

	}

	public float getRoomInfoFAcc(){
		roominfo info = getCurRoom ();
		return info.mFAcc;
	}

	public roominfo getFirstRoom(){
		if (mRoomInfos != null && mRoomInfos.Count > 0) {
			return mRoomInfos[0];
		}
		return null;
	}

	public roominfo getCurRoom(){
		foreach (roominfo roomInfo in mRoomInfos) {
			if(roomInfo.mId == mCurRoomId){
				return roomInfo;
			}
		}
		return null;
	}

	public roominfo getNextRoom(doorinfo doorInfo){
		foreach (roominfo roomInfo in mRoomInfos) {
			if(roomInfo.mId == doorInfo.mNextRoomId){
				roomInfo.mEnterDoorId = doorInfo.mNextDoorId;

				mCurRoomId = roomInfo.mId;
				return roomInfo;
			}
		}
		return null;
	}

	public roominfo getRoomInfo(int roomId){
		foreach (roominfo info in mRoomInfos) {
			if(info.mId == roomId){
				return info;
			}
		}
		return null;
	}

	public roominfo getNextMainRoomInfo(roominfo info){
		if (info.mDoorInfos != null) {
			foreach(doorinfo doorInfo in info.mDoorInfos){
				if(doorInfo.mMain){
					return getRoomInfo(doorInfo.mNextRoomId);
				}
			}
		}
		return null;
	}

}
