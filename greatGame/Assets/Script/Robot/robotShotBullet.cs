using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

public class robotShotBullet : MonoBehaviour {

	// Use this for initialization
	public GameObject bulletPrefabs;		//子弹prefab
	tk2dSprite bulletSprite;				//子弹的精灵图
	bulletGetSpeed shotScript;				//子弹用script(设定速度)
	Direction bulletDirection;				//子弹的射出方向
	float delayTime=0;						//子弹间隔时间
	public float buttleRate = 0.5f;
	float bulletSpeed;

	void Start () {
		bulletSpeed = 10;
	}
	
	// Update is called once per frame
	void Update () {
		delayTime+=Time.deltaTime;

		if( Mathf.Abs(Input.GetAxis("shotHorizontal")) == 1 ||  Mathf.Abs(Input.GetAxis("shotVertical")) == 1  ){
			if(delayTime>buttleRate)
			{
				delayTime = 0;
				shotBullet();
			}
		}

	}

	void bulletDelayTime(){

	}



	void shotBullet()
	{
		GameObject bulletClone = (GameObject)Instantiate(bulletPrefabs,this.transform.position,Quaternion.identity);
		bulletSprite = bulletClone.transform.FindChild("bulletPic").GetComponent<tk2dSprite>();
		setBulletDireciton(bulletSprite);
		shotScript = bulletClone.GetComponent<bulletGetSpeed>();
		setBulletSpeed(shotScript,bulletSpeed);
	}

	void setBulletDireciton(tk2dSprite bulletSprite)
	{
		getBulletDirection();
		switch(bulletDirection) 
		{ 
		default: 
			break; 
		case Direction.up:	 	 		bulletSprite.scale = new Vector3(bulletSprite.scale.x,Mathf.Abs(bulletSprite.scale.y),bulletSprite.scale.z);	  break;	
		case Direction.down:	 		bulletSprite.scale = new Vector3(bulletSprite.scale.x,Mathf.Abs(bulletSprite.scale.y)*-1,bulletSprite.scale.z);   break;	
		case Direction.left: 			bulletSprite.scale = new Vector3(Mathf.Abs(bulletSprite.scale.x),bulletSprite.scale.y,bulletSprite.scale.z);	  break;		
		case Direction.right: 			bulletSprite.scale = new Vector3(Mathf.Abs(bulletSprite.scale.x)*-1,bulletSprite.scale.y,bulletSprite.scale.z);	  break;	
										
			break; 
		} 

	}

	void getBulletDirection()
	{
		if(Input.GetAxis ("shotHorizontal")==1)
		{
			bulletDirection = Direction.right;
		}		
		if(Input.GetAxis ("shotHorizontal")==-1)
		{
			bulletDirection = Direction.left;
		}
		if(Input.GetAxis ("shotVertical")==-1)
		{
			bulletDirection = Direction.up;
		}
		if(Input.GetAxis ("shotVertical")==1)
		{
			bulletDirection = Direction.down;
		}
		
	}

	void setBulletSpeed(bulletGetSpeed anyScript,float Speed){
		Vector3 speed = new Vector3(0,0,0);
		getBulletDirection();
		switch(bulletDirection) 
		{ 
		default: 
			break; 
		case Direction.up:	 	 speed = new Vector3(0,Speed,0);		break;
		case Direction.down:	 speed = new Vector3(0,Speed*-1,0); 	break;			
		case Direction.left: 	 speed = new Vector3(Speed*-1,0,0); 	break;				
		case Direction.right: 	 speed = new Vector3(Speed,0,0); 		break;			
			
			break; 
		}
		anyScript.shotBullet(speed);
	}
}
