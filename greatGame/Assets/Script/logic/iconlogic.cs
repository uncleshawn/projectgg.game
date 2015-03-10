using UnityEngine;
using System.Collections;


public class iconlogic : MonoBehaviour {

		GameObject obj;
		tk2dSprite icon;
		// Use this for initialization
		void Start () {
				checkObjectType();
		}

		// Update is called once per frame
		void Update () {

		}

		public void  checkObjectType(){
				icon = gameObject.GetComponent<tk2dSprite>();
				obj = transform.parent.gameObject;


				if(obj.GetComponent<item_property>()){
						setItemIcon(icon , obj);
				}
		}


		public void setItemIcon(tk2dSprite ui , GameObject obj){
				item_property itemproperty = obj.GetComponent<item_property>();
				int iconId = itemproperty.ID;
				if(iconId!=0){
						GameObject collection = (GameObject)Resources.LoadAssetAtPath("Assets/Sprites/sheet/itemicon/itemIconCollection Data/itemIconCollection.prefab" , typeof(Object));
						tk2dSpriteCollectionData collectionData = collection.GetComponent<tk2dSpriteCollectionData>();
						ui.SetSprite (collectionData, "item_" + iconId);
				}

		}

		public void resetGameobject(){
				//Debug.Log("重置道具图标");
				checkObjectType ();

		}
}
