using UnityEngine;
using System.Collections;

public class robotAniManager : MonoBehaviour {

		// Use this for initialization

		//new animation system
		Direction moveDir;
		Direction shotDir;
		Direction oldMoveDir;
		Direction moveShotDir;
		GameObject playerPic;
		tk2dSpriteAnimator playerAni;
		tk2dSprite playerSprite;
		tk2dSpriteAnimator playerUpperAni;
		tk2dSprite playerUpperSprite;

		private float mWUDIInterval = 0.1f;

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
		GameObject upperShadowObj;
		//影子动画sprite
		private tk2dSprite shadowSprite;
		tk2dSprite upperShadowSprite;

		bool standShotAniPlaying;
		bool walkShotAniPlaying;


		void Awake(){
				playerPic = transform.FindChild ("ui").FindChild ("AnimatedSprite").gameObject;

				playerAni = playerPic.GetComponent<tk2dSpriteAnimator> ();

				playerSprite = playerPic.GetComponent<tk2dSprite> ();

				playerUpperSprite = transform.FindChild ("ui").FindChild ("upperBody").gameObject.GetComponent<tk2dSprite> ();
				playerUpperAni =  transform.FindChild ("ui").FindChild ("upperBody").gameObject.GetComponent<tk2dSpriteAnimator> ();

				if (getShadow) {
						intiShadow ();
				}
		}

		void Start () {
				moveDir = Direction.down;
				shotDir = Direction.down;
				oldMoveDir = Direction.down;
				moveDir = Direction.none;
				moveShotDir = Direction.down;
				playerUpperSprite.renderer.enabled = false;
				upperShadowSprite.renderer.enabled = false;

		}

		// Update is called once per frame

		void FixedUpdate(){
				setCurrentDirection ();
				setPlayerAnimation ();
		}

		//生成影子
		void intiShadow(){
				if (!uniqueSetting) {
						shadowPosY = 1;
						shadowScaleY = 0.3f;
				}

				GameObject shadowParent;
				GameObject upperShadowParent;

				if (shadowParentUI) {
						shadowParent = playerPic.gameObject.transform.parent.gameObject;
						upperShadowParent = playerPic.gameObject.transform.parent.gameObject;
				} else {
						shadowParent = playerPic.gameObject;
						upperShadowParent = transform.FindChild ("ui").FindChild ("upperBody").gameObject;
				}




				GameObject shadow = constant.getMapLogic ().initBulletShadow (playerSprite , shadowParent, dynamicShadow);
				GameObject upperShadow = constant.getMapLogic ().initBulletShadow (playerUpperSprite , shadowParent, dynamicShadow);

				shadowObj = shadow;
				upperShadowObj = upperShadow;
				//shadow.transform.localPosition = new Vector3 (0, -shadowPosY*Mathf.Abs(enemySprite.scale.y)/2, 1);
				//shadow.transform.localScale = enemySprite.gameObject.transform.localScale;
				//tk2dSprite shadowSprite = shadow.GetComponent<tk2dSprite> ();

				shadow.transform.localPosition = new Vector3 (0, -shadowPosY*Mathf.Abs(playerSprite.scale.y)/2, 0.5f);
				shadowSprite = shadow.GetComponent<tk2dSprite> ();

				upperShadow.transform.localPosition = new Vector3 (0, -shadowPosY*Mathf.Abs(playerSprite.scale.y)/2, 0.5f);
				upperShadowSprite = upperShadow.GetComponent<tk2dSprite> ();

				shadowSprite.scale = new Vector3 (playerSprite.scale.x * 0.9f, playerSprite.scale.y * shadowScaleY, playerSprite.scale.z);
				upperShadowSprite.scale = new Vector3 (playerSprite.scale.x * 0.9f, playerSprite.scale.y * shadowScaleY, playerSprite.scale.z);



		} 

		void setCurrentDirection(){
				//Debug.Log ("input: " + Input.GetAxis ("Vertical") + "   " + Input.GetAxis ("Horizontal"));
				//行走方向
				if(Input.GetAxis ("Vertical") >= 0.3f && Mathf.Abs(Input.GetAxis ("Horizontal")) < 0.3f )
				{
						moveDir = Direction.up;
						oldMoveDir = moveDir;
				}
				if(Input.GetAxis ("Vertical") <= -0.3f && Mathf.Abs(Input.GetAxis ("Horizontal")) < 0.3f )
				{
						moveDir = Direction.down;
						oldMoveDir = moveDir;
				}
				if(Input.GetAxis ("Horizontal") >= 0.3f && Mathf.Abs(Input.GetAxis ("Vertical")) < 0.3f )
				{
						moveDir = Direction.right;
						oldMoveDir = moveDir;
				}		
				if(Input.GetAxis ("Horizontal") <= -0.3f && Mathf.Abs(Input.GetAxis ("Vertical")) < 0.3f )
				{
						moveDir = Direction.left;
						oldMoveDir = moveDir;
				}
				if (Mathf.Abs (Input.GetAxis ("Vertical")) < 0.3f && Mathf.Abs (Input.GetAxis ("Horizontal")) < 0.3f) {
						moveDir = Direction.none;
				}
				if( Mathf.Abs (Input.GetAxis ("Vertical")) >= 0.6f && Mathf.Abs (Input.GetAxis ("Horizontal")) >= 0.6f ){
						if (moveDir == Direction.none) {
								if (Input.GetAxis ("Horizontal") >= 0) {
										moveDir = Direction.right;
										oldMoveDir = moveDir;	
								}
								else{
										moveDir = Direction.left;
										oldMoveDir = moveDir;	
								}
						}
				}


				//射击方向
				if(Input.GetAxis ("shotHorizontal") >= 0.6f)
				{
						shotDir = Direction.right;
						oldMoveDir = shotDir;
				}		
				if(Input.GetAxis ("shotHorizontal") <= -0.6f)
				{
						shotDir = Direction.left;
						oldMoveDir = shotDir;

				}
				if (Input.GetAxis ("shotVertical") >= 0.6f) {
						shotDir = Direction.up;
						oldMoveDir = shotDir;
				} 
				if(Input.GetAxis ("shotVertical") <= -0.6f)
				{ 
						shotDir = Direction.down;
						oldMoveDir = shotDir;
				}
				if (Mathf.Abs (Input.GetAxis ("shotHorizontal")) <= 0.6f && Mathf.Abs (Input.GetAxis ("shotVertical")) <= 0.6f) {
						shotDir = Direction.none;
				}
		}

		//人物左右动画翻转
		public void setAniSide(Direction direction){
				//Debug.Log ("动画方向: " + direction);
				switch (direction) {
				default:
						break;
				case Direction.right:
						playerSprite.transform.localScale = new Vector3 (-1,1,1);
						break;
				case Direction.left:

						playerSprite.transform.localScale = new Vector3 (1,1,1);
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

		public void setUpperAniSide(Direction direction){
				switch (direction) {
				default:
						break;
				case Direction.right:
						playerUpperSprite.transform.localScale = new Vector3 (-1,1,1);
						break;
				case Direction.left:

						playerUpperSprite.transform.localScale = new Vector3 (1,1,1);
						break;
						break;
				}
		}


		void setPlayerAnimation(){
				//待机的动画
				if (moveDir == Direction.none) {
						playerUpperSprite.renderer.enabled = false;
						upperShadowSprite.renderer.enabled = false;
						playerWait (oldMoveDir);

				}
				if (shotDir == Direction.none && moveDir != Direction.none) {
						walkShotAniPlaying = false;
						playerUpperSprite.renderer.enabled = false;
						upperShadowSprite.renderer.enabled = false;
						playerWalk (moveDir);
				}
				if (shotDir != Direction.none && moveDir != Direction.none) {
	
						playerUpperSprite.renderer.enabled = true;
						upperShadowSprite.renderer.enabled = true;
						playerLowerWalk (moveShotDir);
				}
		}

		void playerWait(Direction waitDir){
				
				if(playerAni.IsPlaying("standShot_down") || playerAni.IsPlaying("standShot_up") || playerAni.IsPlaying("standShot_left")  ){
						standShotAniPlaying = true;
				}else{
						standShotAniPlaying = false;	
				}
				if (standShotAniPlaying) {
						return;
				}
				switch (waitDir) {
				default:
						break;
				case Direction.down:
						setAniSide (Direction.left);
						playerAni.Play ("wait_down");
						break;
				case Direction.up:
						setAniSide (Direction.left);
						playerAni.Play ("wait_up");
						break;
				case Direction.left:
						setAniSide (Direction.left);
						playerAni.Play ("wait_left");
						break;
				case Direction.right:
						setAniSide (Direction.right);
						playerAni.Play ("wait_left");
						break;

						break;
				}
		}

		void playerWalk(Direction walkDir){
				switch (walkDir) {
				default:
						break;
				case Direction.down:
						setAniSide (Direction.left);
						playSameAni("walk_down");
						break;
				case Direction.up:
						setAniSide (Direction.left);
						playSameAni ("walk_up");
						break;
				case Direction.left:
						setAniSide (Direction.left);
						playSameAni("walk_left");
						break;
				case Direction.right:
						setAniSide (Direction.right);
						playSameAni("walk_left");
						break;

						break;
				}
		}
			
		public void playerLowerWalk(Direction shotDir){
				if (shotDir == moveDir) {
						switch (shotDir) {
						default:
								break;
						case Direction.down:
								setAniSide (Direction.left);
								playerAni.Play ("walkShot_down");
								break;
						case Direction.up:
								setAniSide (Direction.left);
								playerAni.Play ("walkShot_up");
								break;
						case Direction.left:
								setAniSide (Direction.left);
								playerAni.Play ("walkShot_left");
								break;
						case Direction.right:
								setAniSide (Direction.right);
								playerAni.Play ("walkShot_left");
								break;

								break;
						}
				}else{
						switch (shotDir) {
						default:
								break;
						case Direction.down:
								setAniSide (Direction.left);
								playerAni.Play ("backShot_down");
								break;
						case Direction.up:
								setAniSide (Direction.left);
								playerAni.Play ("backShot_up");
								break;
						case Direction.left:
								setAniSide (Direction.left);
								playerAni.Play ("backShot_left");
								break;
						case Direction.right:
								setAniSide (Direction.right);
								playerAni.Play ("backShot_left");
								break;

								break;

						}
				}
		}

		void playerStandShot(){

				standShotAniPlaying = true;

				switch (shotDir) {
				default:
						break;
				case Direction.down:
						setAniSide (Direction.left);
						playerAni.Play ("standShot_down");
						break;
				case Direction.up:
						setAniSide (Direction.left);
						playerAni.Play ("standShot_up");
						break;
				case Direction.left:
						setAniSide (Direction.left);
						playerAni.Play ("standShot_left");
						break;
				case Direction.right:
						setAniSide (Direction.right);
						playerAni.Play ("standShot_left");
						break;

						break;
				}

		}

		//移动射击 上半身
		public void playerWalkShot(){
				walkShotAniPlaying = true;
				if (shotDir == moveDir) {
						//前进射击
						switch (shotDir) {
						default:
								break;
						case Direction.down:
								setUpperAniSide (Direction.left);
								playerUpperAni.Play ("walkShotUpper_down");
								break;
						case Direction.up:
								setUpperAniSide (Direction.left);
								playerUpperAni.Play ("walkShotUpper_up");
								break;
						case Direction.left:
								setUpperAniSide (Direction.left);
								playerUpperAni.Play ("walkShotUpper_left");
								break;
						case Direction.right:
								setUpperAniSide (Direction.right);
								playerUpperAni.Play ("walkShotUpper_left");
								break;

								break;
						}
				} else {
						//后腿射击
						switch (shotDir) {
						default:
								break;
						case Direction.down:
								setUpperAniSide (Direction.left);
								playerUpperAni.Play ("walkShotUpper_down");
								break;
						case Direction.up:
								setUpperAniSide (Direction.left);
								playerUpperAni.Play ("walkShotUpper_up");
								break;
						case Direction.left:
								setUpperAniSide (Direction.left);
								playerUpperAni.Play ("walkShotUpper_left");
								break;
						case Direction.right:
								setUpperAniSide (Direction.right);
								playerUpperAni.Play ("walkShotUpper_left");
								break;

								break;
						}
				}
				moveShotDir = shotDir;
		}



		public void playerShot(){
				if (moveDir == Direction.none) {
						playerStandShot ();
				} else {
						playerWalkShot ();
				}
		}


		public void playSameAni(string aniName){
				int aniFrames = playerAni.CurrentFrame;
				//Debug.Log ("正在播放第" + aniFrames + "frame");
				if (walkShotAniPlaying) {
						playerAni.Play (aniName);
						playerAni.PlayFromFrame (aniFrames);
				} else {
						playerAni.Play (aniName);
				}

		}

		////////////////////人物动作////////////////////
		public void grapItem(){

		}

		public void grapWeapon(){

		}

		public void garpSkill(){

		}

		public void getHurt(){

		}

		public void die(){

		}

		////////////////////人物动作done////////////////////

		////////////////////set color//////////////////////
		public void changeBodyColor(Color color){
				playerSprite.color = color;
		}

		public void stopWUDI(){
				GameObject uiObj = transform.FindChild ("ui").gameObject;
				//uiObj.renderer.enabled = true;
				uiObj.SetActive (true);
				CancelInvoke();
		}

		//播放无敌动画
		public void playWUDI(){
				CancelInvoke();
				InvokeRepeating ("playWUDIAni", mWUDIInterval, mWUDIInterval);
		}

		private void playWUDIAni(){
				GameObject uiObj = transform.FindChild ("ui").gameObject;
				//uiObj.renderer.enabled = !uiObj.renderer.enabled;
				uiObj.SetActive (!uiObj.activeSelf);
		}
}
