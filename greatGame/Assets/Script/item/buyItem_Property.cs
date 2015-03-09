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
				presentItem ();
		}

		// Update is called once per frame
		void Update () {

		}

		void presentItem(){
				if(soldout==false){
						item_property picItem = transform.FindChild ("ui").gameObject.transform.FindChild ("picItem").gameObject.GetComponent<item_property> ();
						if (picItem) {
								picItem.ID = mID;
								picItem.gameObject.SendMessage("resetGameobject");
						}
				}
		}

		void checkStock(){
				if (stock <= 0) {
						soldout = true;
				}
		}
}
