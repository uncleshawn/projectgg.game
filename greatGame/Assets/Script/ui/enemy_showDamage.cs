﻿using UnityEngine;
using System.Collections;

public class enemy_showDamage : MonoBehaviour {

	public string damageNum;
	public tk2dTextMesh textMesh;
	public Color damageButtom;
	public Color damageTop;

	public string Num { get { return damageNum; } set { damageNum = value; }}
	public Color buttonColor { get { return damageButtom; } set { damageButtom = value; }}
	public Color topColor  { get { return damageTop; } set { damageTop = value; }}

	Vector3 startPos;
	Vector3 endPos;
	float moveSpeed;

	GameObject followObject;

	// Use this for initialization
	void Awake(){
		damageNum = "empty";
		damageButtom = new Color(255,0,0);
		damageTop = new Color(255,112,112);
		textMesh = gameObject.GetComponent<tk2dTextMesh>();
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setDamageText(){

		textMesh.text = damageNum;
		textMesh.color2 = damageButtom;
		textMesh.color = damageTop;


	}

	public void moveFont(){
		iTween.MoveBy(gameObject, iTween.Hash("y", 2,  "easeType", "easeInOutQuad", "loopType", "none" , "time" , 1 , "oncomplete" , "finishMove" , "oncompletetarget" , this.gameObject ));
		//iTween.FadeTo(gameObject, iTween.Hash( "easeType", "linear", "loopType", "once" , "time" , 1 , "delay" , 1 , "amount", 0 ) );
	}

	public void showDamage(){
		setDamageText();
		moveFont ();
	}

	public void finishMove(){
		//Debug.Log("finish Move");
		Destroy(this.gameObject);
	}
}