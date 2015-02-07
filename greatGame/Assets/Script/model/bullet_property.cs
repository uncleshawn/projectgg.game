using UnityEngine;
using System.Collections;

public class bullet_property : MonoBehaviour {

	public int bulletDamage;
	public int bulletknock;
	public float bulletDamageRate;
	public ElementType bulletElement;


	// Use this for initialization
	void Awake(){

	}
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void setProperty(int damage, int knock, float damamgeRate, ElementType element){
		bulletDamage = damage;
		bulletknock = knock ;
		bulletDamageRate = damamgeRate ; 
		bulletElement = element;
	}
}
