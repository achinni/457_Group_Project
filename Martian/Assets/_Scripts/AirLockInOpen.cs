using UnityEngine;
using System.Collections;

public class AirLockInOpen : MonoBehaviour {
	public GameObject Player1;
	public GameObject AirlockOuter;
	public float distanceOfComp = 1;
	private bool playerCloseEnough = false;
	private Vector3 startPoint;
	private Vector3 endPoint;
	public GUIStyle notifStyle3 = new GUIStyle();
	private bool open;
	// Use this for initialization
	void Start () {
		startPoint = transform.position;
		endPoint = new Vector3 (startPoint.x, startPoint.y, startPoint.z + 2);

	}
	
	// Update is called once per frame
	/*calculates the distance between closet and player.
		If the distance is less than specified distance (here it is 5) then the player is close enough */
	void Update () {


		if(Vector3.Distance(this.transform.position, Player1.transform.position) < distanceOfComp){
			if(!open){
				transform.position = Vector3.Lerp(startPoint,endPoint, Time.time);
			}
			open = true;
		}
		else if(open == true){
			endPoint = transform.position;
			transform.position = Vector3.Lerp(endPoint,startPoint, Time.time);
		}
	}


	

}
