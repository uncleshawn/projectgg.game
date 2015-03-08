using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class itemfactory {
	private itemtemplate[] mPrefabs;

	private static itemfactory mInstance;

	private itemfactory(){
		initTemplate ();
	}

	static public itemfactory getInstance(){
		if (mInstance == null) {
			mInstance = new itemfactory();
		}
		return mInstance;
	}

	public void initTemplate(){
		mPrefabs = new itemtemplate[]{
			new itemtemplate("Prefabs/item/item1"),
			new itemtemplate("Prefabs/item/item10"),
			new itemtemplate("Prefabs/item/item11"),
			new itemtemplate("Prefabs/item/item20"),
			new itemtemplate("Prefabs/item/item100"),
			new itemtemplate("Prefabs/item/item200"),
		};
	}

	public itemtemplate getRandomTemplate(roominfo roomInfo, mapinfo mapInfo){
		int index = Random.Range (0, mPrefabs.Length);
		return mPrefabs [index];
	}
}
