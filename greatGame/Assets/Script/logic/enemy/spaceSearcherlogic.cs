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
						//Debug.Log (employ.name + "的spaceSearcher正在工作");
						if (iTweenWork == false) {
								searchFreeSpace ();	
						}
				}

		}

		public void startWork(GameObject obj){
				//Debug.Log (obj.name + "开始寻路");
				employ = obj;
				this.gameObject.name = employ.name + "pathSearch";
				working = true;
				if(!employ){
						Debug.Log("请检查错误");
						return;
				}
		}

		public void searchFreeSpace(){
				
				iTween.MoveBy(gameObject, iTween.Hash("name" , this.gameObject.name , "y", Random.Range(-moveRange,moveRange)/150, "x" , Random.Range(-moveRange,moveRange)/150 , "easeType", "linear", "loopType", "none" , "time" , 1 , "oncomplete" , "finishMove" , "oncompletetarget" , this.gameObject ));
				iTweenWork = true;
		}

		void finishMove(){
				employ.SendMessage ("moveNearByPos", this.transform.position);
				searchDone = true;
				GameObject.Destroy (this.gameObject);
		}



		void OnTriggerEnter(Collider other){
				if (!searchDone) {
						if (employ) {
								//Debug.Log (employ.name + "的寻路器处理碰撞中: 面对" + other.name);
								if (other.gameObject.name != employ.name && other.tag != "Bullet" && other.name != "spaceSearch(Clone)") {
										//Debug.Log (employ.name + "寻路失败,发生碰撞: " + other.name);
										//Debug.Log ("碰撞,寻路取消");
										//iTween.StopByName (this.gameObject.name);
										iTweenWork = false;

										//缩小寻路范围
										moveRange = moveRange - 30;
										if (moveRange < 0) {
												moveRange = 0;
										}

										this.transform.position = employ.transform.position;

								}
						} else {
								Debug.Log ("怪物被击败,寻路取消");
								iTween.StopByName (this.gameObject.name);
								GameObject.Destroy (this.gameObject);	
						}
				}
		}


}
