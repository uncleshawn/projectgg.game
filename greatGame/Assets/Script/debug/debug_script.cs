using UnityEngine;
using System.Collections;

public class debug_script : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		maplogic logic = constant.getMapLogic ();
		roominfo info = logic.getCurRoom ();
		if (info == null) {
			return;
		}
		tk2dTextMesh text = this.gameObject.GetComponent<tk2dTextMesh> ();
		text.text = "" + info.mId;
	}
}
