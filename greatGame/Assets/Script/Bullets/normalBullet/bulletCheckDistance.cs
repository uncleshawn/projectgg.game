using UnityEngine;
using System.Collections;

public class bulletCheckDistance : MonoBehaviour {

		// Use this for initialization
		Vector3 originPos;
		Vector3 bulletPos;
		float bulletDistance;
		//public GameObject bullet;
		void Start () {
				setOriginPos(transform.position);
		}

		// Update is called once per frame
		void Update () {
				setBulletPos(transform.position);
				getBulletDistance();
		}

		public void setDistance(float distance){
				bulletDistance = distance;
		}

		public void setOriginPos(Vector3 pos)
		{
				originPos = pos;
		}

		public void setBulletPos(Vector3 pos)
		{
				bulletPos = pos;
		}

		public void getBulletDistance()
		{
				float distance = Vector3.Distance(originPos,bulletPos);
				if(bulletDistance < distance)
				{
						//调用aniManager的距离函数处理结果
						Debug.Log("子弹达到最远距离: " + bulletDistance);
						this.gameObject.GetComponent<bulletAniManager>().distanceEnd();
						this.enabled = false;
				}
		}


}
