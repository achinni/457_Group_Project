using UnityEngine;
using System.Collections;

public class AirConsole : MonoBehaviour {
	public GameObject Player1;
	public float distanceOfComp = 5;
	private bool playerCloseEnough = false;
	public GUIStyle notifStyle3 = new GUIStyle();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	/*calculates the distance between closet and player.
		If the distance is less than specified distance (here it is 5) then the player is close enough */
	void Update () {
		if(Vector3.Distance(this.transform.position, Player1.transform.position) < distanceOfComp){
			playerCloseEnough = true;
		}
		else{
			playerCloseEnough = false;
		}
	}
	
	void OnGUI () {
		//GUI.Label(Rectangle(x,y), message, message style)
		if (playerCloseEnough) {
			GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Press 1 to turn water into oxygen, and press 2 to refill space suit", notifStyle3);
		}
	}
}
