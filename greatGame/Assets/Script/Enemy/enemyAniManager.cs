using UnityEngine;
using System.Collections;

public class enemyAniManager : MonoBehaviour {

		GameObject enemyUI;
		tk2dSprite aniSprite;
		MeshRenderer mesh;
		// Use this for initialization
		void Awake(){
				enemyUI = transform.FindChild ("ui").FindChild ("enemySprite").gameObject;
				aniSprite = enemyUI.GetComponent<tk2dSprite> ();
				mesh = enemyUI.GetComponent<MeshRenderer> ();
		}

		void Start () {
				if (enemyUI == null) {
						Debug.Log("敌人没有sprite object");
				}
				setSpriteColor (stateColor.normal);

				//renderer.material.color = c;
				settransparency (0.5f);
		}

		// Update is called once per frame
		void Update () {

		}

		public void setSpriteColor(Color color){
				aniSprite.color = color;	
		}

		public void settransparency(float alpha){
				Color alphaColor = aniSprite.color;
				alphaColor.a = alpha;
				aniSprite.color = alphaColor;
		}
}
