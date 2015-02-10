using UnityEngine;
using System.Collections;

public class robotShotLaserNomal : MonoBehaviour {
	
	string laserPath; 
	float laserRate; 					//laser shoot rate, rateTime > laser fullAni time
	float delayTime;	

	public float preTime;
	public float flyingTime;
	public float coldDown;
	float mcoldDown;
	public int laserDamage;
	int mbulletDamage;

	public float damageRate; 

	Direction bulletDirection;

	void Awake(){
		coldDown = 1;
		laserRate = preTime + flyingTime + mcoldDown;
		laserPath = "Prefabs/bullets/normalLaser";
	}

	// Use this for initialization
	void Start () {
		delayTime = laserRate;

	}
	// Update is called once per frame
	void FixedUpdate () {
		delayTime+=Time.deltaTime;
		if( Mathf.Abs(Input.GetAxis("shotHorizontal")) == 1 ||  Mathf.Abs(Input.GetAxis("shotVertical")) == 1  ){
			if(delayTime>laserRate)
			{
				delayTime = 0;
				getBulletDirection();
				shootLaser();
			}
		}
	}

	void shootLaser(){

		GameObject laserClone = (GameObject)Instantiate(Resources.Load(laserPath),transform.position,Quaternion.identity);
		laserClone.GetComponent<laserAniManager>().setShooter(this.transform.parent.gameObject,preTime,flyingTime);
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
	

	public void setEnabled(name_bool other){
		if(other.name == this.GetType().ToString() ){
			this.enabled = other.choose;
		}
	}
 	public void disableAll(){
		this.enabled = false;
	}

	public void upgradeProperties(char_property property){
		mcoldDown = coldDown - property.AttackRate*0.1f;
		laserRate = preTime + flyingTime + mcoldDown;
		mbulletDamage = laserDamage + property.Damage*5;
	}
}
