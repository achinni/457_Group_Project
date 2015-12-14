using UnityEngine;
using System.Collections;

public class tutorial : MonoBehaviour {
	public Texture gameOverTexture;
	public GUIStyle notifIntro = new GUIStyle();
	public GUIStyle notifMessage = new GUIStyle();
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),gameOverTexture);
			GUI.Label(new Rect(Screen.width/2, 10, 200f, 200f), "INTRODUCTION", notifIntro);
			GUI.Label(new Rect(20, 40, Screen.width-20,Screen.height-40), "You are caught on Mars. You are the only survivor! Your friends are on their way to rescue you. It will take them 7 days to reach here", notifMessage);
			GUI.Label(new Rect(20, 60, Screen.width-20,Screen.height-60), "GOAL: The goal of this game is to survive for 7 days.", notifMessage);
			GUI.Label(new Rect(20, 80, Screen.width-20,Screen.height-80), "You have resources (food, water, oxygen) to use inside your hab. You also have a space suit if you want to go out on the mars!", notifMessage);
			GUI.Label(new Rect(20, 180, Screen.width-20,Screen.height-180), "PROBLEM: As you use the resources, eventually you run out of them. You have to figure out a way to replenish.", notifMessage);
			GUI.Label(new Rect(20, 200, Screen.width-20,Screen.height-200), "DONT PANIC! Almost every night, there will be shower on mars. You can find ice blocks laying around the hab. You can go and collect them.", notifMessage);
			GUI.Label(new Rect(20, 220, Screen.width-20,Screen.height-220), "Using the ice you have collected, you can get water to drink, grow crops & food, and turn it into oxygen. You have equipment inside your hab which does that for you. :)", notifMessage);
			GUI.Label(new Rect(20, 240, Screen.width-20,Screen.height-240), "NOTE: Remember, you need to wear your space suit when you are going out of the hab. And be sure to return to the hab during night.", notifMessage);
		if (GUI.Button(new Rect(Screen.width / 2, Screen.height-50, 150, 25),"SKIP TO GAME")) 
		{
		 Application.LoadLevel(1);
		}
		if (GUI.Button(new Rect(Screen.width / 2, Screen.height-75, 150, 25),"Quit Game")) 
		{
				 //If we are running in a standalone build of the game
			#if UNITY_STANDALONE
				//Quit the application
				Application.Quit();
			#endif

				//If we are running in the editor
			#if UNITY_EDITOR
				//Stop playing the scene
				UnityEditor.EditorApplication.isPlaying = false;
			#endif
		}
	}
}
