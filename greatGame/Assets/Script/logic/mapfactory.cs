using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mapfactory {

	private static mapfactory mInstance;
	
	private mapfactory()
	{
		initScenes ();
	}
	
	public static mapfactory getInstance()
	{
		if(mInstance == null)
		{
			mInstance = new mapfactory();
		}
		return mInstance;
	}

	private SceneTemplate[] mSceneTemplateInfos;
	
	public SceneTemplate getSceneTemplate(int sceneIndex){
		SceneTemplate[] sceneTemplates = mSceneTemplateInfos;
		foreach (SceneTemplate template in sceneTemplates) {
			if(template.SceneIndex == sceneIndex){
				return template;
			}
		}
		return null;
	}

	//初始化所有场景
	public void initScenes()
	{
		List<Vector3> itemPos = new List<Vector3>{new Vector3(-0.4f, 0, -1), };
		List<Vector3> monsterPos = new List<Vector3> ();
		float startX = -10.4f;
		float startY = 3;
		float endX = 10.4f;
		float endY = -3;
		float intervalX = 1;
		float intervalY = -0.5f;
		for (float x = startX; x < endX; x=x+intervalX) {
			for(float y = startY; y > endY; y=y+intervalY){
				monsterPos.Add(new Vector3(x,y, -1));
			}
		}

		mSceneTemplateInfos = new SceneTemplate[]{
			new SceneTemplate(2, itemPos, monsterPos, "Prefabs/scene/Scene_Trap"),
		};
	}

	public mapinfo getNextMap()
	{
		return new mapinfo();
	}
	
	public mapinfo getRandomMap(maplogic logic){

		mapinfo mapInfo = new mapinfo ();
		mapInfo.mRoomInfos = new List<roominfo> ();

		int roomNum = logic.getFloorTotalRoomNum ();
		int itemRoomNum = roomNum / 4 + (Random.Range (-1,1));
		if(itemRoomNum <= 0){
			itemRoomNum = 1;
		}
		int roomId = 1;
		int doorId = 1;
		for (int i = 0; i < roomNum; ++i) {
			roominfo roomInfo = new roominfo ();
			roomInfo.roomH = 1;
			roomInfo.roomW = 1;

			roomInfo.mRoomType = constant.RoomType.Monster;
			if(i == 0){
				roomInfo.mRoomType = constant.RoomType.Start;
			}
			if(i == roomNum-1){
				roomInfo.mIsBossRoom = true;
			}

			roomInfo.mSceneIndex = 2;
			roomInfo.mId = roomId;
			++roomId;
			roomInfo.mDoorInfos = new List<doorinfo>();

			//左边门
			if(i > 0)
			{
				doorinfo doorInfo = new doorinfo();
				doorInfo.mId = doorId;
				++doorId;

				doorInfo.mDir = constant.Direction.east;
				doorInfo.mNextDoorId = doorId-2;
				doorInfo.mNextRoomId = roomId-2;
				doorInfo.mX = 0f;
				doorInfo.mY = 0.5f;
				roomInfo.mDoorInfos.Add(doorInfo);
			}

			//右边门
			if(i != roomNum-1)
			{
				doorinfo doorInfo = new doorinfo();
				doorInfo.mId = doorId;
				++doorId;

				doorInfo.mDir = constant.Direction.west;
				doorInfo.mNextDoorId = doorId;
				doorInfo.mNextRoomId = roomId;
				doorInfo.mX = 1f;
				doorInfo.mY = 0.5f;
				doorInfo.mMain = true;
				roomInfo.mDoorInfos.Add(doorInfo);
			}
			
			mapInfo.mRoomInfos.Add (roomInfo);

		}

		List<int> mMonsterRoomIds = new List<int>();
		for(int i =1; i < roomId-1; ++i){
			mMonsterRoomIds.Add(i);
		}

		//创建几个到道具房间
		for(int i = 0; i < itemRoomNum; ++i){

			//随机选择一个房间
			int nextRoomIndex = Random.Range(0,mMonsterRoomIds.Count-1);
			int nextRoomId = mMonsterRoomIds[nextRoomIndex];
			mMonsterRoomIds.RemoveAt(nextRoomIndex);
			roominfo nextRoomInfo = null;
			for(int j = 0 ; j < mapInfo.mRoomInfos.Count; ++j){
				roominfo childInfo = mapInfo.mRoomInfos[j];
				if(childInfo.mId == nextRoomId){
					nextRoomInfo = childInfo;
					break;
				}
			}

			if(nextRoomInfo != null){
				roominfo roomInfo = new roominfo ();
				roomInfo.roomH = 1;
				roomInfo.roomW = 1;
				roomInfo.mSceneIndex = 2;
				roomInfo.mRoomType = constant.RoomType.Item;
				roomInfo.mId = roomId;
				++roomId;
				roomInfo.mDoorInfos = new List<doorinfo>();

				{
					//随机选择一个门
					List<constant.Direction> dirs = nextRoomInfo.getRemainDirs();
					constant.Direction enterDir = dirs[Random.Range(0,dirs.Count-1)]; 
					Debug.Log("随机选择一个门:" + enterDir);
					//加一个门给monster房间
					{
						doorinfo doorInfo = new doorinfo();
						doorInfo.mId = doorId;
						++doorId;
						doorInfo.mDir = enterDir;
						doorInfo.mNextDoorId = doorId;
						doorInfo.mNextRoomId = roomInfo.mId;
						//doorInfo.mX = 1f;
						//doorInfo.mY = 0.5f;
						doorInfo.mX = getDoorX(nextRoomInfo, doorInfo.mDir);
						doorInfo.mY = getDoorY(nextRoomInfo, doorInfo.mDir);
						nextRoomInfo.mDoorInfos.Add(doorInfo);
					}
					
					//加一个门给item房间指向上一个monster房间
					{
						doorinfo doorInfo = new doorinfo();
						doorInfo.mId = doorId;
						++doorId;
						doorInfo.mDir = constant.getOppsiteDir(enterDir); //constant.Direction.west;
						doorInfo.mNextDoorId = doorId-2;
						doorInfo.mNextRoomId = nextRoomInfo.mId;
						doorInfo.mX = getDoorX(roomInfo, doorInfo.mDir);
						doorInfo.mY = getDoorY(roomInfo, doorInfo.mDir);
						roomInfo.mDoorInfos.Add(doorInfo);
					}
				}

				{
					roominfo nextNextRoomInfo = mapInfo.getNextMainRoomInfo(nextRoomInfo);
					//随机选择一个门
					List<constant.Direction> dirs = roomInfo.getCanUseDirs(nextNextRoomInfo);
					constant.Direction enterDir = dirs[Random.Range(0,dirs.Count-1)];
					//加一个门给item房间指向下一个monster房间
					{					
						doorinfo doorInfo = new doorinfo();
						doorInfo.mId = doorId;
						++doorId;
						doorInfo.mDir = enterDir;
						doorInfo.mNextDoorId = doorId;
						doorInfo.mNextRoomId = nextNextRoomInfo.mId;
						doorInfo.mX = getDoorX(roomInfo, doorInfo.mDir);
						doorInfo.mY = getDoorY(roomInfo, doorInfo.mDir);
						roomInfo.mDoorInfos.Add(doorInfo);
					}

					//加一个门给下一个monster房间
					{
						doorinfo doorInfo = new doorinfo();
						doorInfo.mId = doorId;
						++doorId;
						doorInfo.mDir = constant.getOppsiteDir(enterDir); //constant.Direction.west;
						doorInfo.mNextDoorId = doorId-2;
						doorInfo.mNextRoomId = roomInfo.mId;
						doorInfo.mX = getDoorX(nextNextRoomInfo, doorInfo.mDir);
						doorInfo.mY = getDoorY(nextNextRoomInfo, doorInfo.mDir);
						nextNextRoomInfo.mDoorInfos.Add(doorInfo);
					}
				}

				mapInfo.mRoomInfos.Add(roomInfo);
			}

		}

		mapInfo.mCurRoomId = 1;
		initMapScenes (mapInfo);

		return mapInfo;
	}

	//加入房间的prefab
	public void initMapScenes(mapinfo mapInfo){
		foreach (roominfo roomInfo in mapInfo.mRoomInfos) {
			SceneTemplate template = getSceneTemplate(roomInfo.mSceneIndex);
			if(roomInfo.mRoomType == constant.RoomType.Item){ 

				int itemNum = 1;
				List<Vector3> poss = new List<Vector3>();
				foreach(Vector3 v in template.BaseItemPos){
					poss.Add(v);
				}

				for(int i = 0; i < itemNum; ++i){
					itemtemplate item = constant.getItemFactory().getRandomTemplate(roomInfo, mapInfo);
					int index = Random.Range(0, poss.Count-1);
					Vector3 v = poss[index];
					poss.RemoveAt(index);
					roomInfo.addItemPrefabs(item, v);
				}
			}else if(roomInfo.mRoomType == constant.RoomType.Monster){
				int monsterNum = 1;
				List<Vector3> poss = new List<Vector3>();
				foreach(Vector3 v in template.BaseMonsterPos){
					poss.Add(v);
				}
				//Debug.Log("count:" + poss.Count);
				for(int i = 0; i < monsterNum; ++i){
					monstertemplate monster = constant.getMonsterFactory().getRandomTemplate(roomInfo, mapInfo);
					int index = Random.Range(0, poss.Count-1);
					Vector3 v = poss[index];
					poss.RemoveAt(index);
					roomInfo.addMonsterPrefabs(monster, v);
				}
			}
		}
	}

	public mapinfo getFirstMap()
	{
		mapinfo mapInfo = new mapinfo ();
		mapInfo.mRoomInfos = new List<roominfo> ();

		roominfo roomInfo = new roominfo ();
		roomInfo.roomH = 1;
		roomInfo.roomW = 1;
		roomInfo.roomX = 4;
		roomInfo.roomY = 3;

		roomInfo.mSceneIndex = 0;
		roomInfo.mId = 1;

		roomInfo.mDoorInfos = new List<doorinfo>();
		{
			doorinfo doorInfo = new doorinfo();
			doorInfo.mId = 1;
			doorInfo.mDir = constant.Direction.south;
			doorInfo.mNextDoorId = 2;
			doorInfo.mNextRoomId = 1;
			doorInfo.mX = 0.5f;
			doorInfo.mY = 1;
			roomInfo.mDoorInfos.Add(doorInfo);
		}
		{
			doorinfo doorInfo = new doorinfo();
			doorInfo.mId = 2;
			doorInfo.mDir = constant.Direction.north;
			doorInfo.mNextDoorId = 1;
			doorInfo.mNextRoomId = 1;
			doorInfo.mX = 0.5f;
			doorInfo.mY = 0;
			roomInfo.mDoorInfos.Add(doorInfo);
		}

		mapInfo.mRoomInfos.Add (roomInfo);

		mapInfo.mCurRoomId = 1;

		return mapInfo;
	}

	public roominfo getNextRoom(mapinfo mapInfo, doorinfo doorInfo)
	{
		return mapInfo.getNextRoom (doorInfo);
	}

	public float getDoorX(roominfo info , constant.Direction dir){
		switch (dir) {
		case constant.Direction.east:
			return 0;
			break;
		case constant.Direction.north:
			return 0.5f;
			break;
		case constant.Direction.south:
			return 0.5f;
			break;
		case constant.Direction.west:
			return 1;
			break;
		}
		return 0;
	}

	public float getDoorY(roominfo info , constant.Direction dir){
		switch (dir) {
		case constant.Direction.east:
			return 0.5f;
			break;
		case constant.Direction.north:
			return 0;
			break;
		case constant.Direction.south:
			return 1;
			break;
		case constant.Direction.west:
			return 0.5f;
			break;
		}
		return 0;
	}
}
