//这个道具将会在被玩家拾取后显示在玩家身上
//这个道具将会在背包里留下图标


using UnityEngine;
using System.Collections;

public class equipItem_Property : MonoBehaviour {

	public int id;
	public int itemId { get { return id; } set { itemId = id; }}
	// Use this for initialization
	void Awake(){
		item_property itemProperty = gameObject.GetComponent<item_property>();
		id = itemProperty.itemId;
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
