using UnityEngine;
using System.Collections;

public class bulletEnforce_Property : MonoBehaviour {


		public ElementType elementType;
		public bool bulletPierce;
		public KnockType knockType;
		public bulletSpeStruct bulletSpe;

		// Use this for initialization
		void Awake(){
				checkForget ();
				bulletSpe.element = elementType;
				bulletSpe.pierceBullet = bulletPierce;
				bulletSpe.knockType = knockType;
		}

		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		void checkForget(){
				if (elementType == 0 && bulletPierce == false && knockType == 0) {
						Debug.Log ("子弹特殊效果道具未初始化, 请修复!");
				}
		}
}
