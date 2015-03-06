using UnityEngine;
using System.Collections;

public class followerAniManager : MonoBehaviour {

	// Use this for initialization
	GameObject head;
	
	tk2dSpriteAnimator headAni;
	tk2dSprite headSprite;

	Direction robotDirection;

	void Start () {
		
		head = transform.FindChild("ui").FindChild("head").gameObject;

		
		if(head!=null){
			headAni = head.GetComponent<tk2dSpriteAnimator>();
			headSprite =  head.GetComponent<tk2dSprite>();
		}
		robotDirection = Direction.down;
		
		
	}
	
	// Update is called once per frame
	void Update () {

		robotHeadMove();
		robotMoveNormal();
		
	}
	
	public void robotMoveNormal(){

		if(headAni.Playing  == false){
			robotHeadMove();
		}
		
	}
	

	//move head
	public void robotHeadMove()
	{
		robotTurnHead();
		headWait(robotDirection);
//		if( (Input.GetAxis("Horizontal")!=0) || (Input.GetAxis("Vertical")!=0)){
//			headRun(robotDirection);
//			
//		}
//		else{
//			headWait(robotDirection);
//		}
	}


	
	public void robotTurnHead()
	{
		if(Input.GetAxis ("shotHorizontal")==1)
		{
			robotDirection = Direction.right;
		}		
		if(Input.GetAxis ("shotHorizontal")==-1)
		{
			robotDirection = Direction.left;
		}
		if(Input.GetAxis ("shotVertical")==1)
		{
			robotDirection = Direction.up;
		}
		if(Input.GetAxis ("shotVertical")==-1)
		{
			robotDirection = Direction.down;
		}
		
	}
	
	void headWait(Direction robotDirection)
	{
		switch(robotDirection) 
		{ 
		default: 
			break; 
		case Direction.up:	 			headAni.Play("wait_head_up"); 		break;
		case Direction.down:	 		headAni.Play("wait_head_down");		break;
		case Direction.left: 			headSprite.scale = new Vector3(Mathf.Abs(headSprite.scale.x)*-1,headSprite.scale.y,headSprite.scale.z);			
			headAni.Play("wait_head_side");		break;
		case Direction.right: 			headSprite.scale = new Vector3(Mathf.Abs(headSprite.scale.x),headSprite.scale.y,headSprite.scale.z);	
			headAni.Play("wait_head_side"); 	break;
			break; 
		} 
	}
	
	void headRun(Direction robotDirection)
	{
		switch(robotDirection) 
		{ 
		default: 
			break; 
		case Direction.up:	 			headAni.Play("run_head_up"); 		break;
		case Direction.down:	 		headAni.Play("run_head_down");		break;
		case Direction.left:		 	headSprite.scale = new Vector3(Mathf.Abs(headSprite.scale.x)*-1,headSprite.scale.y,headSprite.scale.z);			
			headAni.Play("run_head_side");		break;
		case Direction.right: 			headSprite.scale = new Vector3(Mathf.Abs(headSprite.scale.x),headSprite.scale.y,headSprite.scale.z);			
			headAni.Play("run_head_side"); 		break;
			break; 
		} 
	}

	public void shootBullet(){

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
	public void changeColor(Color color){
		headSprite.color = color;
	}
}
