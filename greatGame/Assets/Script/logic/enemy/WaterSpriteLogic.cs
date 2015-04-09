using UnityEngine;
using System.Collections;

public class WaterSpriteLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

        void onTriggerEnter(Collider other) {
                GameObject obj = constant.getBaseParentGameObject(this.gameObject);
                WaterLogic waterLogic = obj.GetComponent<WaterLogic>();
                waterLogic.onTriggerEnter(other);
        }

        void onTriggerStay(Collider other) {
                GameObject obj = constant.getBaseParentGameObject(this.gameObject);
                WaterLogic waterLogic = obj.GetComponent<WaterLogic>();
                waterLogic.onTriggerStay(other);
        }

        void onTriggerExit(Collider other) {
                GameObject obj = constant.getBaseParentGameObject(this.gameObject);
                WaterLogic waterLogic = obj.GetComponent<WaterLogic>();
                waterLogic.onTriggerExit(other);
        }
}
