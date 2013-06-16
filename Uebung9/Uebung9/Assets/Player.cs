using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Transform player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float deltaX = Input.GetAxis("Mouse X");
		float deltaY = Input.GetAxis("Mouse Y");
	   
	   	//if(deltaX != 0 || deltaY != 0) {
	    	player.transform.position = new Vector3(deltaY, 0, deltaX);
	   	//}
		
	}
}
