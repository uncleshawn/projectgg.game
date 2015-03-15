using UnityEngine;
using System.Collections;

public class weaponItem_property : MonoBehaviour {
		public weaponType mType;

		public float baseBulletRate;			//子弹origin间隔时间
		public float baseBulletSpeed;			//子弹的origin速度
		public float baseBulletDistance;		//子弹的origin距离
		public int baseBulletDamage;			//子弹的基础攻击
		public int mknockBack;					//子弹的击退效果

		public ElementType elementType;			//子弹的特效－元素属性
		public bool pierceBullet;
		public bulletSpeStruct bulletSpe;		//子弹的特效

		private int mID;
		public int ID { get { return mID; } set { mID = value; }}
		// Use this for initialization
		void Awake(){
				item_property itemProperty = gameObject.GetComponent<item_property>();
				mID = itemProperty.ID;
				setBulletSpecial ();

		}
		void Start () {
				
				if (mType == 0) {
						getWeaponType (mID);
						Debug.LogError ("忘记给武器道具定义类型");
				}

		}

		// Update is called once per frame
		void Update () {

		}

		void getWeaponType(int id){
				Debug.Log ("武器id搜索匹配失败,请手动定义!");

		}

		void setBulletSpecial(){
				if (elementType!=0) {
						bulletSpe.element = elementType;
				} else {
						bulletSpe.element = ElementType.normal;
				}

				bulletSpe.pierceBullet = pierceBullet;

		}
}
