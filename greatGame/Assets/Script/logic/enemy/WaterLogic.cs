using UnityEngine;
using System.Collections;

public class WaterLogic : MonoBehaviour {

        protected float mScaleX;
        protected float mScaleY;
        protected float mScaleTime;

        protected float mDisappearTime;
        protected float mStartDisappearTime;
        protected float mFadeOutTime;

        public float ScaleX { get { return mScaleX; } set { mScaleX = value; } }
        public float ScaleY { get { return mScaleY; } set { mScaleY = value; } }


        void Awake() {

                mScaleX = 2.0f;
                mScaleY = 2.0f;

                mScaleTime = 1.0f;
                mDisappearTime = 6.0f;
                mStartDisappearTime = mDisappearTime;

                mFadeOutTime = 1.0f;
        }

	// Use this for initialization
	void Start () {
                startScale();
	}
	
	// Update is called once per frame
	void Update () {
                if (mStartDisappearTime > 0) {
                        mStartDisappearTime = mStartDisappearTime - Time.deltaTime;
                        if (mStartDisappearTime <= 0) {
                                startDisappear();
                        }
                }
	}

        protected void startDisappear() {
                Debug.Log("startDisappear");
                GameObject sprObj = constant.getChildGameObject(this.gameObject, constant.TAG_ANIMATEDSPRITE);

                //Hashtable args = new Hashtable();

                //args.Add("color", new Color(1.0f,1.0f,1.0f,0));
                //args.Add("time", mFadeOutTime);
                //args.Add("easetype", iTween.EaseType.linear);

                //args.Add("oncomplete", "destroySelf");
                //args.Add("oncompletetarget", gameObject);

                //iTween.ColorTo(spr, args);

                iTween.ValueTo(sprObj, iTween.Hash("from", 1, "to", 0, "time", mFadeOutTime, 
                        "onupdate", "OnUpdateTween", "onupdatetarget",this.gameObject,
                        "oncomplete", "destroySelf", "oncompletetarget", this.gameObject));
        }

        void OnUpdateTween(float value) {
                GameObject sprObj = constant.getChildGameObject(this.gameObject, constant.TAG_ANIMATEDSPRITE);
                tk2dBaseSprite spr = sprObj.GetComponent<tk2dBaseSprite>();
                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, value);
        }

        protected void destroySelf() {
                Destroy(this.gameObject);
        }

        protected void startScale() {
                GameObject ui = constant.getChildGameObject(this.gameObject, "ui");

                ui.transform.localScale = new Vector3(0, 0, 1);

                Hashtable args = new Hashtable();
                args.Add("x", mScaleX);
                args.Add("y", mScaleY);

                args.Add("time", mScaleTime);
                args.Add("easetype", iTween.EaseType.easeOutQuart);

                iTween.ScaleTo(ui, args);

        }


        public virtual void onTriggerEnter(Collider other) {
        }

        public virtual void onTriggerStay(Collider other) {

        }

        public virtual void onTriggerExit(Collider other) {

        }
}
