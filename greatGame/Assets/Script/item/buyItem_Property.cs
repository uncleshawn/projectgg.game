using UnityEngine;
using System.Collections;

public class buyItem_Property : MonoBehaviour {

		public string itemName;
		public int mID;
		public int itemPrice;
		public int stock;
		bool soldout;


		void Awake(){
				soldout = false;
		}
		// Use this for initialization
		void Start () {
				checkStock ();
				presentItem ();
		}

		// Update is called once per frame
		void Update () {

		}

		void presentItem(){
				if (soldout == false) {
						//显示道具
						GameObject itemPic = transform.FindChild ("ui").gameObject.transform.FindChild ("itempic").gameObject;
						item_property itemProperty = itemPic.GetComponent<item_property> ();
						if (itemProperty) {
								itemProperty.ID = mID;
								itemProperty.itemName = itemName;
								itemPic.SendMessage ("resetGameobject");
						}
				} else {
						//mId=0 时,隐藏道具
						itemName = "已售完";
						mID = 0;
						GameObject itemPic = transform.FindChild ("ui").gameObject.transform.FindChild ("itempic").gameObject;
						item_property itemProperty = itemPic.GetComponent<item_property> ();
						if (itemProperty) {
								itemProperty.ID = mID;
								itemProperty.itemName = itemName;
								itemPic.SendMessage ("resetGameobject");

						}
				}
		}

		void checkStock(){
				if (stock <= 0) {
						soldout = true;
				}
		}

		void OnCollisionEnter(Collision other){

		}

}


