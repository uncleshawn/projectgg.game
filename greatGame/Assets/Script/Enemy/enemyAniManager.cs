using UnityEngine;
using System.Collections;

public class enemyAniManager : MonoBehaviour {

		GameObject enemyUI;
		tk2dSprite aniSprite;
		// Use this for initialization
		void Awake(){
				enemyUI = transform.FindChild ("ui").FindChild ("enemySprite").gameObject;
				aniSprite = enemyUI.GetComponent<tk2dSprite> ();
		}

		void Start () {
				if (enemyUI == null) {
						Debug.Log("敌人没有sprite object");
				}
				setSpriteColor (stateColor.normal);
		}

		// Update is called once per frame
		void Update () {

		}

		public void setSpriteColor(Color color){
				aniSprite.color = color;	
		}
}
