using UnityEngine;
using System.Collections;

public class scaredBulletEffect : MonoBehaviour {

		public bool startWork;

		public bool scaredEffect;
		public int scaredPercent;
		ScaredBullet scaredBullet;
		// Use this for initialization
		void Awake(){
		}
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		void intPassitiveSkill(speItem_property item){
				if (startWork == false) {
						scaredPercent = item.parameterInt;
						scaredEffect = true;

						//设置struct ScaredBullet
						scaredBullet.sacredPercent = scaredPercent;
						scaredBullet.scaredEffect = scaredEffect;
						startWork = true;
						GameObject shooter = this.transform.parent.transform.FindChild ("shoot").gameObject;
						if (shooter) {
								shooter.SendMessage ("setScaredBullet", scaredBullet);
						} else {
								Debug.Log("找不到shoot object");
						}
				}


		}

		void disableScaredEffect(){
				scaredPercent = 0;
				scaredEffect = false;
				scaredBullet.sacredPercent = scaredPercent;
				scaredBullet.scaredEffect = scaredEffect;
				startWork = false;
				GameObject shooter = this.transform.parent.transform.FindChild ("shoot").gameObject;
				if (shooter) {
						shooter.SendMessage ("setScaredBullet", scaredBullet);
				} else {
						Debug.Log("找不到shoot object");
				}
		}
}
