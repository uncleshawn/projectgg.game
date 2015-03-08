using UnityEngine;
using System.Collections;

public class itemtemplate {
	private string mPrefabPath;
	public string PrefabPath { get { return mPrefabPath; } }	

	public itemtemplate(string prefabPath){
		mPrefabPath = prefabPath;
	}
}
 