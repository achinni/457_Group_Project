using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 camLocation;
	private float indoorsCamHeight = 8;
	private float outdoorsCamHeight = 15;
	// Use this for initialization
	void Start () {
		camLocation = new Vector3(player.transform.position.x,indoorsCamHeight,player.transform.position.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		camLocation.x = player.transform.position.x;
		camLocation.y = indoorsCamHeight;
		camLocation.z = player.transform.position.z;
		transform.position = camLocation;

	}
}
