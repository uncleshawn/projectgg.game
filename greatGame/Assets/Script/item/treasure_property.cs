using UnityEngine;
using System.Collections;

public class treasure_property : MonoBehaviour {
		private int mID;
		public int mTreasureNum;
		public int ID { get { return mID; } set { mID = value; }}
		public int Num { get { return mTreasureNum;}  set { mTreasureNum = value; }}
		// Use this for initialization
		void Awake(){
				item_property itemProperty = gameObject.GetComponent<item_property>();
				mID = itemProperty.ID;
		}
		void Start () {
				if (mTreasureNum == 0) {
						mTreasureNum = 1;
				}
		}

		// Update is called once per frame
		void Update () {

		}
}
