using UnityEngine;
using System.Collections;

public class followerlogic : monsterbaselogic {


		// Use this for initialization
		void Start () {
				//startWork();
		}

		// Update is called once per frame
		void Update () {
		}

		public void startWork(){
				Debug.Log("add equipment done!");
				iTween.MoveBy(gameObject, iTween.Hash("y", 1,  "easeType", "linear", "loopType", "pingpong" , "time" , 1 ,  "islocal" , true ));
		}
}
