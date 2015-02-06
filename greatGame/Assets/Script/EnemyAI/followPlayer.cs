using UnityEngine;
using System.Collections;

public class followPlayer : MonoBehaviour {

	// Use this for initialization
	GameObject enemy;
	Hashtable args = new Hashtable();
	public GameObject player;
	Vector3 playerPos;
	public Transform [] paths;

	float timeO = 0.1f;
	float timeP = 0;

	public float enemySpeed;

	void Start () {
		player = GameObject.FindWithTag("Player");
		enemy = this.gameObject;

		//设置路径的点
		//args.Add("path",paths);

		//设置类型为线性，线性效果会好一些。
		args.Add("easeType", iTween.EaseType.linear);
		//设置寻路的速度

		args.Add("speed",enemySpeed);
		//是否先从原始位置走到路径中第一个点的位置


		args.Add("position",playerPos);

		args.Add("islocal",true);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (player) {
						getPlayerPos ();
						if (timeP >= timeO) {
								args.Remove ("position");
								args.Add ("position", playerPos);
								iTween.MoveTo (enemy, args);
								timeP = 0;
						}
						timeP += Time.deltaTime;
				}
	}

	void getPlayerPos(){
		playerPos = player.transform.position;
	}

	void OnDrawGizmos()
	{
		//在scene视图中绘制出路径与线
		//iTween.DrawLine(paths,Color.yellow);
		
		//iTween.DrawPath(paths,Color.red);
		
	}

}
