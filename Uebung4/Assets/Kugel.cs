using UnityEngine;
using System.Collections;

public class Kugel : MonoBehaviour {

public Vector3 startpos; 
public static float poweruprate = 0;
public GameObject kamera;
private bool touchingPlatform; 	
public bool boost = false;
public Booster booster;

	
	
	// Use this for initialization
	void Start () {
		 startpos = transform.position;  
	}
	
	
	// Update is called once per frame
	void Update () {
		kamera.transform.position = new Vector3(transform.position.x, transform.position.y+2.2f, transform.position.z-10); 
		if(transform.position.y <= -10)
		{ 
			startpos = new Vector3 (transform.position.x,5,0);
			respawn();
			return;
		}
		
		if (Input.GetKeyUp(KeyCode.RightArrow)) {
			if(rigidbody.velocity.x < 5) {
				rigidbody.velocity = new Vector3(5,0,0);
			}
		}
	
		if (Input.GetKeyUp(KeyCode.LeftArrow)){
			if(rigidbody.velocity.x > -5) {
				rigidbody.velocity = new Vector3(-5,0,0);
			}
		}
		//Springen: wenn Kugel Platform berührt, also touchingPlatform auf true ist und Space gedrückt wird
		if(touchingPlatform && Input.GetButtonDown("Jump")){
			rigidbody.velocity = new Vector3(0,10,0);;
		}
	}
		
	void OnCollisionEnter () {
		touchingPlatform = true;
	}
	
	void onTriggerEnter() {
		Debug.Log("hit booster");
	}

	void OnCollisionExit () {
		touchingPlatform = false;
	}
	
	
	void respawn ()
	{
	    rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		transform.position = startpos;
		poweruprate = poweruprate;
			
	}
	
	public void AddBoost() {
		boost = true;
		//poweruprate = booster.CountPowerupRate("kugel");
		
		//hier oder in Update noch, stärkere Kraft hinzufügen für kurze Zeit
	}
}

