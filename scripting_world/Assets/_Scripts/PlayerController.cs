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
	
	
	void Start () {
		controller = GetComponent<CharacterController>();
		gameObject = GetComponent<GameObject>();
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


		if (Input.GetKeyDown ("c") && inCloset == true) {

			if(dressed){
				print("taking off");
				suit.SetActive(false);
				dressed = false;
			}

			else{
				print ("putting on");
				suit.SetActive(true);
				dressed = true;
			}
			//renderer.material = suitOn;
		}
		//End Change into Spacesuit
	}

	void OnTriggerStay(Collider other){

		if (other.name == "Closet") {
			inCloset = true;
		} 
		else if (other.name == "Computer") {
			inComputer = true;
		}


//		if(Input.GetKeyDown("c")){
//
//			if(dressed){
//				print("taking off");
//				suit.SetActive(false);
//				dressed = false;
//			}
//
//			else{
//				print ("putting on");
//				suit.SetActive(true);
//				dressed = true;
//			}
//			//renderer.material = suitOn;
//		}
	}

	void OnTriggerExit(Collider other){
		inCloset = false;
		inComputer = false;
	}
}
