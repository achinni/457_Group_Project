using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour {
	
	// Handling
	public float rotationSpeed = 450;
	public float walkSpeed = 5;
	public float encumberedSpeed = 2;
	public float runSpeed = 8;
	public float hungerSpeed = 1;
	public float airSpeed = 1;
	public float airSpeedRunning = 2;
	public float thirstSpeed = 1;
	public float thirstSpeedRunning = 2;
	public float habWaterSpeed = 1;
	public float habFoodSpeed = 1;
	public float habAirSpeed = 1;
	public float foodSpeed = 1;
	public float waterSpeed = 1;
	public float salvageWater = 100;
	public GameObject meteorObject;
	public int meteorNum = 10;

	public GameObject suit;

	public GUIStyle notifStyle3 = new GUIStyle();

	
	// System
	private Quaternion targetRotation;
	private float randX;
	private float randZ;
	
	// Components
	private CharacterController controller;
	private GameObject gameObject;
	private bool dressed;
	private bool inComputer;
	private bool inCloset;
	private bool inWaterConsole;
	private bool inFoodConsole;
	private bool inAirConsole;
	private bool outside;
	private bool inMeteor;
	private bool encumbered;
	private bool inDoor;
	private bool outDoor;
	private bool running;
	private bool dead;

	private GameObject meteor;
	private GameObject ice;

	//Player Stats
	private float hunger;
	private float thirst;
	private float air;
	private float walkSpeedStatic;
	private float airSpeedStatic;
	private float thirstSpeedStatic;


	//Hab Stats

	private float food;
	private float water;
	private float hab_air;
	private float day_count = 1;

	//GUI text for HUD

	public Light lt;
	public float duration = 10.0F;
	public bool timer =false;
	Vector3 temp = new Vector3(50.0f,0,0);  

	public GameObject hab_food_text;
	public GameObject hab_water_text;
	public GameObject hab_air_text;

	public GameObject player_hunger_text;
	public GameObject player_thirst_text;
	public GameObject player_air_text;

	public GameObject day_text;


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

		food = 100;
		water = 100;
		hab_air = 1000;

		hunger = 0;
		thirst = 0;
		air = 100;
		hab_food_text.GetComponent<Text> ().text = food.ToString();
		hab_water_text.GetComponent<Text> ().text = water.ToString();
		hab_air_text.GetComponent<Text> ().text = hab_air.ToString();

		player_hunger_text.GetComponent<Text> ().text = hunger.ToString();
		player_air_text.GetComponent<Text> ().text = "Not In Suit";
		player_thirst_text.GetComponent<Text> ().text = thirst.ToString();

		day_text.GetComponent<Text> ().text = day_count.ToString();

		walkSpeedStatic = walkSpeed;
		thirstSpeedStatic = thirstSpeed;
		airSpeedStatic = airSpeed;

		ice = transform.FindChild ("Ice").gameObject;

		for(int i = 0; i<meteorNum;i++){
			randX = Random.Range(-90f,90f);
			randZ = Random.Range(-90f,90f);
			if((randZ >20 || randX < -20) && (randX < 30 || randX > 2)){
				Instantiate(meteorObject,new Vector3(randX,0,randZ),Quaternion.identity);
			}
			else{
				i--;
			}
		}
	}
	
	void Update () {

		if (hunger >= 100) {
			GetComponent<Renderer>().enabled=false;
			Invoke("killThePlayer", 3);
		}

		if (thirst >= 100) {
			GetComponent<Renderer>().enabled=false;
			Invoke("killThePlayer", 3);
		}

		if (air <= 0) {
			GetComponent<Renderer>().enabled=false;
			Invoke("killThePlayer", 3);
		}

		if (hab_air <= 0 && dressed == false) {
			GetComponent<Renderer>().enabled=false;
			Invoke("killThePlayer", 3);
		}

		if (encumbered) {
			walkSpeed = encumberedSpeed;
		} else {
			walkSpeed = walkSpeedStatic;
		}

		if (Input.GetButtonDown ("Run")) {

			if(!running){
				walkSpeed = runSpeed;
				airSpeed = airSpeedRunning;
				thirstSpeed = thirstSpeedRunning;
			}
			else{
				walkSpeed = walkSpeedStatic;
				airSpeed = airSpeedStatic;
				thirstSpeed = thirstSpeedStatic;
				running = false;
			}
		}
		//Move PLayer
		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
		
		if (input != Vector3.zero) {
			targetRotation = Quaternion.LookRotation(input);
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetRotation.eulerAngles.y,rotationSpeed * Time.deltaTime);
		}
		
		Vector3 motion = input;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)?.7f:1;
		motion *= walkSpeed;
		motion += Vector3.up * -8;
		
		controller.Move(motion * Time.deltaTime);





		//End Move Player

		//Change into Spacesuit
		//checks to make sure the player is in the closet and whether or not they
		// are pressing the c key, and then changes their suit if both conditions
		//are met. 

		if (Input.GetKeyDown ("1") && inCloset == true) {

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

		//eat food

		if(Input.GetKeyDown("1") && inFoodConsole == true){
			print ("Eating food");
			if(food > 0){
				food -= hunger;
				hunger = 0;
			}
		}



		//generate water

		if(Input.GetKeyDown("1") && inWaterConsole == true){
			print ("Converting air to water");
			if(hab_air > 0){
				hab_air -= 10;
				water +=5;
			}

		}

		//drink water

		if(Input.GetKeyDown("2") && inWaterConsole == true){
			print ("drinking water");
			if(water > 0){
				water -= thirst;
				thirst = 0;
			}

		}

		//generate air
		if(Input.GetKeyDown("1") && inAirConsole == true){
			print ("Converting water to air");
			if(water > 0){
				water -= 10;
				hab_air += 5;
			}
		}

		//refill space suit
		if(Input.GetKeyDown("2") && inAirConsole == true){
			print ("refill suit");
			if(hab_air > 0){
				hab_air -= (100-air);
				air = 100;
			}
		}

		//harvest ice
		if(Input.GetKeyDown("1") && inMeteor == true){
			print ("pick up ice");
			encumbered = true;
			meteor.SetActive(false);
			ice.gameObject.SetActive(true);
		}

		//Control Player HUD Resource Display
		if (hunger >= 0) {
			hunger += Time.deltaTime * hungerSpeed;
			player_hunger_text.GetComponent<Text> ().text = hunger.ToString ("F0");
		}
		if (thirst >= 0) {
			thirst += Time.deltaTime * thirstSpeed;
			player_thirst_text.GetComponent<Text> ().text = thirst.ToString ("F0");
		}

		food += Time.deltaTime * foodSpeed;



		if (dressed) {
			air -= Time.deltaTime * airSpeed;
			player_air_text.GetComponent<Text> ().text = air.ToString ("F0");
		} else {
			player_air_text.GetComponent<Text> ().text = "Not In Suit";
		}

		//Control HAB HUD Resource Display
		hab_food_text.GetComponent<Text> ().text = food.ToString("F0");
		hab_air -= Time.deltaTime * habAirSpeed;
		hab_air_text.GetComponent<Text> ().text = hab_air.ToString("F0");
		if (water > 0) {
			water -= Time.deltaTime * habWaterSpeed;
		}
		hab_water_text.GetComponent<Text> ().text = water.ToString("F0");



		//Day Night Setting

		if (timer == false) {
			duration -= Time.deltaTime;
		}
		
		if(duration == 4){
			for(int tempi = 3; tempi >=0; tempi--){
				lt.enabled = false;
				Invoke("makeItDark",1);
			}
			lt.intensity = 0.3F;
		}
		if (duration <= 0) {
			lt.intensity = 0.1F;
			if(outside == true){
				GetComponent<Renderer>().enabled=false;
				Invoke("killThePlayer", 3);
			}
			else if(outside==false){
				day_count++;

				for(int i = 0; i<meteorNum;i++){
					randX = Random.Range(-90f,90f);
					randZ = Random.Range(-90f,90f);
					if((randZ >20 || randX < -20) && (randX < 30 || randX > 2)){
						Instantiate(meteorObject,new Vector3(randX,0,randZ),Quaternion.identity);
					}
					else{
						i--;
					}
				}
			}

			timer = true;
			day_text.GetComponent<Text>().text = day_count.ToString();
		}
		
		if (timer == true) {
			duration += Time.deltaTime;
		}
		
		if (duration >= 5) {
			lt.intensity=1;
			timer = false;
			duration = 10;
		}
		
		if(duration == 7)
		{
			Invoke("killThePlayer", 5);
		}


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
			ComputerSound.Play ();
		} else if (other.name == "Water_Console_Trigger") {
			print("you are at the water console");
			inWaterConsole = true;
		} else if (other.name == "Food_Console_Trigger") {
			print("you are at the food console");
			inFoodConsole = true;
		} else if (other.name == "Air_Console_Trigger") {
			print("you are at the food console");
			inAirConsole = true;
		} else if (other.name == "AirlockOut" && dressed == false) {
			dead = true;
			GetComponent<Renderer>().enabled=false;
			Invoke("killThePlayer", 2);
			//DoorSound.Play();
			print ("you are dead, you are dead");
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
			if(encumbered){
				water += salvageWater;
			}
			encumbered = false;
			ice.gameObject.SetActive(false);
			outside = false;
			DoorSound.Play();
			if(windSound.isPlaying)
			{
				windSound.Stop();
				mainSound.Play();
			}

		}else if (other.tag == "meteor") {
			print ("you found a meteor");
			meteor = GameObject.Find(other.name);
		 	inMeteor = true;
		}

	

	}
	//Built in
	void OnTriggerExit(Collider other){
	//This resets the bools that track entering and exiting when the player leaves the trigger.
		inCloset = false;
		inComputer = false;
		inFoodConsole = false;
		inAirConsole = false;
		inWaterConsole = false;
		inMeteor = false;
	}
	
	void OnGUI () {
	//GUI.Label(Rectangle(x,y), message, message style)
		if (dead) {
		 GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "You are dead due to lack of oxygen! Try again!!.", notifStyle3);
		}
		if (hunger >= 100) {
		 GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "You died of hunger! Try again!!.", notifStyle3);
		}

		if (thirst >= 100) {
		 GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "You died of thirst! Try again!!.", notifStyle3);
		}

		if (air <= 0) {
		 GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "You ran out of air! You are dead!! Try again!!.", notifStyle3);
		}

		if (hab_air <= 0 && dressed == false) {
		 GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "You ran out of oxygen! Try again!!.", notifStyle3);
		}
		if(duration <= 0 && outside == true){
			GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "It is dark outside! You are not supposed to leave the hab! Better luck next time!!", notifStyle3);
		}
		if(duration == 3){
			GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "WARNING: It is going to be dark. Get inside the hab!", notifStyle3);
		}
		if(duration == 7)
		{
			GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Yea!! You won!", notifStyle3);
		}
	}
	
	void killThePlayer()
	{
		transform.gameObject.SetActive (false);
		Application.LoadLevel(0);
	}
	
	void makeItDark(){
		lt.enabled=true;
	}
}
