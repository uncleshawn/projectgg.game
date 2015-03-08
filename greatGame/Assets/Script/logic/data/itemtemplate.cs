using UnityEngine;
using System.Collections;

public class itemtemplate {
	private string mPrefabPath;
	public string PrefabPath { get { return mPrefabPath; } }	

	private int mId;
	public int Id { get { return mId; } }

	public itemtemplate(string prefabPath){
		mPrefabPath = prefabPath;
		mId = int.Parse(mPrefabPath.Replace ("Prefabs/item/item", ""));
	}
}
 