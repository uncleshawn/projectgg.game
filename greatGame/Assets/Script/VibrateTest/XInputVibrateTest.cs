//using UnityEngine;
//using System.Collections;
//using XInputDotNetPure; 
//
//public class XInputVibrateTest : MonoBehaviour {
//
//	// Use this for initialization
//	public float originLeft  = 0;
//	public float originRight = 0;
//	public float testC;
//	public float testD;
//	void Start () {
//
//	}
//	
//	// Update is called once per frame
//	void Update () {
//
//	}
//	public void joystickVibrate(float delayTime,float leftV, float rightV){
//		originLeft += leftV;
//		originRight += rightV;
//		GamePad.SetVibration(PlayerIndex.One,originLeft,originLeft);
//		StartCoroutine(stopVibrate(delayTime,leftV,rightV));
//	}
//
//	IEnumerator stopVibrate(float delayTime,float leftV, float rightV){
//		yield return new WaitForSeconds(delayTime);
//		originLeft  -= leftV;
//		originRight -= rightV;
//		GamePad.SetVibration(PlayerIndex.One,originLeft,originRight);
//
//	}
//}
