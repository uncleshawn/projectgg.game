using UnityEngine;
using System.Collections;

public class robotWeaponSwitch : MonoBehaviour {

		GameObject shoot;
		public robotShotNormalBullet bulletNormal;
		public robotShotLaserNomal laserNormal;

		select_name_bool otherStru;

		// Use this for initialization
		void Start () {
				shoot = transform.FindChild("shoot").gameObject;
				laserNormal = transform.FindChild("shoot").GetComponent<robotShotLaserNomal>();
				bulletNormal = transform.FindChild("shoot").GetComponent<robotShotNormalBullet>();
				disableAllWeapon();
				enableComponent(laserNormal);
		}

		// Update is called once per frame
		void Update () {

		}


		public bool switchWeapon(weaponItem_property weapon){
				weaponType weapontype = weapon.mType;
				switch(weapontype){
				default: 
						break;

				case weaponType.bulletNormal:	
						disableAllWeapon();	 
						enableComponent(bulletNormal);	
						break;
				case weaponType.laserNormal:	
						disableAllWeapon ();	 
						enableComponent (laserNormal);		
						break;
						return false;
						break;
				}
				upgradeShootProperties (weapon);
				return true;

		}

		void enableComponent(Component gameCompo){

				otherStru.name = gameCompo.GetType().ToString() ;
				otherStru.choose = true;
				shoot.SendMessage("setEnabled",otherStru);

		}

		void disableComponent(Component gameCompo){

				otherStru.name = gameCompo.GetType().ToString() ;
				otherStru.choose = false;
				shoot.SendMessage("setEnabled",otherStru);
		}



		void disableAllWeapon(){
				shoot.SendMessage("disableAll");
		}

		public void upgradeShootProperties(weaponItem_property weapon){
				GameObject shoot = this.transform.FindChild("shoot").gameObject;
				if(shoot){
						shoot.SendMessage("setBaseProperty",weapon);
				}
		}

}
