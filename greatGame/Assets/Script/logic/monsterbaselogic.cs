using UnityEngine;
using System.Collections;

public class monsterbaselogic : MonoBehaviour {

		protected float deltaTime_scared;

		//private float mFAcc = 20.0f;
		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		virtual public Vector3 getMoveAcc(){
				Vector3 v = new Vector3 ();
				v.x = 0;
				v.y = 0;
				v.z = 0;
				return v;
		}

		virtual public Vector3 scaredMovePos(){
				Vector3 v = new Vector3 ();
				v.x = 0;
				v.y = 0;
				v.z = 0;
				return v;
		}

		virtual public void scaredMove(){

		}

		virtual public Vector3 getFAcc(){
				Vector3 v = new Vector3 ();

				maplogic logic = constant.getMapLogic ();
				v.x = logic.getRoomInfoFAcc() * this.rigidbody.mass;
				v.y = logic.getRoomInfoFAcc() * this.rigidbody.mass;
				v.z = 0;
				return v;
		}

		virtual public void beAttack(GameObject obj){

		}
		virtual public void beAttackByBullet(GameObject obj){

		}

		virtual public void getKnockBack(GameObject obj , bullet_property bullet){
				Debug.Log ("敌人击退override函数没写,请注意");
		}

		virtual public void normalKnockBack(bullet_property bullet, int force){
				Debug.Log ("敌人击退override函数没写,请注意");
		}

		virtual public void explodeKnockBack(bullet_property bullet, int force){
				Debug.Log ("敌人击退override函数没写,请注意");
		}
		virtual public void checkBulletEffect(GameObject enemy , GameObject bullet){
				Debug.Log ("子弹特殊效果override函数没写,请注意");
		}
		virtual public void checkScaredRecover(float deltaTime){
				Debug.Log ("恐惧恢复override函数没写,请注意");
		}

		virtual public void getSlow(float slowDown){
				Debug.Log ("减速效果override函数没写,请注意");
		}

		virtual public void destroy(){
				Destroy (this.gameObject);
		}

}
