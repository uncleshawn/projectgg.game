using UnityEngine;
using System.Collections;

public class onceAniManager : MonoBehaviour {

		tk2dSpriteAnimator animator;
		// Use this for initialization
		void Awake(){
				animator = gameObject.GetComponent<tk2dSpriteAnimator> ();
		}

		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}



		public void setAniLib(string aniLibPath){
				
				Debug.Log ("播放一次性动画: " + aniLibPath);
				animator.Library =  Resources.LoadAssetAtPath(aniLibPath, typeof(tk2dSpriteAnimation)) as tk2dSpriteAnimation;
				
		}

		public void playAni(string aniName){
				constant.getSoundLogic ().playEffect ();

				animator.Play (aniName);
				animator.AnimationCompleted = afterAni;
		}

		void afterAni(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip){
				GameObject.Destroy(this.gameObject);	
		}
}
