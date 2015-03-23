using UnityEngine;
using System.Collections;

public class spaceSearcherlogic : MonoBehaviour {

		GameObject employ;
		bool searchDone;
		bool working;
		bool iTweenWork;
		int moveRange = 800;
		// Use this for initialization
		void Awake(){
				searchDone = false;
				working = false;
		}

		void Start () {

		}

		// Update is called once per frame
		void FixedUpdate () {
				if (working) {
						Debug.Log ("spaceSearcher开始工作");
						if (iTweenWork == false) {
								searchFreeSpace ();	
						}
				}

		}

		public void startWork(GameObject obj){
				employ = obj;
				working = true;
				if(!employ){
						Debug.Log("请检查错误");
						return;
				}
				searchFreeSpace ();
		}

		public void searchFreeSpace(){
				iTween.MoveBy(gameObject, iTween.Hash("y", Random.Range(-moveRange,moveRange)/100, "x" , Random.Range(-moveRange,moveRange)/100 , "easeType", "linear", "loopType", "none" , "time" , 0.2 , "oncomplete" , "finishMove" , "oncompletetarget" , this.gameObject ));
				iTweenWork = true;
		}

		void finishMove(){
				moveRange = 800;
				Debug.Log ("成功搜索到移动位置:" + this.transform.position);
				employ.SendMessage ("moveNearByPos", this.transform.position);
				this.transform.localPosition = employ.transform.position;
				GameObject.Destroy (this.gameObject);
		}



		void OnTriggerStay(Collider other){
				if (other.gameObject != employ) {
						Debug.Log ("moveRange: " + moveRange + " 在地下移动遇到碰撞,碰撞的物体是: " + other.name);
						iTween.Stop ();
						moveRange = moveRange - 10;
						this.transform.position = employ.transform.position;
						iTweenWork = false;


				}
		}


}
