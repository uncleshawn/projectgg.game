using UnityEngine;
using System.Collections;

public class enemyAniManager : MonoBehaviour {

		GameObject enemyUI;
		tk2dSprite enemySprite;
		MeshRenderer mesh;
		public AniDimension aniDimension; 
		public bool getShadow;
		public bool shadowParentUI;
		public bool dynamicShadow;
		public bool uniqueSetting;
		public float shadowPosY;
		public float shadowScaleY;
		tk2dSprite shadowSprite;

		bool aniStart;

		// Use this for initialization
		void Awake(){
				enemyUI = transform.FindChild ("ui").FindChild ("AnimatedSprite").gameObject;
				enemySprite = enemyUI.GetComponent<tk2dSprite> ();
				mesh = enemyUI.GetComponent<MeshRenderer> ();
				if (getShadow) {
						shadowSprite = intiShadow ();
				}
				aniStart = true;

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
		void FixedUpdate () {
				if (aniStart) {
				}
		}

		tk2dSprite intiShadow(){
				if (!uniqueSetting) {
						shadowPosY = 1;
						shadowScaleY = 0.3f;
				}
				GameObject shadowParent;
				if (shadowParentUI) {
						shadowParent = enemySprite.gameObject.transform.parent.gameObject;
				} else {
						shadowParent = enemySprite.gameObject;
				}

				GameObject shadow = constant.getMapLogic ().initBulletShadow (enemySprite , shadowParent, dynamicShadow);
				shadow.transform.localPosition = new Vector3 (0, -shadowPosY*Mathf.Abs(enemySprite.scale.y)/2, 1);
				tk2dSprite shadowSprite = shadow.GetComponent<tk2dSprite> ();
				shadowSprite.scale = new Vector3 (enemySprite.scale.x * 0.9f, enemySprite.scale.y * shadowScaleY, enemySprite.scale.z);
				return shadowSprite;
		}

		public void setAniSide(Direction direction){
				//Debug.Log ("动画方向: " + direction);
				switch (direction) {
				default:
						break;
				case Direction.right:
						enemySprite.transform.localScale = new Vector3 (1,1,1);
						break;
				case Direction.left:
						enemySprite.transform.localScale = new Vector3 (-1,1,1);
						break;
						break;
				}
				if (shadowParentUI&&getShadow) {
						switch (direction) {
						default:
								break;
						case Direction.right:
								shadowSprite.transform.localScale = new Vector3 (1,1,1);
								break;
						case Direction.left:
								shadowSprite.transform.localScale = new Vector3 (-1,1,1);
								break;
								break;
						}	
				}
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
