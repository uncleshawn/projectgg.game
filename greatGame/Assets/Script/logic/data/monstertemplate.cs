using UnityEngine;
using System.Collections;

public class monstertemplate {
	private string mPrefabPath;
	public string PrefabPath { get { return mPrefabPath; } }	
	
	public monstertemplate(string prefabPath){
		mPrefabPath = prefabPath;
	}

}
