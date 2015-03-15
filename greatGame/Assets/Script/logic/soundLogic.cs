using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class soundLogic {
		
		private static soundLogic mInstance;

		public bool sound;
		public bool effect;


		private soundLogic(){
				
		}

		static public soundLogic getInstance(){
				if (mInstance == null) {
						mInstance = new soundLogic();
				}
				return mInstance;
		}

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		public void playEffect(){
				
		}
				
		public void playBackGround(){
				
		}

		public void playSound(){

		}
}
