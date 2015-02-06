using UnityEngine;
using System.Collections;

public class uilogic : MonoBehaviour, notifylogic {

	public GameObject mDieMenuPrefabs;		//主角prefab

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Die(){
		showDieMenu ();
	}

	void MyDelegateFunc(tk2dButton source)
	{
		Debug.Log ("delegate function...");

		constant.getMapLogic ().resetStartGame ();
		Application.LoadLevel(0);
	}  

	public void showDieMenu(){

		GameObject camera = constant.getMainCamera ();
		
		Vector3 v = new Vector3 ();
		v.x = -0f+camera.transform.position.x;
		v.y = -0.92f+camera.transform.position.y;
		v.z = 1f+camera.transform.position.z;
		GameObject menu = (GameObject)Instantiate(mDieMenuPrefabs,v,Quaternion.identity);
		
		menu.transform.parent = camera.transform;
		GameObject btn = GameObject.Find ("prevButton");
		tk2dButton button = btn.GetComponent<tk2dButton>();
		button.ButtonUpEvent += new tk2dButton.ButtonHandlerDelegate(MyDelegateFunc);

	}
	  
}
