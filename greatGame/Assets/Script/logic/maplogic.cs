using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class maplogic : MonoBehaviour  {

	public mapinfo mMapInfo;
	public bool isFinishScene = false;
	public GameObject mDoor;
	GameObject[] mDoorSprs;
	
	public GameObject mCharPrefabs;		//主角prefab
	public GameObject mChar;
	
	public static bool mIsStartGame = false;

	public void resetStartGame(){
		mIsStartGame = false;
	}

	public void initCharPos(){
		roominfo roomInfo = mMapInfo.getCurRoom ();

		float x = 0;
		float y = 0;

		doorinfo doorInfo = roomInfo.getDoorInfo (roomInfo.mEnterDoorId);
		if (doorInfo != null) {
			switch (doorInfo.mDir) {
			case constant.Direction.east:
				x = 4;
				break;
			case constant.Direction.west:
				x = -4;
				break;
			case constant.Direction.south:
				y = -4;
				break;
			case constant.Direction.north:
				y = 4;
				break;
			}

			GameObject door = getDoorTouchObj (doorInfo.mId);
			
			
			if (door != null) {
				GameObject role = GameObject.FindGameObjectWithTag("Player");
				role.gameObject.transform.position = new Vector3 (door.transform.position.x+x, door.transform.position.y+y, -1);
			}
		}

	}

	public GameObject getDoorTouchObj(int doorId){
		List<string> doornames = new List<string>();
		doornames.Add ("UpRoom");
		doornames.Add ("DownRoom");
		doornames.Add ("LeftRoom");
		doornames.Add ("RightRoom");

		foreach (string name in doornames) {
			GameObject[] objs = GameObject.FindGameObjectsWithTag(name);
			foreach(GameObject obj in objs){
				room_property pro = obj.GetComponent<room_property>();
				if(pro.mDoorInfo.mId == doorId){
					return obj;
				}
			}
		}

		return null;
	}

	public void initRoomInfo(){

		Dictionary<string, constant.Direction> dic = new Dictionary<string, constant.Direction>();
		dic.Add ("UpRoom", constant.Direction.south);
		dic.Add ("DownRoom", constant.Direction.north);
		dic.Add ("LeftRoom", constant.Direction.east);
		dic.Add ("RightRoom", constant.Direction.west);

		List<doorinfo> doorinfos = mMapInfo.getCurRoom ().mDoorInfos;

		foreach(KeyValuePair<string, constant.Direction> pair in dic)
		{
			GameObject[] objs = GameObject.FindGameObjectsWithTag(pair.Key);
			if(objs != null){
				foreach(GameObject obj in objs){
					char_enter_script com = obj.GetComponent<char_enter_script>();
					com.mDir = pair.Value;

					room_property pro = obj.GetComponent<room_property>();
					Debug.Log ("pro:" + pro.mX + "," + pro.mY + "," + (pro.mDoorInfo));
					foreach(doorinfo info in doorinfos){
						Debug.Log ("info:" + info.mX + "," + info.mY);
						if(pro.mX == info.mX && pro.mY == info.mY){
							Debug.Log("add roominfo");
							pro.mDoorInfo = info;
							Debug.Log ("info:" + info);
							Debug.Log ("pro.mDoorInfo:" + pro.mDoorInfo.mX);
						}
					}
				}
			}

		}

	}

	public void initMapInfo(){
		mMapInfo = mapfactory.getFirstMap ();
	}

	void Start () {
	}

	public void startRoom(){
		if (!mIsStartGame) {			
			Debug.Log ("game start");			
			startMap ();
			mIsStartGame = true;
		}

		isFinishScene = true;
		mDoorSprs = GameObject.FindGameObjectsWithTag("normalDoors");
		mChar = GameObject.FindGameObjectWithTag("Player");
		
		initRoomInfo ();
		initCharPos ();
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (mDoorSprs == null) {
			return;
		}

		foreach (GameObject doorSpr in mDoorSprs) {
			BoxCollider collider = doorSpr.GetComponent<BoxCollider>();
			collider.isTrigger = true;
		}
		*/

		checkOpenDoor ();
	}
	
	void startGame(){
		
	}
	
	void startMap(){
		initMapInfo ();
		createChar ();
	}
	
	void createChar(){
		Vector3 v = new Vector3 ();
		v.x = -6.820158f;
		v.y = -0.5814921f;
		v.z = -1f;
		GameObject charClone = (GameObject)Instantiate(mCharPrefabs,v,Quaternion.identity);
	}
	
	public void enterRoom(GameObject door, GameObject role){
		doorinfo doorInfo = door.GetComponent<room_property>().mDoorInfo;
		Debug.Log ("enterRoom doorInfo:" + doorInfo.mX + ","
		           + doorInfo.mY + "," 
		           + doorInfo.mDir + ","
		           + doorInfo.mNextRoomId + ","
		           + doorInfo.mNextDoorId );

		role.gameObject.rigidbody.velocity = new Vector3 (0,0,0);
		//role.gameObject.transform.position = new Vector3 (-6, 0, -1);

		//char_enter_script enter = door.GetComponent<char_enter_script> ();
		//Debug.Log ("enterRoom dir:" + enter.mDir);
		 
		DontDestroyOnLoad (this.gameObject);
		DontDestroyOnLoad (role);
		//Application.LoadLevel(0);
		roominfo roomInfo = mapfactory.getNextRoom (mMapInfo, doorInfo);
		Application.LoadLevelAdditive(roomInfo.mSceneIndex);
	}

	public void openDoor(){
		GameObject[] doors = GameObject.FindGameObjectsWithTag ("normalDoors");
		//Debug.Log ("openDoor");
		foreach (GameObject door in doors) {
			tk2dSpriteAnimator ani = door.GetComponent<tk2dSpriteAnimator>();
			BoxCollider box = ani.gameObject.GetComponent<BoxCollider> ();
			if(!box.isTrigger){
				ani.Play("open");
				ani.AnimationCompleted = playOpenDoorAni;
			}
		}
	}

	public void playOpenDoorAni(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip){
		//Debug.Log ("playOpenDoorAni");
		BoxCollider box = animator.gameObject.GetComponent<BoxCollider> ();
		box.isTrigger = true;
	}

	public void checkOpenDoor(){
		GameObject[] enemys = GameObject.FindGameObjectsWithTag ("Enemy");
		//Debug.Log ("checkOpenDoor: " +  enemys.Length);
		if (enemys.Length == 0) {
			openDoor();
		}
	}

	public void showDamage(){

	}

}
