using UnityEngine;
using System.Collections;

public class Computer : MonoBehaviour {
	public GameObject Player1;
	public float distanceOfComp = 5;
	private bool playerCloseEnough = false;
	public GUIStyle notifStyle3 = new GUIStyle();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(this.transform.position, Player1.transform.position) < distanceOfComp){
			playerCloseEnough = true;
		}
		else{
			playerCloseEnough = false;
		}
	}
	
	void OnGUI () {
     if (playerCloseEnough) {
         GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Player approaching computer...", notifStyle3);
     }
	}
}
