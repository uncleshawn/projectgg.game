using UnityEngine;
using System.Collections;

public class FallStoneLogic : MonoBehaviour {

        private GameObject mSprObj;
        private GameObject mShadowObj;
        private float mDestroyTime;
        private bool mHasFall;
        private bool mHasBroken;
        private float mFallTime;

        private bool mStartFall;
	// Use this for initialization
	void Start () {
                mSprObj = constant.getChildGameObject(this.gameObject, "Sprite");
                mShadowObj = constant.getChildGameObject(this.gameObject, "Shadow");

                mHasFall = false;
                mHasBroken = false;
                mDestroyTime = 0.5f;
                mFallTime = 1.0f;

                //mStartFall = false;
                startFall();
	}
	
	// Update is called once per frame
	void Update () {
                //if (!mStartFall) {
                //        startFall();
                //        mStartFall = true;
                //}
	}

        private void startFall() {
                {
                        Hashtable args = new Hashtable();

                        mShadowObj.transform.localScale = new Vector3(0.2f,0.2f,1);
                        args.Add("x", 1);
                        args.Add("y", 1);
                        args.Add("z", 1);

                        args.Add("time", mFallTime);
                        args.Add("easetype", iTween.EaseType.easeInExpo);

                        iTween.ScaleTo(mShadowObj, args);
                }

                {
                        Hashtable args = new Hashtable();
                        float y = mSprObj.transform.position.y;
                        mSprObj.transform.localPosition = new Vector3(0, 20, -5);

                        args.Add("x", mSprObj.transform.position.x);
                        args.Add("y", y);
                        args.Add("z", -5);

                        args.Add("time", mFallTime);
                        args.Add("easetype", iTween.EaseType.easeInExpo);

                        args.Add("onupdate", "fallUpdate");
                        args.Add("onupdatetarget", gameObject);
                        args.Add("oncomplete", "finishFall");
                        args.Add("oncompletetarget", gameObject);
                        iTween.MoveTo(mSprObj, args);
                }
        }

        private void fallUpdate() {
                //Debug.Log("fallUpdate:" + mSprObj.transform.localPosition.y);
                if (mSprObj.transform.localPosition.y < 5) {
                        mHasFall = true;
                }
        }

        private void finishFall() {
                mHasBroken = true;
                mSprObj.transform.localPosition = new Vector3(0, 0, 0.1f);

                tk2dSpriteAnimator ani = mSprObj.GetComponent<tk2dSpriteAnimator>();
                ani.Play();
                ani.AnimationCompleted = finishBrokenStone;

                constant.getSoundLogic().playEffect("fall_stone");
        }

        public void finishBrokenStone(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip) {
                InvokeRepeating("destoySelf", mDestroyTime, 0);  
        }

        public void destoySelf() {
                Destroy(this.gameObject);
        }

        void OnTriggerStay(Collider other) {
                if (!mHasFall) {
                        return;
                }
                if (mHasBroken) {
                        return;
                }
                maplogic mapLogic = constant.getMapLogic();
                mapLogic.triggerEnter(this.gameObject, other.gameObject);
        }
}
