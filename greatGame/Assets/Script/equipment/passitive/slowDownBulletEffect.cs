using UnityEngine;
using System.Collections;

public class slowDownBulletEffect : MonoBehaviour {

		public bool startWork;

		public bool slowEffect;
		public int slowPercent;
		public float slowLevel;
		SlowBullet slowBullet;
		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		void intPassitiveSkill(speItem_property item){
				if (startWork == false) {
						slowPercent = item.parameterInt;
						slowEffect = true;
						slowLevel = item.parameterFloat;
						//设置struct ScaredBullet
						slowBullet.slowEffect = slowEffect;
						slowBullet.slowPercent = slowPercent;
						slowBullet.slowLevel = slowLevel;
						startWork = true;
						GameObject shooter = this.transform.parent.transform.FindChild ("shoot").gameObject;
						if (shooter) {
								shooter.SendMessage ("setSlowBullet", slowBullet);
						} else {
								Debug.Log("找不到shoot object");
						}
				}


		}

		void disableBulletEffect(){
				slowPercent = 0;
				slowEffect = false;
				slowLevel = 1;
				slowBullet.slowEffect = slowEffect;
				slowBullet.slowPercent = slowPercent;
				slowBullet.slowLevel = slowLevel;
				startWork = false;
				GameObject shooter = this.transform.parent.transform.FindChild ("shoot").gameObject;
				if (shooter) {
						shooter.SendMessage ("setSlowBullet", slowBullet);
				} else {
						Debug.Log("找不到shoot object");
				}
		}
}
