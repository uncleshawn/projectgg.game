using UnityEngine;
using System.Collections;
//旋转子弹到正确碰撞动画方向
public class bulletAutoRotate : MonoBehaviour {

		bullet_property bulletProperty;
		tk2dSprite bulletSprite;
		void Awake(){
				bulletProperty = gameObject.GetComponent<bullet_property> ();
				bulletSprite = transform.FindChild ("ui").FindChild ("bulletPic").GetComponent<tk2dSprite> ();
		}
		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		float angle_360(Vector3 from_, Vector3 to_){ 
				Vector3 v3 = Vector3.Cross(from_,to_); 
				if(v3.z > 0) return Vector3.Angle(from_,to_); 
				else return 360-Vector3.Angle(from_,to_); 
		}

		void OnTriggerEnter(Collider other){
				if (bulletProperty.BattleType == constant.BattleType.Player) {

						enemy_property enemyPro = other.gameObject.GetComponent<enemy_property> ();
						if (enemyPro) {
								Vector3 targerPos = other.gameObject.transform.position;
								Vector3 selfPos = transform.position;
								Vector3 rotatePos = targerPos - selfPos;
								Vector3 angle = new Vector3(0,0,angle_360 (new Vector3 (1, 0, 0), rotatePos));
								//Debug.Log ("子弹和X轴角度: " + angle);
								bulletSprite.transform.rotation = new Quaternion();
								bulletSprite.transform.Rotate (angle);

						}
				}
				if (bulletProperty.BattleType == constant.BattleType.Enemy) {
						char_property charPro = other.gameObject.GetComponent<char_property> ();
						if (charPro) {
								Vector3 targerPos = other.gameObject.transform.position;
								Vector3 selfPos = transform.position;
								Vector3 rotatePos = targerPos - selfPos;
								Vector3 angle = new Vector3(0,0,angle_360 (new Vector3 (1, 0, 0), rotatePos));
								//Debug.Log ("子弹和X轴角度: " + angle);
								bulletSprite.transform.rotation = new Quaternion();
								bulletSprite.transform.Rotate (angle);
						}
				}
		}

}
