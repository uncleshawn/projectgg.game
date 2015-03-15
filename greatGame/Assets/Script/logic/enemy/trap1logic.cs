using UnityEngine;
using System.Collections;

public class trap1logic : MonoBehaviour {


	private bool mIsOpen;
	private float mIntervalTime = 2;
	private float mTime = 0;

	// Use this for initialization
	void Start () {
		mIsOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
		mTime = mTime + Time.deltaTime;
		if (mTime > mIntervalTime) {
			changeStatus();
			mTime = 0;
		}
	}

	public void finishAni(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip){
		mIsOpen = !mIsOpen;
	}

	void OnTriggerStay(Collider collision){
		//Debug.Log ("OnTriggerStay:" + mIsOpen);
		if (mIsOpen) {
			constant.getMapLogic ().triggerEnter (this.gameObject, collision.gameObject);
		}
	}

	void onTriggerEnter(){
	}

	private void changeStatus(){
		bool isOpen = !mIsOpen;
		GameObject aniSpr = constant.getChildGameObject(this.gameObject, "AnimatedSprite");
		tk2dSpriteAnimator animator = aniSpr.GetComponent<tk2dSpriteAnimator>();
		animator.AnimationCompleted = finishAni; 
		tk2dSprite spr = aniSpr.GetComponent<tk2dSprite>();
		if(isOpen){
			spr.SetSprite("trap_03");
			animator.Play("trap1_open");
		}else{
			spr.SetSprite("trap_19");
			animator.Play("trap1_close");
		}
	}
}
