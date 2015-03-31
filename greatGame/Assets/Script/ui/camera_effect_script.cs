using UnityEngine;
using System.Collections;

public class camera_effect_script : MonoBehaviour {

		public bool cameraEffect;
		// Use this for initialization
		void Start () {
				shakeCamera ();
		}

		// Update is called once per frame
		void Update () {

		}

		void shakeCamera(){
				if (cameraEffect) {
						iTween.ShakePosition (gameObject, iTween.Hash ("x", 0.4, "y" , 0.4 , "time", 0.5, "loopType", "none", "oncomplete", "resetGameobject", "oncompletetarget", this.gameObject));
				}
		}
}
