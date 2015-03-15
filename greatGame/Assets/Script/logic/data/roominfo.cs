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

	public float mFAcc; //场地摩擦力
	public constant.RoomType mRoomType;

	public bool mIsBossRoom;

	public List< KeyValuePair<itemtemplate, Vector3> > mItemPrefabs;
	public List< KeyValuePair<monstertemplate, Vector3> > mMonsterPrefabs;

	public roominfo(){
		mDoorInfos = new List<doorinfo> ();

		doorinfo doorInfo = new doorinfo ();
		doorInfo.mId = 1;
		doorInfo.mNextRoomId = 1;
		mDoorInfos.Add (doorInfo);

		mSceneIndex = 0;
		mFAcc = 60.0f;

		mItemPrefabs = new List< KeyValuePair<itemtemplate, Vector3> > ();
		mMonsterPrefabs = new List< KeyValuePair<monstertemplate, Vector3> > ();
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

	//剩余可以选择的方向
	public List<constant.Direction> getRemainDirs(){
		List<constant.Direction> dirs = new List<constant.Direction>();
		dirs.Add(constant.Direction.east);
		dirs.Add(constant.Direction.west);
		dirs.Add(constant.Direction.south);
		dirs.Add(constant.Direction.north);
		foreach(doorinfo info in this.mDoorInfos){
			foreach(constant.Direction dir in dirs){
				if(constant.Equals(info.mDir,dir)){
					dirs.Remove(dir);
					break;
				}
			}
		}
		return dirs;
	}

	public List<constant.Direction> getCanUseDirs(roominfo roomInfo){
		List<constant.Direction> dirs = getRemainDirs ();
		List<constant.Direction> nextDirs = roomInfo.getRemainDirs ();

		List<constant.Direction> getDirs = new List<constant.Direction> ();
		foreach (constant.Direction dir in dirs) {
			foreach(constant.Direction nextDir in nextDirs){
				if(constant.Equals(dir,constant.getOppsiteDir(nextDir))){
					getDirs.Add(dir);
					break;
				}
			}
		}

		return getDirs;
	}


	public void addItemPrefabs(itemtemplate template, Vector3 v){
		mItemPrefabs.Add (new KeyValuePair<itemtemplate, Vector3>(template, v));
	}

	public void addMonsterPrefabs(monstertemplate template, Vector3 v){
		mMonsterPrefabs.Add (new KeyValuePair<monstertemplate, Vector3>(template, v));
	}
}
