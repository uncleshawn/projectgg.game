using UnityEngine;
using System.Collections;

public class enemyAniManager : MonoBehaviour {

		GameObject enemyUI;
		tk2dSprite enemySprite;
		tk2dSpriteAnimator enemyAnimated;
		MeshRenderer mesh;
		public AniDimension aniDimension; 
		public bool getShadow;
		public bool shadowParentUI;
		public bool dynamicShadow;
		public bool uniqueSetting;
		public float shadowPosY;
		public float shadowScaleY;

		private GameObject shadowObj;
		private tk2dSprite shadowSprite;

		bool aniStart;

		// Use this for initialization
		void Awake(){
				enemyUI = transform.FindChild ("ui").FindChild ("AnimatedSprite").gameObject;
				enemySprite = enemyUI.GetComponent<tk2dSprite> ();
				enemyAnimated = enemyUI.GetComponent<tk2dSpriteAnimator> ();
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

				shadowObj = shadow;

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

		public void enemyDie(){
				enemyAnimated.Play ("defeat");
		}

		public void colorEffectHurt(){
				Color oldColor = enemySprite.color;
				//Debug.Log ("颜色:oldColor: " + oldColor );
				enemySprite.color = stateColor.hurt;
				//Debug.Log ("颜色:受伤Color: " + enemySprite.color );
				StartCoroutine (resetColor (oldColor));
		}


		IEnumerator resetColor(Color color){
				yield return new WaitForSeconds (0.16f);
				enemySprite.color = color;
		}

		public void setSpriteColor(Color color){
				enemySprite.color = color;	
		}

		public void settransparency(float alpha){
				Color alphaColor = enemySprite.color;
				alphaColor.a = alpha;
				enemySprite.color = alphaColor;
		}

		public GameObject getShadowObj() {
				return shadowObj;
		}
}
