using UnityEngine;
using System.Collections;

public class _MainGame : MonoBehaviour {
	public GameObject meteor;
	public int meteorNum = 10;

	private float randX;
	private float randZ;
	// Use this for initialization
	void Start () {

		for(int i = 0; i<meteorNum;i++){
			randX = Random.Range(-90f,90f);
			randZ = Random.Range(-90f,90f);
			if((randZ >20 || randX < -20) && (randX < 30 || randX > 2)){
				Instantiate(meteor,new Vector3(randX,0,randZ),Quaternion.identity);
			}
			else{
				i--;
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
