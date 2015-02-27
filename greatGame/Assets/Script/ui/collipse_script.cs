using UnityEngine;
using System.Collections;

public class collipse_script : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		//Debug.Log("OnCollisionEnter : " + collision.gameObject.tag); 

		if (collision.gameObject.tag.Equals ("Player")) {
			charlogic charLogic = collision.gameObject.GetComponent<charlogic>();
			//charLogic.beAttack(this.gameObject);
		}
	}

	void OnTriggerEnter( Collider other ){
		//other.GetComponent
		//Debug.Log ("OnTriggerEnter "+other.gameObject.tag);
		Debug.Log ("OnTriggerEnter " + other.gameObject.tag);		
		if (other.gameObject.tag.Equals ("Bullet")) {
			monsterbaselogic monsterLogic = this.gameObject.GetComponent<monsterbaselogic>();
			monsterLogic.beAttack(other.gameObject);
		}
		//Component charProperty = obj.GetComponent<"char_property">();
	}

}
