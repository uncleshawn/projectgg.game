using UnityEngine;
using System.Collections;

public class bulletGetSpeed : MonoBehaviour {

		public Vector3 currentSpeed;
		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}
		public void setBulletSpeed(Vector3 speed){

		}

		public void shotBullet(Vector3 bulletSpeed){
				currentSpeed = bulletSpeed;
				this.rigidbody.velocity = bulletSpeed;
		}
}
