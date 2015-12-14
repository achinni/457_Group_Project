using UnityEngine;
using System.Collections;

public class tutorial : MonoBehaviour {
	public Texture gameOverTexture;
	public GUIStyle notifIntro = new GUIStyle();
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),gameOverTexture);
			GUI.Label(new Rect(10, 10, 200f, 200f), "Message goes here", notifIntro);
		if (GUI.Button(new Rect(Screen.width / 2, Screen.height /2, 150, 25),"SKIP TO GAME")) 
		{
		 Application.LoadLevel(1);
		}
		if (GUI.Button(new Rect(Screen.width / 2, Screen.height /2 + 25, 150, 25),"Quit Game")) 
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
