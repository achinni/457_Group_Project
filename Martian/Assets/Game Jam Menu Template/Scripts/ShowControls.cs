using UnityEngine;
using System.Collections;

public class ShowControls : MonoBehaviour {

	public GameObject controlsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject optionsTint;							//Store a reference to the Game Object OptionsTint 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	

	//Call this function to activate and display the Options panel during the main menu
	public void ShowControlsPanel()
	{
		controlsPanel.SetActive(true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideControlsPanel()
	{
		controlsPanel.SetActive(false);
		optionsTint.SetActive(false);
	}
	
	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
		menuPanel.SetActive (true);
	}

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		menuPanel.SetActive (false);
	}
}
