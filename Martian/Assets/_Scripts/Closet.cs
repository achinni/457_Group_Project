using UnityEngine;
using System.Collections;

public class Closet : MonoBehaviour {

	public GameObject Player;
	public float distance = 5;
	private bool playerCloseEnough = false;
	public GUIStyle notifStyle = new GUIStyle();
	public GUIStyle notifStyle2 = new GUIStyle();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*calculates the distance between closet and player.
		If the distance is less than specified distance (here it is 5) then the player is close enough */
		if(Vector3.Distance(this.transform.position, Player.transform.position) < distance){
			playerCloseEnough = true;
		}
		else{
			playerCloseEnough = false;
		}
	}
	
	void OnGUI () {
     if (playerCloseEnough) {
		 //GUI.Label(Rectangle(x,y), message, message style)
         GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Player approaching closet...", notifStyle);
		 GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Press 'c' to wear/remove suit when you are in the closet.", notifStyle2);
     }
	}
}
