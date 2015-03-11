using UnityEngine;
using System.Collections;


public class iconlogic : MonoBehaviour {

		GameObject obj;
		tk2dSprite icon;
		Vector3 originPos;
		Vector3 originScale;
		// Use this for initialization
		void Awake(){
				originPos = transform.localPosition;
				originScale = transform.localScale;
		}
		void Start () {
				checkObjectType();
				positiveAni ();
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
				GameObject collection = (GameObject)Resources.LoadAssetAtPath ("Assets/Sprites/sheet/itemicon/itemIconCollection Data/itemIconCollection.prefab", typeof(Object));
				tk2dSpriteCollectionData collectionData = collection.GetComponent<tk2dSpriteCollectionData> ();
				int iconId = itemproperty.ID;
				if (iconId != 0) {
						ui.gameObject.GetComponent<MeshRenderer>().enabled = true;
						ui.SetSprite (collectionData, "item_" + iconId);
				} else 
				{
						ui.gameObject.GetComponent<MeshRenderer>().enabled = false;
				}

		}

		public void positiveAni(){
				iTween.ScaleBy(gameObject, iTween.Hash("amount", new Vector3(1.1f,1.1f,1), "time" ,0.5 , "loopType", "none" , "oncomplete" , "resetGameobject" , "oncompletetarget" , this.gameObject) );
		}

		public void negativeAni(){
			
				iTween.ShakePosition(gameObject , iTween.Hash("x" , 1.5 , "loopType", "none" , "time" ,0.5 , "islocal", true ,  "oncomplete" , "resetGameobject" , "oncompletetarget" , this.gameObject) );

		}


		public void resetGameobject(){
				//Debug.Log("重置道具图标");

				transform.localScale = originScale;
				transform.localPosition = originPos;
				checkObjectType ();

		}
}
