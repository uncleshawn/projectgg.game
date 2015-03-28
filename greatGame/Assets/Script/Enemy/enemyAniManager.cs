using UnityEngine;
using System.Collections;

public class enemyAniManager : MonoBehaviour {

		GameObject enemyUI;
		tk2dSprite enemySprite;
		MeshRenderer mesh;

		public bool getShadow;
		public bool dynamicShadow;
		tk2dSprite shadowSprite;


		// Use this for initialization
		void Awake(){
				enemyUI = transform.FindChild ("ui").FindChild ("enemySprite").gameObject;
				enemySprite = enemyUI.GetComponent<tk2dSprite> ();
				mesh = enemyUI.GetComponent<MeshRenderer> ();
				if (getShadow) {
						shadowSprite = intiShadow ();
				}
		}

		void Start () {
				if (enemyUI == null) {
						Debug.Log("敌人没有sprite object");
				}
				setSpriteColor (stateColor.normal);

				//renderer.material.color = c;
				//settransparency (0.5f);
		}

		// Update is called once per frame
		void Update () {

		}

		tk2dSprite intiShadow(){
				GameObject shadow = constant.getMapLogic ().initBulletShadow (enemySprite , enemySprite.gameObject.transform.parent.gameObject , dynamicShadow);
				shadow.transform.localPosition = new Vector3 (0, -1.2f, 1);
				tk2dSprite shadowSprite = shadow.GetComponent<tk2dSprite> ();
				shadowSprite.scale = new Vector3 (enemySprite.scale.x * 0.9f, enemySprite.scale.y * 0.2f, enemySprite.scale.z);
				return shadowSprite;
		}


		public void setSpriteColor(Color color){
				enemySprite.color = color;	
		}

		public void settransparency(float alpha){
				Color alphaColor = enemySprite.color;
				alphaColor.a = alpha;
				enemySprite.color = alphaColor;
		}
}
