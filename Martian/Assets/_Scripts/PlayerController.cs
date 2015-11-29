using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour {
	
	// Handling
	public float rotationSpeed = 450;
	public float walkSpeed = 5;
	public float runSpeed = 8;

	public GameObject suit;
	
	// System
	private Quaternion targetRotation;
	
	// Components
	private CharacterController controller;
	private GameObject gameObject;
	private bool dressed;
	private bool inComputer;
	private bool inCloset;
	private bool outside;	
	
	//Sounds
	public AudioSource mainSound;
	public AudioSource windSound;
	public AudioSource DoorSound;
	public AudioSource SuitSound;
	public AudioSource ComputerSound;

	void Start () {
		controller = GetComponent<CharacterController>();
		gameObject = GetComponent<GameObject>();
		mainSound.Play();
	}
	
	void Update () {

		//Move PLayer
		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
		
		if (input != Vector3.zero) {
			targetRotation = Quaternion.LookRotation(input);
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetRotation.eulerAngles.y,rotationSpeed * Time.deltaTime);
		}
		
		Vector3 motion = input;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)?.7f:1;
		motion *= (Input.GetButton("Run"))?runSpeed:walkSpeed;
		motion += Vector3.up * -8;
		
		controller.Move(motion * Time.deltaTime);

		//End Move Player

		//Change into Spacesuit
		//checks to make sure the player is in the closet and whether or not they
		// are pressing the c key, and then changes their suit if both conditions
		//are met. 

		if (Input.GetKeyDown ("c") && inCloset == true) {

			if(dressed){
				print("taking off");
				SuitSound.Play();
				suit.SetActive(false);
				dressed = false;
			}

			else{
				print ("putting on");
				SuitSound.Play();
				suit.SetActive(true);
				dressed = true;
			}
		}
		//End Change into Spacesuit
	}
	//Built in.
	void OnTriggerStay(Collider other){
	//Checks for whether or not the player is colliding with a trigger
	//Tells the player whether or not it is inside the closet or by the computer and thus allows
	//the game to know whether or not the player is allwed to put their suit on. 

		if (other.name == "Closet") {
			inCloset = true;
		} else if (other.name == "Computer") {
			inComputer = true;
			ComputerSound.Play();
		} else if (other.name == "AirlockOut" && dressed == false) {
			transform.gameObject.SetActive (false);
			//DoorSound.Play();
			print ("you are dead, you are dead");
			Application.LoadLevel(0);
		} 
		else if (other.name == "AirlockOut" && dressed == true) {
			outside = true;
			//DoorSound.Play();
			if(mainSound.isPlaying)
			{
				mainSound.Stop();
				windSound.Play();
			}
		}
		else if (other.name == "AirlockIn" && dressed == true) {
			outside = false;
			DoorSound.Play();
			if(windSound.isPlaying)
			{
				windSound.Stop();
				mainSound.Play();
			}
		}

	}
	//Built in
	void OnTriggerExit(Collider other){
	//This resets the bools that track entering and exiting when the player leaves the trigger.
		inCloset = false;
		inComputer = false;
	}
}
