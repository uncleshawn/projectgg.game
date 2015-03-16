using UnityEngine;
using System.Collections;

public class itemlogic : monsterbaselogic {

		// Use this for initialization
		void Start () {
				debugShowName ();
		}

		// Update is called once per frame
		void Update () {
		}


		//debug mode use
		public void debugShowName() {

				if(GameObject.FindGameObjectWithTag("GameLogic").GetComponent<uilogic>().debugText == true){
						string itemName = this.gameObject.GetComponent<item_property>().itemName;
						if (itemName == "") {
								//获取道具名称

						}
						GameObject debugText = transform.FindChild("debugMode_itemName").gameObject;
						if(debugText){
								debugText.GetComponent<tk2dTextMesh>().text = itemName;
						}
				}
		}

		//播放购买成功动画
		public void canBuyAni(){
				GameObject itemIcon = transform.FindChild ("UI_itemIcon").gameObject;
				if (itemIcon) {
						itemIcon.GetComponent<iconlogic> ().positiveAni ();
				}
		}

		//播放购买失败动画
		public void canNotBuyAni(){
				GameObject itemIcon = transform.FindChild ("UI_itemIcon").gameObject;
				if (itemIcon) {
						itemIcon.GetComponent<iconlogic> ().negativeAni ();
				}
			
		}

		//刷新item
		public void resetGameobject(){
				//Debug.Log("重置道具名称");
				debugShowName();
				transform.FindChild("UI_itemIcon").gameObject.SendMessage ("resetGameobject");
		}


		void OnTriggerEnter(Collider other){
				constant.getMapLogic ().colliderEnter (this.gameObject, other.gameObject);
		}
}
