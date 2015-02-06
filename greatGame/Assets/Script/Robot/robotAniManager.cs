using UnityEngine;
using System.Collections;

public class robotAniManager : MonoBehaviour {

	// Use this for initialization
	public GameObject head;
	public GameObject body;
	public GameObject feet;

	tk2dSpriteAnimator headAni;
	tk2dSprite headSprite;
	tk2dSpriteAnimator bodyAni;
	tk2dSprite bodySprite;
	tk2dSpriteAnimator feetAni;
	tk2dSprite feetSprite;

	Direction robotDirection;
	Direction feetDirection;
	

	
	void Start () {

		head = transform.FindChild("ui").FindChild("robotHead").gameObject;
		body = transform.FindChild("ui").FindChild("robotBody").gameObject;
		feet = transform.FindChild("ui").FindChild("robotFeet").gameObject;

		if(head!=null){
			headAni = head.GetComponent<tk2dSpriteAnimator>();
			headSprite =  head.GetComponent<tk2dSprite>();
		}
		if(body!=null){

			bodyAni = body.GetComponent<tk2dSpriteAnimator>();
			bodySprite =  body.GetComponent<tk2dSprite>();
		}
		if(feet!=null){
			feetAni = feet.GetComponent<tk2dSpriteAnimator>();
			feetSprite =  feet.GetComponent<tk2dSprite>();
		}

		robotDirection = Direction.down;


	}
	
	// Update is called once per frame
	void Update () {
		robotBodyMove();
		robotFeetMove();
		robotHeadMove();
		robotMoveNormal();

	}

	public void robotMoveNormal(){

		if(bodyAni.Playing == false){
			robotBodyMove();
		}
		if(feetAni.Playing  == false){
			robotFeetMove();
		}
		if(headAni.Playing  == false){
			robotHeadMove();
		}
		
	}


	public void robotBodyMove()
	{
		if( (Input.GetAxis("Horizontal")!=0) || (Input.GetAxis("Vertical")!=0)){
			bodyRun();

		}
		else{
			bodyWait();
		}
	}
	public void robotFeetMove()
	{
		if( (Input.GetAxis("Horizontal")!=0) || (Input.GetAxis("Vertical")!=0)){
			feetRun();
			
		}
		else{
			feetWait();
		}
	}

	//move head
	public void robotHeadMove()
	{
		robotTurnHead();
		if( (Input.GetAxis("Horizontal")!=0) || (Input.GetAxis("Vertical")!=0)){
			headRun(robotDirection);
			
		}
		else{
			headWait(robotDirection);
		}
	}
	//身体方向
	public void bodyRun(){
		//robotTurnHead();
		switch(feetDirection) 
		{ 
		default: 
			break; 
		case Direction.up:	 			bodyAni.Play("run_body_up"); 		break;
		case Direction.down:	 		bodyAni.Play("run_body_down");		break;
		case Direction.left:		 	bodySprite.scale = new Vector3(Mathf.Abs(bodySprite.scale.x)*-1,bodySprite.scale.y,bodySprite.scale.z);			
										bodyAni.Play("run_body_side");		break;
		case Direction.right: 			bodySprite.scale = new Vector3(Mathf.Abs(bodySprite.scale.x),bodySprite.scale.y,bodySprite.scale.z);			
										bodyAni.Play("run_body_side"); 		break;
			break; 
		} 
	}


	public void bodyWait(){
		bodyAni.Play("wait_body_down");
	}

	//腿部方向
	public void feetRun(){
		robotTurnFeet();
		switch(feetDirection) 
		{ 
		default: 
			break; 
		case Direction.up:	 			feetAni.Play("run_feet_up"); 		break;
		case Direction.down:	 		feetAni.Play("run_feet_down");		break;
		case Direction.left:		 	feetSprite.scale = new Vector3(Mathf.Abs(feetSprite.scale.x)*-1,feetSprite.scale.y,feetSprite.scale.z);			
										feetAni.Play("run_feet_side");		break;
		case Direction.right: 			feetSprite.scale = new Vector3(Mathf.Abs(feetSprite.scale.x),feetSprite.scale.y,feetSprite.scale.z);			
										feetAni.Play("run_feet_side"); 		break;
			break; 
		} 
	}

	public void feetWait(){
		feetAni.Play("wait_feet_down");

	}




	//turn feet
	public void robotTurnFeet()
	{
		if(Input.GetAxis ("Horizontal")==1)
		{
			feetDirection = Direction.right;
		}		
		if(Input.GetAxis ("Horizontal")==-1)
		{
			feetDirection = Direction.left;
		}
		if(Input.GetAxis ("Vertical")==1)
		{
			feetDirection = Direction.up;
		}
		if(Input.GetAxis ("Vertical")==-1)
		{
			feetDirection = Direction.down;
		}
		
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
		bodySprite.color = color;
	}


	/////////////OnCollisionEnter//////////
	void OnCollisionEnter(Collision other) {
		//Debug.Log("OnCollisionEnter : " + collision.gameObject.tag); 
		
		if (other.gameObject.tag.Equals ("Item")) {
			Debug.Log("player touch item");
			charlogic charLogic = transform.gameObject.GetComponent<charlogic>();
			if(charLogic.grapItem(other.gameObject)){
				Destroy(other.gameObject);
			}
		}
	}
}
