using UnityEngine;
using System.Collections;

public class char_enter_script : MonoBehaviour {

	public constant.Direction mDir;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter( Collider other ){
		//other.GetComponent
		//Debug.Log ("OnTriggerEnter "+other.gameObject.tag);
		//Debug.Log ("OnTriggerEnter " + other.gameObject.tag);
		if (other.gameObject.tag.Equals ("Player")) {
			//Debug.Log ("player enter");
			//other.gameObject.rigidbody.velocity = new Vector3(0,0,0);
			//DontDestroyOnLoad(other.gameObject);
			//Application.LoadLevel(0);
			room_property pro = this.gameObject.GetComponent<room_property>();
			//Debug.Log("pro:" + pro);
			//Debug.Log("pro.mRoomInfo:" + pro.mDoorInfo);
			if(pro != null && pro.mDoorInfo.mId != 0){
				constant.getMapLogic().enterRoom(this.gameObject, other.gameObject);
			}
		}

	}
}
