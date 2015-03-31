using UnityEngine;
using System.Collections;

public class pos_update_script : MonoBehaviour {

		private Transform m_transform;
		private Vector3 originPos;
		// Use this for initialization

		void Awake(){
				originPos = transform.localPosition;
		}
		void Start () {
				m_transform = this.transform;
		}

		// Update is called once per frame
		void Update () {
		}

		void FixedUpdate(){
				Transform parentTransform = this.GetComponentInParent<Transform>();
				float z = originPos.z + parentTransform.position.y*0.01f;
				//m_transform.position.Set (m_transform.position.x, m_transform.position.y, 100);

				GameObject obj = this.gameObject;
				obj.transform.localPosition = new Vector3 (obj.transform.localPosition.x, obj.transform.localPosition.y, z);
		}
}
