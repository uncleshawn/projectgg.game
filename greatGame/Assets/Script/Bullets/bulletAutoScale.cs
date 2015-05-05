using UnityEngine;
using System.Collections;

public class bulletAutoScale : MonoBehaviour {

		public float scaleX;
		public float scaleY;

		// Use this for initialization
		void Start () {
				if (scaleX == 0 && scaleY == 0) {
						scaleX = 1.6f;
						scaleY = 1.6f;
				}
				//iTween.MoveTo(gameObject, iTween.Hash("position", ,  "easeType", "linear", "loopType", "none" , "time" , 2 , "oncomplete" , "finishMove" , "oncompletetarget" , this.gameObject ));
				iTween.ScaleTo (gameObject, iTween.Hash( "x" , 2f , "y" , 2f, "easeType", "linear", "loopType", "none" , "time" , 2f));
		}

		// Update is called once per frame
		void Update () {

		}
}
