using UnityEngine;
using System.Collections;

public class enemy_showDamage : MonoBehaviour {

		private string damageNum;
		tk2dTextMesh textMesh;
		private Color damageButtom;
		private Color damageTop;

		public string mNum { get { return damageNum; } set { damageNum = value; }}
		public Color mButtonColor { get { return damageButtom; } set { damageButtom = value; }}
		public Color mTopColor  { get { return damageTop; } set { damageTop = value; }}

		Vector3 startPos;
		Vector3 endPos;
		float moveSpeed;

		//GameObject followObject;
		float time;

		// Use this for initialization
		void Awake(){
				damageNum = "empty";
				damageButtom = new Color(255,0,0);
				damageTop = new Color(255,112,112);
				textMesh = gameObject.GetComponent<tk2dTextMesh>();
				time = 0;
		}

		void Start () {

		}

		// Update is called once per frame
		void FixedUpdate () {
				time = time + Time.deltaTime;
				if (time >= 1) {
						Debug.Log ("伤害数字超过时间, 请解决问题");
						GameObject.Destroy (this.gameObject);
				}
		}

		public void setDamageText(){

				textMesh.text = damageNum;
				textMesh.color2 = damageButtom;
				textMesh.color = damageTop;


		}

		public void moveFont(){
				iTween.MoveBy(gameObject, iTween.Hash("y", 1.5f,  "easeType", "easeInOutQuad", "loopType", "none" , "time" , 0.6 , "oncomplete" , "finishMove" , "oncompletetarget" , this.gameObject ));
				//iTween.FadeTo(gameObject, iTween.Hash( "easeType", "linear", "loopType", "once" , "time" , 1 , "delay" , 1 , "amount", 0 ) );
		}

		public void showDamage(string damage){
				setDamageText();
				textMesh.text = damage;
				moveFont ();
		}

		public void finishMove(){
				//Debug.Log("finish Move");
				GameObject.Destroy(this.gameObject);
		}
}
