using UnityEngine;
using System.Collections;

public class monsterfactory {

		private int monsterId;

	private monstertemplate[] mPrefabs;

	private monsterfactory(){
		initTemplate ();
	}

	private static monsterfactory mInstance;
	static public monsterfactory getInstance(){
		if (mInstance == null) {
			mInstance = new monsterfactory();
		}
		return mInstance;
	}
		public int getMonsterId(){
				int oldMonsterId = monsterId;
				monsterId++;
				return oldMonsterId;

		}
	
	public void initTemplate(){
		mPrefabs = new monstertemplate[]{
			//new monstertemplate("Prefabs/enemy/bat"),
			new monstertemplate("Prefabs/enemy/enemy1"),
		};
	}
	
	public monstertemplate getRandomTemplate(roominfo roomInfo, mapinfo mapInfo){
		int index = Random.Range (0, mPrefabs.Length);
		return mPrefabs [index];
	}
}
