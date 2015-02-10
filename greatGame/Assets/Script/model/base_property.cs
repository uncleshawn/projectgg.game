﻿using UnityEngine;
using System.Collections;

public class base_property : MonoBehaviour {

	protected float mFAcc = 20.0f; //摩擦力
	protected float mMass = 1.0f;
	public float FAcc { get { return mFAcc; } set { mFAcc = value; }}
	public float Mass { get { return mMass; } set { mMass = value; }}

	// Use this for initialization
	void Start () {
		mMass = this.rigidbody.mass;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}