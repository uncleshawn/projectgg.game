using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneTemplate {
	private int mSceneIndex;
	private List<Vector3> mBaseItemPos;
	private List<Vector3> mBaseMonsterPos;

	public int SceneIndex { get { return mSceneIndex; } set { mSceneIndex = value; }}
	public List<Vector3> BaseItemPos { get { return mBaseItemPos; } set { mBaseItemPos = value; }}
	public List<Vector3> BaseMonsterPos { get { return mBaseMonsterPos; } set { mBaseMonsterPos = value; }}
	
	public SceneTemplate(int sceneIndex){
		mSceneIndex = sceneIndex;
	}

	public SceneTemplate(int sceneIndex, List<Vector3> baseItemPos, List<Vector3> baseMonsterPos){
		mSceneIndex = sceneIndex;
		mBaseItemPos = baseItemPos;
		mBaseMonsterPos = baseMonsterPos;
	}
}
