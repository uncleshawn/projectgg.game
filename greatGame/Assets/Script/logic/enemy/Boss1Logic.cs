using UnityEngine;
using System.Collections;

public class Boss1Logic : enemylogic {


        private float mWalkTime = 2.0f;
        private float mInterWalkTime = 0.4f;
        private float mUseWalkTime = 0;

        private float mRestTime = 3;
        private float mUseTime = 0;


        private float mFollowTime = 3.0f;

        public enum Status { 
                Rest = 1,       //原地休息
                Walk = 2,       //小步走
                Sprint = 3,     //冲向主角
                Angry = 4,      //生气变大
                Follow = 5,     //跟随主角
                Jump = 6,
        }
        private Status mStatus;
        private float mAddX;
        private float mAddY;

        private float mWalkAcc;
        private float mWalkMaxSpeed;

        private float mSprintAcc;
        private float mSprintMaxSpeed;
        private float mAngrySprintMaxSpeed;

        private float mFollowAcc;
        private float mFollowMaxSpeed;

        private bool mHasAngry;
        private float mAngryPer;

        private bool mHasJump;
        private float mJumpTime;

        void Start() {
                mWalkAcc = 30;
                mWalkMaxSpeed = 2;

                mSprintAcc = 200;
                mSprintMaxSpeed = 10;
                mAngrySprintMaxSpeed = 40;

                mFollowAcc = 80;
                mFollowMaxSpeed = 8;

                mHasAngry = false;
                mAngryPer = 0.5f;

                mStatus = Status.Walk;

                mHasJump = false;
                mJumpTime = 1.0f;

                setWalkAI();
        }

        private Status getNextStatus() { 
                switch(mStatus){
                        case Status.Rest:
                                if (mHasAngry) {
                                        return Status.Sprint;
                                } else {
                                        if (Random.Range(0, 1.0f) < 0.5f) {
                                                return Status.Walk;
                                        } else {
                                                return Status.Follow;
                                        }
                                        //return Status.Follow;
                                }
                                break;
                        case Status.Walk:
                                //if (Random.Range(0, 1.0f) < 0.5f) {
                                //        return Status.Sprint;
                                //} else {
                                //        return Status.Jump;
                                //}
                                return Status.Sprint;
                                break;
                        case Status.Sprint:
                                return Status.Rest;
                                break;
                        case Status.Angry:
                                return Status.Sprint;
                        case Status.Follow:
                                return Status.Walk;
                                break;
                        case Status.Jump:
                                return Status.Rest;
                                break;
                }
                return Status.Walk;
        }
	
	// Update is called once per frame
	void Update () {
                switch (mStatus) {
                        case Status.Rest:
                                mUseTime = mUseTime - Time.deltaTime;
                                if (mUseTime <= 0) {
                                        changeNextStatus();
                                }
                                break;
                        case Status.Walk:
                                mUseTime = mUseTime - Time.deltaTime;
                                if (mUseTime <= 0) {
                                        changeNextStatus();
                                } else {
                                        mUseWalkTime = mUseWalkTime - Time.deltaTime;
                                        if (mUseWalkTime <= 0) {
                                                resetWalkDir();
                                        }
                                }

                                break;
                        case Status.Sprint:
                                break;
                        case Status.Angry:
                        case Status.Follow:
                                mUseTime = mUseTime - Time.deltaTime;
                                if (mUseTime <= 0) {
                                        changeNextStatus();
                                } else {
                                        updateFollowAcc();
                                }
                                break;
                }
	}

        public void changeNextStatus() {
                Status status = getNextStatus();
                changeToNextStatus(status);
        }

        public void setWalkAI() {
                GameObject obj = constant.getChildGameObject(this.gameObject, "AnimatedSprite");

                tk2dSpriteAnimator ani = obj.GetComponent<tk2dSpriteAnimator>();

                //spr.SetSprite("boss1_04");
                ani.Play("walk");

                resetWalkDir();

                mUseTime = mWalkTime;
        }

        public void resetWalkDir() {
                mUseWalkTime = mInterWalkTime;
                enemy_property pro = this.gameObject.GetComponent<enemy_property>();
                pro.BaseMoveSpeed = this.mWalkMaxSpeed;

                float d = Mathf.Deg2Rad * Random.Range(0, 359);

                mAddX = mWalkAcc * Mathf.Cos(d);
                mAddY = mWalkAcc * Mathf.Sin(d);
        }

        public void setRestAI() {
                GameObject obj = constant.getChildGameObject(this.gameObject, "AnimatedSprite");

                tk2dSpriteAnimator ani = obj.GetComponent<tk2dSpriteAnimator>();

                ani.Play("rest");

                mUseTime = mRestTime;

                enemy_property pro = this.gameObject.GetComponent<enemy_property>();
                pro.BaseMoveSpeed = 0;
        }

        public void setFollowAI() {
                GameObject player = constant.getPlayer();
                if (player == null) {
                        return;
                }

                enemy_property pro = this.gameObject.GetComponent<enemy_property>();
                pro.BaseMoveSpeed = this.mFollowMaxSpeed;

                GameObject obj = constant.getChildGameObject(this.gameObject, "AnimatedSprite");

                tk2dSpriteAnimator ani = obj.GetComponent<tk2dSpriteAnimator>();

                //spr.SetSprite("boss1_01");
                ani.Play("follow");
                updateFollowAcc();

                mUseTime = mFollowTime;
        }

        public void updateFollowAcc() {
                GameObject player = constant.getPlayer();
                float x = player.transform.position.x - this.transform.position.x;
                float y = player.transform.position.y - this.transform.position.y;

                SphereCollider collider = this.GetComponent<SphereCollider>();
                Debug.Log("collider.bounds.size:" + collider.bounds.size.x + "," + collider.bounds.size.y);
                Debug.Log("dis:" + x + "," + y);
                if (collider.bounds.size.x / 2 > Mathf.Abs(x)) {
                        x = 0;
                }
                if (collider.bounds.size.y / 2 > Mathf.Abs(y)) {
                        y = 0;
                }

                float d = Mathf.Atan2(y, x);
                //Debug.Log("X,Y,D:" + x + "," + y + "," + d);
                mAddX = mFollowAcc * Mathf.Cos(d);
                mAddY = mFollowAcc * Mathf.Sin(d);
        }

        public void stopAni() {
                GameObject obj = constant.getChildGameObject(this.gameObject, "AnimatedSprite");

                tk2dSpriteAnimator ani = obj.GetComponent<tk2dSpriteAnimator>();
                ani.Stop();
        }

        public void setJumpAI() {
                jumpToSky();
        }

        private void jumpToSky() {
                GameObject obj = constant.getChildGameObject(this.gameObject, "AnimatedSprite");

                GameObject shadowObj = constant.getChildGameObject(this.gameObject, "Shadow");
                tk2dSprite shadow = shadowObj.GetComponent<tk2dSprite>(); 
                
                {
                        float y = obj.transform.position.y;
                        Vector3 v = obj.transform.position;
                        v.z = -5;
                        obj.transform.position = v; //.z = 5;
                        Hashtable args = new Hashtable();
                        args.Add("y", y+30);

                        args.Add("time", mJumpTime);
                        //args.Add("easetype", iTween.EaseType.easeInCirc);
                        args.Add("oncomplete", "jumpMove");
                        args.Add("oncompletetarget", gameObject);
                        iTween.MoveTo(obj, args);
                }

                {
                        Hashtable args = new Hashtable();
                        args.Add("x", 0.2f);
                        args.Add("y", 0.2f);

                        args.Add("time", mJumpTime);
                        args.Add("easetype", iTween.EaseType.easeInCirc);

                        iTween.ScaleTo(shadowObj, args);
                }

                setIsTrigger(true);
                constant.getSoundLogic().playEffect("boss1_jump");
        }

        private void jumpMove() {
                GameObject player = constant.getPlayer();
                this.gameObject.transform.position = player.transform.position;
                jumpBackSky();
        }

        private void jumpBackSky() {
                GameObject obj = constant.getChildGameObject(this.gameObject, "AnimatedSprite");

                GameObject shadowObj = constant.getChildGameObject(this.gameObject, "Shadow");
                tk2dSprite shadow = shadowObj.GetComponent<tk2dSprite>();

                {
                        float y = obj.transform.position.y;
                        Hashtable args = new Hashtable();
                        args.Add("y", y - 30);

                        args.Add("time", mJumpTime);
                        args.Add("easetype", iTween.EaseType.easeInCubic);
                        args.Add("oncomplete", "finishJump");
                        args.Add("oncompletetarget", gameObject);
                        iTween.MoveTo(obj, args);
                }

                {
                        Hashtable args = new Hashtable();
                        args.Add("x", 1f);
                        args.Add("y", 1f);

                        args.Add("time", mJumpTime);
                        args.Add("easetype", iTween.EaseType.easeInCirc);

                        iTween.ScaleTo(shadowObj, args);
                }
        }

        private void finishJump() {
                GameObject obj = constant.getChildGameObject(this.gameObject, "AnimatedSprite");

                GameObject shadowObj = constant.getChildGameObject(this.gameObject, "Shadow");
                tk2dSprite shadow = shadowObj.GetComponent<tk2dSprite>();

                Vector3 v = obj.transform.localPosition;
                v.z = 0;
                obj.transform.localPosition = v;

                constant.getSoundLogic().playEffect("boss1_fall");
                setIsTrigger(false);
                scaleBoxCollider();
                changeNextStatus();
        }

        public void setAngryAI() {

                mHasAngry = true;

                GameObject obj = constant.getChildGameObject(this.gameObject, "AnimatedSprite");

                tk2dSpriteAnimator ani = obj.GetComponent<tk2dSpriteAnimator>();

                ani.Play("angry");

                GameObject ui = constant.getChildGameObject(this.gameObject, "ui");
                //iTween.ScaleTo(ui, new Vector3(3, 3, 1), 0.5f);

                Hashtable args = new Hashtable();
                args.Add("x", 3);
                args.Add("y", 3);
                args.Add("z", 1);

                args.Add("time", 1.5f);
                args.Add("easetype", iTween.EaseType.easeInBounce);
                args.Add("oncomplete", "AnimationEnd");
                args.Add("oncompletetarget", gameObject);
                iTween.ScaleTo(ui, args);

                //播放生气音效
                soundLogic soundLogic = constant.getSoundLogic();
                soundLogic.playEffect("boss1_angry");

                //摄像机晃动
                maplogic logic = constant.getMapLogic();
                logic.shakeCamera();
        }

        public void AnimationEnd() {
                Debug.Log("AnimationEnd");
                //碰撞体变大
                SphereCollider collider = this.gameObject.GetComponent<SphereCollider>();
                //collider.size.Set(4,4,1);
                //collider.size = new Vector3(2.5,4,1);
                collider.radius = 1.5f*1.5f;
                changeNextStatus();
        }

        public void setSprintAI() {
                GameObject player = constant.getPlayer();
                if (player == null) {
                        return;
                }

                enemy_property pro = this.gameObject.GetComponent<enemy_property>();
                pro.BaseMoveSpeed = this.mSprintMaxSpeed;
                if (this.mHasAngry) {
                        pro.BaseMoveSpeed = this.mAngrySprintMaxSpeed;
                }

                GameObject obj = constant.getChildGameObject(this.gameObject, "AnimatedSprite");

                tk2dSpriteAnimator ani = obj.GetComponent<tk2dSpriteAnimator>();

                //spr.SetSprite("boss1_01");
                ani.Play("sprint");

                float x = player.transform.position.x - this.transform.position.x;
                float y = player.transform.position.y - this.transform.position.y;

                SphereCollider collider = this.GetComponent<SphereCollider>();
                Debug.Log("collider.bounds.size:" + collider.bounds.size.x + "," + collider.bounds.size.y);
                Debug.Log("dis:" + x + "," + y);
                if (collider.bounds.size.x / 2 > Mathf.Abs(x)) {
                        x = 0;
                }
                if (collider.bounds.size.y / 2 > Mathf.Abs(y)) {
                        y = 0;
                }

                float d = Mathf.Atan2(y, x);
                //Debug.Log("X,Y,D:" + x + "," + y + "," + d);
                mAddX = mSprintAcc * Mathf.Cos(d);
                mAddY = mSprintAcc * Mathf.Sin(d);

                //Debug.Log("mAddX,mAddY:" + mAddX + "," + mAddY);
        }

        public void changeToNextStatus(Status status) {
                mStatus = status;
                //Debug.Log("changeToNextStatus：" + mStatus);
                stopAni();
                stopMove();
                switch (mStatus) {
                        case Status.Rest:
                                setRestAI();
                                break;
                        case Status.Walk:
                                setWalkAI();
                                break;
                        case Status.Sprint:
                                setSprintAI();
                                break;
                        case Status.Angry:
                                setAngryAI();
                                break;
                        case Status.Follow:
                                setFollowAI();
                                break;
                        case Status.Jump:
                                setJumpAI();
                                break;
                }
        }

        override public Vector3 getMoveAcc() {
                Vector3 v = new Vector3();
                v.x = 0;
                v.y = 0;
                v.z = 0;

                switch (mStatus) {
                        case Status.Rest:
                                break;
                        case Status.Walk:
                                v.x = mAddX;
                                v.y = mAddY;
                                break;
                        case Status.Sprint:
                                v.x = mAddX;
                                v.y = mAddY;
                                break;
                        case Status.Angry:
                        case Status.Follow:
                                v.x = mAddX;
                                v.y = mAddY;
                                break;
                }

                return v;
        }

        override public void beAttack(GameObject obj) {
                if (mStatus == Status.Angry) { 
                        return;
                }
 
                base.beAttack(obj);
                if (!mHasAngry) {
                        checkAngry();
                }
        }

        public void checkAngry() {
                enemy_property pro = this.gameObject.GetComponent<enemy_property>();
                float curPer = ((float)pro.Hp)/pro.MaxHp;
                if (curPer <= mAngryPer) {
                        changeToNextStatus(Status.Angry);
                }
        }

        private void OnCollisionEnter(Collision collision) {
                Debug.Log("OnCollisionEnter:" + collision.gameObject.name);
                onCollision(collision);
        }

        private void OnCollisionExit(Collision collision) {
                Debug.Log("OnCollisionEnter:" + collision.gameObject.name);
                //onCollision(collision);
                stopMove();

        }

        //void OnCollisionStay(Collision collision) {
        //        onCollision(collision);
        //}

        private void onCollision(Collision collision) {
                if (collision.gameObject.tag == constant.TAG_PLAYER) {
                        pushPlayer();
                }
                if (mStatus == Status.Sprint) {
                        constant.getSoundLogic().playEffect("boss1_attack");
                        startFall();
                        changeNextStatus();
                } else if (mStatus == Status.Follow) {
                        constant.getSoundLogic().playEffect("boss1_attack");
                        changeNextStatus();
                }

        }

        private void pushPlayer() {
                Debug.Log("pushPlayer");
                GameObject player = constant.getPlayer();

                Vector3 v = player.transform.position - this.gameObject.transform.position;
                float force = 2000f;

                player.transform.rigidbody.AddForce(v.normalized * force);
        }

        private void startFall() {
                int num = 5;
                for (int i = 0; i < 5; ++i) {
                        float deltaTime = Random.Range(0,1.0f);
                        InvokeRepeating("fallStone", deltaTime, 0);  
                }
        }

        private void setIsTrigger(bool ret) {
                SphereCollider collider = this.gameObject.GetComponent<SphereCollider>();
                collider.isTrigger = ret;
        }

        private void scaleBoxCollider() {
                SphereCollider collider = this.gameObject.GetComponent<SphereCollider>();

                //collider.size = new Vector3(0, 0, 1);
                collider.radius = 0;

                InvokeRepeating("finshScaleBoxCollider", 0f, 0.01f);
        }

        private void finshScaleBoxCollider() {
                SphereCollider collider = this.gameObject.GetComponent<SphereCollider>();
                float x = collider.radius;
                float maxX = 1.5f;
                if (mHasAngry) {
                        maxX = 1.5f * 1.5f;
                }
                x = x + 0.1f;
                if (x > maxX) {
                        x = maxX;
                        CancelInvoke("finshScaleBoxCollider");
                }
                collider.radius = x;
        }

        private void fallStone() {
                Rect rect = constant.getFloorRect();
                //Debug.Log("fallStone:" + rect.xMin + "," + rect.xMax + "," + rect.yMin + "," + rect.yMax);
                Vector2 v2 = constant.getRandomPosInFloor();
                Vector3 v = new Vector3(v2.x, v2.y, -1);
                string fallStonePrefabStr = "Prefabs/scene/fallstone";
                GameObject clone = (GameObject)GameObject.Instantiate(Resources.Load(fallStonePrefabStr), v, Quaternion.identity);

                BoxCollider boxCollider = clone.GetComponent<BoxCollider>();
                float w = boxCollider.bounds.size.x;
                float h = boxCollider.bounds.size.y;
                if (v.x - w / 2 < rect.xMin) {
                        v.x = rect.xMin + w / 2;
                } else if (v.x + w / 2 > rect.xMax) {
                        v.x = rect.xMax - w / 2;
                }

                if (v.y - h / 2 < rect.yMin) {
                        v.y = rect.yMin + h / 2;
                } else if (v.y + h / 2 > rect.yMax) {
                        v.y = rect.yMax - h / 2;
                }
                //clone.transform.position = v;
        }
}
