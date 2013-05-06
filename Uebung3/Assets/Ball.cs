using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	public Material Material1;
    public Material Material2;
	
	private BallColor ballcolor;
	private Scoreboard scoreboard;
	private GameControl gameControl;
	
	public float startTime; 
	public float duration;
	public Vector3 accVector;
	public float horizontalSpeed = 2000.0F;
	private int fallenPins;
	
	
	public Vector3 startPos;
	
	// Use this for initialization
	void Start () {
		ballcolor = new BallColor(Material1, Material2, renderer);
		scoreboard = new Scoreboard(); 
		gameControl = new GameControl();
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// 0 is the number of the left mouse button
		CheckMouseState(0);
		CheckFallen();
		if(gameControl.CheckForReset(this)) {
			scoreboard.StoreRoundPoints(fallenPins);
			gameControl.Reset(this);
			ballcolor.setColor(0);
		}
					
		if(Input.GetMouseButtonDown(0)){
			
			/*
			Debug.Log("Pressed left mouse button!");
			float neuesX = rigidbody.position.x - Input.GetAxis("Mouse X");
			neuesX = neuesX*-1;
			float neuesY = rigidbody.position.y - Input.GetAxis("Mouse Y");    
			float neuesZ = rigidbody.position.z; 
			rigidbody.position = new Vector3 (neuesX,neuesY,neuesZ);
			*/
			float neuesX = rigidbody.position.x - Input.mousePosition.x;
			float neuesY = rigidbody.position.y - Input.mousePosition.y;    
			float neuesZ = rigidbody.position.z; 
			rigidbody.position = new Vector3 (neuesX, neuesY, neuesZ);
		}
			
		if (Input.GetMouseButtonUp(0)) {
			rigidbody.AddForce(Vector3.forward * -1500 * duration);
		}
			
				/*Test:
			float mouseY = rigidbody.position.y - Input.mousePosition.y;
			float h = Input.GetAxis("Mouse X");
			transform.Rotate(new Vector3(h,0,0) * Time.deltaTime); 
			transform.Rotate(accVector); 
			accVector = new Vector3 (h, 0,0);
			
			accVector = new Vector3 (mouseX, 0, duration/5*-10000);
			accVector = new Vector3 (mouseX, 0,-2500);
			rigidbody.AddForce(accVector);
			Debug.Log("Clicked at " + Input.mousePosition);
			*/
		
	}
	
	

	void OnGUI ()
	{
		scoreboard.Update();
	}
	
	private void CheckFallen() {
		fallenPins = 0;
		foreach(GameObject kegel in GameObject.FindGameObjectsWithTag("respawn")) {
			
			if((kegel.name == "Cylinder") && kegel.rigidbody.position.y < 8) {
				fallenPins++;
			}
		}	
		scoreboard.setFallenPins(fallenPins);
	}
	
	

	private void CheckMouseState(int buttonNr) {
		if(Input.GetMouseButtonDown(buttonNr) == true) {
			startTime = Time.time;
		}
		
		if(Input.GetMouseButton(buttonNr) == true) {
			duration = Time.time - startTime;
			ballcolor.setColor(duration);
		}
		
	}
	

}
