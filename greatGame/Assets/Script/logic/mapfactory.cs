﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mapfactory {

	List<scenetemplateinfo> mSceneTemplateInfos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void initScenes()
	{
		mSceneTemplateInfos = new List<scenetemplateinfo>();

		int num = Application.levelCount;
		for(int i = 0; i < num ; ++i)
		{
			//DontDestroyOnLoad(this.gameObject);
			Application.LoadLevel(i);
		}
	}

	public mapinfo getNextMap()
	{
		return new mapinfo();
	}

	static public mapinfo getFirstMap()
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

	public static roominfo getNextRoom(mapinfo mapInfo, doorinfo doorInfo)
	{
		return mapInfo.getNextRoom (doorInfo);
	}
}
