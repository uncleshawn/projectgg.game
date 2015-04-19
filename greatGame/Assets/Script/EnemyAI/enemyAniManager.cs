using UnityEngine;
using System.Collections;

public class enemyAniManager : MonoBehaviour {

		//怪物动画物体
		GameObject enemyUI;
		//怪物动画精灵
		tk2dSprite enemySprite;
		//怪物动画控制器
		tk2dSpriteAnimator enemyAnimated;
		//怪物动画的mesh
		MeshRenderer meshRenderer;
		//怪物动画方向数(2或者4)
		public AniDimension aniDimension; 
		//是否有影子
		public bool getShadow;
		//影子的父节点是否是UI
		public bool shadowParentUI;
		//是否是动态影子
		public bool dynamicShadow;
		//影子的属性是否特殊处理
		public bool uniqueSetting;
		//影子相对位置Y 越大越低
		public float shadowPosY;
		//影子Y轴缩放
		public float shadowScaleY;
		//影子的gameobject
		private GameObject shadowObj;
		//影子动画sprite
		private tk2dSprite shadowSprite;

		bool aniStart;

		// Use this for initialization
		void Awake(){
				enemyUI = transform.FindChild ("ui").FindChild ("AnimatedSprite").gameObject;
				enemySprite = enemyUI.GetComponent<tk2dSprite> ();
				enemyAnimated = enemyUI.GetComponent<tk2dSpriteAnimator> ();
				meshRenderer = enemyUI.GetComponent<MeshRenderer> ();
				if (getShadow) {
						shadowSprite = intiShadow ();
				}
				aniStart = true;

		}

		void Start () {
				if (enemyUI == null) {
						Debug.Log("敌人没有sprite object");
				}
				setSpriteColor (StateColor.normal);

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

<<<<<<< HEAD
                                shadow.transform.localPosition = new Vector3 (0, -shadowPosY*Mathf.Abs(enemySprite.scale.y)/2, 1);
                                shadow.transform.localScale = enemySprite.gameObject.transform.localScale;
                                tk2dSprite shadowSprite = shadow.GetComponent<tk2dSprite> ();
=======
				shadow.transform.localPosition = new Vector3 (0, -shadowPosY*Mathf.Abs(enemySprite.scale.y)/2, 0.5f);
				tk2dSprite shadowSprite = shadow.GetComponent<tk2dSprite> ();
>>>>>>> e573b79dce18f29ac143136d9a35986c0997fddb
				shadowSprite.scale = new Vector3 (enemySprite.scale.x * 0.9f, enemySprite.scale.y * shadowScaleY, enemySprite.scale.z);
				return shadowSprite;
		}

		public void setAniSide(Direction direction){
				//Debug.Log ("动画方向: " + direction);
				switch (direction) {
				default:
						break;
				case Direction.right:
						enemySprite.transform.localScale = new Vector3 (-1,1,1);
						break;
				case Direction.left:
						
						enemySprite.transform.localScale = new Vector3 (1,1,1);
						break;
						break;
				}
				if (shadowParentUI&&getShadow) {
						switch (direction) {
						default:
								break;
						case Direction.right:
								shadowSprite.transform.localScale = new Vector3 (-1,1,1);
								break;
						case Direction.left:
								shadowSprite.transform.localScale = new Vector3 (1,1,1);
								break;
								break;
						}	
				}
		}

		public void playAni(string aniName) {
				enemyAnimated.Play (aniName);
		}

		public void stopAni(){
				enemyAnimated.Stop ();
		}

		public void playSameAni(string aniName){
				int aniFrames = enemyAnimated.CurrentFrame;
				//Debug.Log ("正在播放第" + aniFrames + "frame");
				if (!enemyAnimated.IsPlaying (aniName)) {
						
						enemyAnimated.Play (aniName);
						//enemyAnimated.SetFrame (aniFrames);
						enemyAnimated.PlayFromFrame (aniFrames);

						//Debug.Log ("正在播放第" + aniFrames + "frame");
				} else {
						//Debug.Log ("正在播放同一动画");
				}

		}

		public void enemyDie(){
				enemyAnimated.Play ("defeat");
		}

		public void colorEffectHurt(){
				//Debug.Log ("颜色:oldColor: " + oldColor );
				enemySprite.color = StateColor.hurt;
				//Debug.Log ("颜色:受伤Color: " + enemySprite.color );
				Color stateColor = constant.getMapLogic().getStateColor(gameObject);
				StartCoroutine (resetColor () );
		}
		public void colorSlowDown(){
				enemySprite.color = StateColor.slowDown;
		}

		public void resetStateColor(){
				Color stateColor = constant.getMapLogic().getStateColor(gameObject);
				enemySprite.color = stateColor;
		}


		IEnumerator resetColor(){
				yield return new WaitForSeconds (0.16f);
				resetStateColor ();
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
