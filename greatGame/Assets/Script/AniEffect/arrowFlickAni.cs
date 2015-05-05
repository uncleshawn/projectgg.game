using UnityEngine;
using System.Collections;

public class arrowFlickAni : MonoBehaviour {

		float distance = 10;

		tk2dSprite selfSprite;
		tk2dSpriteAnimator aniManager;
		Vector3 mOriginDir;
		public Vector3 OriginDir { get { return mOriginDir; } set { mOriginDir = value; }}

		bool startWork;
		float lerp;
		Vector3 originPos;
		Vector3 targetPos;
		Color nextAlpha;
		float fadeTimeDelta = 0.6f;
		float fadeTime = 0.6f;
		void Awake(){
				selfSprite =  GetComponent<tk2dSprite> ();
				aniManager = GetComponent<tk2dSpriteAnimator> ();
		}

		// Use this for initialization
		void Start () {
				startAni ();

		}

		// Update is called once per frame
		void FixedUpdate () {
				if(startWork){
						fadeTimeDelta = fadeTimeDelta - Time.deltaTime;
						colorFade (fadeTimeDelta/fadeTime);
				}
		}

		void startAni(){
				//startWork = true;
				originPos = transform.position;
				//Debug.Log ("mOriginDir: " + mOriginDir);
				Quaternion rot;
				int randomAngel;
				if (Random.Range (0, 2) == 0) {
						randomAngel = Random.Range (-85, -65);
				} else {
						randomAngel = Random.Range (65, 86);
				}
				rot = Quaternion.Euler (0f, 0f, randomAngel);
				mOriginDir = rot * mOriginDir;
				//Debug.Log ("mOriginDir: " + mOriginDir);
				targetPos = originPos - mOriginDir.normalized * 2.5f;
				//Debug.Log ("origin position: " + originPos);
				//Debug.Log ("targetPos position: " + targetPos);


				aniManager.Play ("arrowFlick");
				aniManager.AnimationCompleted = AnimationCompleteDelegate;
				aniManager.AnimationEventTriggered = flickRandomDir;
		}

		void AnimationCompleteDelegate(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip){
				GameObject.Destroy(this.gameObject);
		}

		void flickRandomDir(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip,  int frameNum) {
				startWork = true;
				iTween.MoveTo (gameObject, iTween.Hash ("position", targetPos , "easeType", "linear", "loopType", "none", "time", 0.6f));
				//iTween.FadeTo ();
		}

		void colorFade(float colorAlpha){
				Color nextAlpha = new Color (selfSprite.color.r, selfSprite.color.g,selfSprite.color.b);
				nextAlpha.a = colorAlpha;
				selfSprite.color = nextAlpha;
		}
}
