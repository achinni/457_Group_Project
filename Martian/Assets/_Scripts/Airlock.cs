using UnityEngine;
using System.Collections;

public class Airlock : MonoBehaviour {

	public GameObject Player3;
	public float distanceOfAirlock = 5;
	private bool playerCloseEnough = false;
	public GUIStyle notifStyle4 = new GUIStyle();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	/*calculates the distance between airlock and player.
		If the distance is less than specified distance (here it is 5) then the player is close enough */
	void Update () {
		if(Vector3.Distance(this.transform.position, Player3.transform.position) < distanceOfAirlock){
			playerCloseEnough = true;
		}
		else{
			playerCloseEnough = false;
		}
	}
	
	void OnGUI () {
     //GUI.Label(Rectangle(x,y), message, message style)
	 if (playerCloseEnough) {
         GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Player approaching Airlock...Make sure that the suit is on.", notifStyle4);
     }
	}
}
