//	激光武器的动画和gameobject管理
//	在蓄力期间可以随意改变射击位置
//	射出后激光跟随玩家移动但不能改变方向
//	按下发射激光后不能取消，每一发需要等待冷却时间，冷却时间>每一发的激光全部动画时间

using UnityEngine;
using System.Collections;
public class laserAniManager : MonoBehaviour {

	// Use this for initialization


	GameObject player;								//玩家
	Vector3 shootPos;
	Direction bulletDirection = Direction.down;		//射击方向
	GameObject shoot;								//射出的位置

	public tk2dSprite bulletSprite;					//子弹的精灵图	
	public tk2dSpriteAnimator bulletAni;			//子弹动画

	float totalClipLength;							//动画总时间
	float preTime = 0.25f;							//激光蓄力时间
	float flyingTime = 1.5f;						//激光持续时间

	public Direction BulletDirection { get { return bulletDirection; } set { bulletDirection = value; }}

	void Start () {
			//use setShooterFirst() !!!
	}

	// Update is called once per frame
	void Update () {
		if(player!=null){
			if(bulletAni.IsPlaying("end") == false){
				//激光准备期间可以改方向
				if( (Mathf.Abs(Input.GetAxis ("shotHorizontal")) == 1) || (Mathf.Abs(Input.GetAxis ("shotVertical")) ==1) ){
					if(bulletAni.IsPlaying("pre")){
						bulletDirection = getDirection();
						setShootPos(bulletDirection);
						setAniDireciton(bulletDirection);
						}
				}
			this.transform.position = shoot.transform.position;
			}
		}
		else{
			Destroy(this.gameObject);
		}
	}

	public void setShooter(GameObject shooter, float preTime, float flyingTime){
		player = shooter;
		shoot = player.transform.FindChild("shoot").gameObject;
		this.preTime = preTime;
		this.flyingTime = flyingTime;
		bulletSprite = transform.FindChild("ui").FindChild("bulletPic").GetComponent<tk2dSprite>();
		bulletAni = transform.FindChild("ui").FindChild("bulletPic").GetComponent<tk2dSpriteAnimator>();
		setAniDireciton(bulletDirection);
		laserPre();
	}


	Direction getDirection(){
		if(Input.GetAxis ("shotHorizontal")==1)
		{
			return Direction.right;
		}		
		if(Input.GetAxis ("shotHorizontal")==-1)
		{
			return Direction.left;
		}
		if(Input.GetAxis ("shotVertical")==1)
		{
			return Direction.up;
		}
		if(Input.GetAxis ("shotVertical")==-1)
		{
			return Direction.down;
		}
		return bulletDirection;
	}

	void setShootPos(Direction pos){
		switch(pos) 
		{ 
		default: 
			break;
		case Direction.up:		shoot = player.transform.FindChild("shoot").FindChild("up").gameObject; 		break;
		case Direction.down:	shoot = player.transform.FindChild("shoot").FindChild("down").gameObject;		break;
		case Direction.left:	shoot = player.transform.FindChild("shoot").FindChild("left").gameObject;		break;
		case Direction.right:	shoot = player.transform.FindChild("shoot").FindChild("right").gameObject;		break;
			break;
		}
	}

	void setAniDireciton(Direction bulletDirection)
	{
		switch(bulletDirection) 
		{ 
		default: 
			break; 
		case Direction.up:	 	transform.rotation = Quaternion.identity; transform.Rotate(0,0,90);  	break;	
		case Direction.down:	transform.rotation = Quaternion.identity; transform.Rotate(0,0,270);  	break;	
		case Direction.left: 	transform.rotation = Quaternion.identity; transform.Rotate(0,0,180); 	break;		
		case Direction.right: 	transform.rotation = Quaternion.identity; 							  	break;	
			break; 
		} 
		
	}
	
//	void setSpriteDirection(Direction direction){
//
//		if(direction == Direction.left || direction == Direction.right){
//			//Debug.Log ("横向子弹");
//			transform.rotation = Quaternion.identity;
//		}
//		else{
//			transform.rotation = Quaternion.identity;
//			transform.Rotate(0,0,90);
//		}
//	}

	void laserPre(){
		bulletlogic bulletEnable = gameObject.GetComponent<bulletlogic>();
		bulletEnable.doDamage = false;
		StartCoroutine(shotAfterPre());
	}

	void laserFlying(){
		bulletlogic bulletEnable = gameObject.GetComponent<bulletlogic>();
		bulletEnable.doDamage = true;
		StartCoroutine(endAfterShot());
	}

	void laserEnd(){
		bulletlogic bulletEnable = gameObject.GetComponent<bulletlogic>();
		bulletEnable.doDamage = false;
		destroyAfterAni("end");
	}



	IEnumerator shotAfterPre(){
		bulletAni.Play("pre");
		yield return new WaitForSeconds(preTime);
		laserFlying();
	}
	IEnumerator endAfterShot(){
		bulletAni.Play("flying");
		yield return new WaitForSeconds(flyingTime);								//等待flyingTime秒后，结束激光动画
		laserEnd();
	}

	public void afterAni(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip){
		Destroy(this.gameObject);
	}
	
	public void destroyAfterAni(string aniName){
		bulletAni.Play(aniName);
		bulletAni.AnimationCompleted = afterAni;

	}




}
