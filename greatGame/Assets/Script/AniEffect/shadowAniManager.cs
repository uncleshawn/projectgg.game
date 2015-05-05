using UnityEngine;
using System.Collections;

public class shadowAniManager : MonoBehaviour {

		bool dynamicShadow;
		tk2dSprite targetSprite;
		tk2dSpriteAnimator targetAni;
		tk2dSprite selfSprite;
		tk2dSpriteAnimator selfAni;

		bool working;
		// Use this for initialization

		void Awake(){
				dynamicShadow = false;
				working = false;
				selfSprite = gameObject.GetComponent<tk2dSprite> ();
				selfAni = gameObject.GetComponent<tk2dSpriteAnimator> ();
		}

		void Start () {

		}

		// Update is called once per frame
		void FixedUpdate () {
				if (working&&dynamicShadow) {
						if (targetAni.Playing) {
								selfAni.Play (targetAni.CurrentClip);
								selfAni.PlayFromFrame (targetAni.CurrentFrame);
						} else {
								//Debug.Log ("影子无法捕捉目标动作");
						}
				}

		}

		public void setUp(tk2dSprite target , bool aniShadow){
				dynamicShadow = aniShadow;
				targetSprite = target;
				selfSprite.Collection = targetSprite.Collection;
				selfSprite.spriteId = targetSprite.spriteId;
				Color shadowColor = new Color (0, 0, 0);
				shadowColor.a = 0.5f;
				selfSprite.color = shadowColor;
				if (dynamicShadow == true) {
						targetAni = targetSprite.gameObject.GetComponent<tk2dSpriteAnimator>();
						selfAni.Library = targetAni.Library;
				}
				working = true;
		}

		public void shadowRotate(Vector3 rotation){
				transform.Rotate(rotation);
		}
}
