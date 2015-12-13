using UnityEngine;
using System.Collections;

public class MapCameraController : MonoBehaviour {
	
	public GameObject player;
	public float camHeight;

	
	private Vector3 camLocation;
	// Use this for initialization
	void Start () {
		camLocation = new Vector3(player.transform.position.x,camHeight,player.transform.position.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		camLocation.x = player.transform.position.x;
		camLocation.y = camHeight;
		camLocation.z = player.transform.position.z;
		transform.position = camLocation;
		
		
		
	}
}
