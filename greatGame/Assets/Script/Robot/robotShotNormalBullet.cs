using UnityEngine;
using System.Collections;

public class robotShotNormalBullet : MonoBehaviour {

	// Use this for initialization
	string  bulletPath = "Prefabs/bullets/normalBullet";	//子弹prefab
	bulletGetSpeed shotScript;				//子弹用script(设定速度)
	Direction bulletDirection;				//子弹的射出方向
			
	public float bulletRate = 0.5f;			//子弹origin间隔时间
	float mbulletRate;
	public float bulletSpeed;				//子弹的origin速度
	float mbulletSpeed;
	public float bulletDistance;			//子弹的origin距离
	float mbulletDistance;
	public int bulletDamage;				//bullet damage
	int mbulletDamage;

	public int mknockBack;
	public float mdamageRate;
	public ElementType mType;

	float delayTime=0;	

	GameObject player;						//获得玩家gameobject
	Vector3 playerVelocity;					//玩家的移动速度
	public float velocityTransfer;			//玩家给子弹的动量转化率

	public Vector3 shotPosition;			//射出子弹位置(不是同一个位置)
	public float posV = 1;					//位置偏差值

	//射击的位置
	GameObject posUp;
	GameObject posDown;
	GameObject posLeft;
	GameObject posRight;
	
	void Start () {

		player = this.transform.parent.gameObject;
		shotPosition = new Vector3(0,0,0);
		mbulletRate = bulletRate;
		mbulletSpeed = bulletSpeed;
		mbulletDistance = bulletDistance;
		mbulletDamage = bulletDamage;

		posUp = transform.FindChild("up").gameObject;
		posDown = transform.FindChild("down").gameObject;
		posLeft = transform.FindChild("left").gameObject;
		posRight = transform.FindChild("right").gameObject;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		upgradeBulletProperties();

		//时间间隔
		delayTime+=Time.deltaTime;
		if( Mathf.Abs(Input.GetAxis("shotHorizontal")) == 1 ||  Mathf.Abs(Input.GetAxis("shotVertical")) == 1  ){
			if(delayTime>mbulletRate)
			{
				delayTime = 0;
				shootBullet();
			}
		}
		
	}

	public void upgradeBulletProperties(){

	}

	void shootBullet()
	{
		getBulletDirection();			//按键设定子弹的飞行方向
		setShotPosition();				//设置子弹的发射位置
		GameObject bulletClone = (GameObject)Instantiate(Resources.Load(bulletPath),shotPosition,Quaternion.identity);
		//set bullet end distance
		bulletClone.GetComponent<bulletCheckDistance>().setDistance(mbulletDistance);
		//子弹的damage + 速度 +方向
		setBulletProperty(bulletClone);
		shotScript = bulletClone.GetComponent<bulletGetSpeed>();
		setBulletSpeed(shotScript,mbulletSpeed);



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
		if(Input.GetAxis ("shotVertical")==1)
		{
			bulletDirection = Direction.up;
		}
		if(Input.GetAxis ("shotVertical")==-1)
		{
			bulletDirection = Direction.down;
		}
		
	}
	
	void setBulletSpeed(bulletGetSpeed anyScript,float Speed){
		Vector3 speed = new Vector3(0,0,0);
		playerVelocity = player.rigidbody.velocity;
		//Debug.Log("人物移动速度: " +playerVelocity );
		getBulletDirection();
		switch(bulletDirection) 
		{ 
		default: 
			break; 
		case Direction.up:	 	speed = new Vector3(0,Speed,0);		speed += new Vector3(playerVelocity.x,0,0)*velocityTransfer;
			break;
		case Direction.down:	speed = new Vector3(0,Speed*-1,0);	speed += new Vector3(playerVelocity.x,0,0)*velocityTransfer;	
			break;			
		case Direction.left: 	speed = new Vector3(Speed*-1,0,0); 	speed += new Vector3(0,playerVelocity.y,0)*velocityTransfer;
			break;				
		case Direction.right: 	speed = new Vector3(Speed,0,0);	 	speed += new Vector3(0,playerVelocity.y,0)*velocityTransfer;
			break;			
			
			break; 
		}
		anyScript.shotBullet(speed);
	}



	void setShotPosition(){
		switch(bulletDirection) 
		{ 
		default: 
			break; 
		case Direction.up:	 	shotPosition = posUp.transform.position;	
			break;
		case Direction.down:	shotPosition = posDown.transform.position;	
			break;			
		case Direction.left: 	shotPosition = posLeft.transform.position;	 	
			break;				
		case Direction.right: 	shotPosition = posRight.transform.position;	
			break;			
			
			break; 
		}
	}

	//only being used by other scripts
	public void setEnabled(name_bool other){
		if(other.name == this.GetType().ToString()  ){
			this.enabled = other.choose;
		}
	}
	//only being used by other scripts
	public void disableAll(){
		this.enabled = false;
	}

	//only being used by other scripts
	public void upgradeProperties(char_property property){
		mbulletSpeed = bulletSpeed + property.AttackSpeed;
		mbulletRate = bulletRate - (bulletRate-0.1f)*property.AttackRate/10;
		mbulletDistance = bulletDistance + property.AttackDistance;
		mbulletDamage = bulletDamage + property.Damage*5;

	}

	//using when shoot a bullet
	public void setBulletProperty(GameObject bulletClone){
		if(mdamageRate == 0) {mdamageRate = 10;}
		bulletClone.GetComponent<bullet_property>().setProperty(mbulletDamage,mknockBack,mdamageRate,mType);
	}
}


