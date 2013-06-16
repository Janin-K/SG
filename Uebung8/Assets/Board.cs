using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {
	public Rigidbody ballPrefab;
	private Rigidbody ball;
	private GameObject gameBoard;
	private bool mouse = false;
	
	private float delay = 0.0F;
	private float sensitivity = 1.0F;
	
	// Use this for initialization
	void Start () {
		ball = Instantiate(ballPrefab) as Rigidbody;
		ball.transform.position = new Vector3(-4,2,-4);
		ball.rigidbody.useGravity = true;
		ball.name = "Ball";
		
		gameBoard = GameObject.FindGameObjectWithTag("gameBoard");	
		
		mouse = false;
	}
	
	void FixedUpdate () {
		float SecToWait = delay * 0.01f;
		StartCoroutine(GameFlow(SecToWait));	
		
		if(ball.rigidbody.position.y < -10)
		{
			gameBoard.transform.rotation = new Quaternion(0,0,0,1);
			ball.transform.position = new Vector3(-4,2,-4);
		}
	}
	
	void OnTriggerEnter(Collider Goal) {
		if(Goal.name == "Ball"){
			gameBoard.transform.rotation = new Quaternion(0,0,0,1);
			ball.transform.position = new Vector3(-4,2,-4);
		
		}
	}
	
	void OnGUI(){
		
		delay = GUILayout.HorizontalSlider(delay,0.0F,1000.0F);
		GUILayout.Label("Delay = " + delay * 0.01f + " seconds");
		
		sensitivity = GUILayout.HorizontalSlider(sensitivity,1.0F,100.0F); 
		GUILayout.Label("Sensitivity = " + sensitivity);
		
		if(mouse == false)
		{
		
			if (GUI.Button (new Rect (0,90,150,30), "Switch to mouse")) {
			mouse = true;
			}
		}
		else
		{
			if (GUI.Button (new Rect (0,90,150,30), "Switch to keyboard")) {
			mouse = false;
			}
		}
	}
	
	IEnumerator GameFlow(float seconds)
    {
		if(mouse == false)
		{	
			float deltaX = Input.GetAxisRaw("Horizontal");
			float deltaY = Input.GetAxisRaw("Vertical");
			
			if(deltaX != 0 || deltaY != 0) {
		    	yield return new WaitForSeconds(seconds);
				gameBoard.transform.Rotate(new Vector3(deltaY * sensitivity * 0.03f, 0, deltaX * sensitivity * -0.03f));
			}
		}
		
		if(mouse == true)
		{		
			float deltaX = Input.GetAxis("Mouse X");
			float deltaY = Input.GetAxis("Mouse Y");
			
			if(deltaX != 0 || deltaY != 0) {
		    	yield return new WaitForSeconds(seconds);
				gameBoard.transform.Rotate(new Vector3(deltaY * sensitivity * 0.03f, 0, deltaX * sensitivity * -0.03f));
			}
		}
    }
}

