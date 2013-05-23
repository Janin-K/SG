using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public Transform PlayerPrefab;
	public Transform WallPrefab;
	public Transform BallPrefab;
	public Transform ColliderPrefab;
	private Transform player1;
	private Transform player2;
	private Transform Wall1;
	private Transform Wall2;
	private Transform Ball;
	private Transform Colrechts;
	private Transform Collinks;
	private int pointsClient;
	private int	pointsServer;
	
	
	// Use this for initialization
	void Start () {
		CreateScene();
		pointsClient = 0;
		pointsServer = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Network.peerType == NetworkPeerType.Client)
		{
			if(Input.GetKey("up"))
			{
				player2.transform.position += Vector3.up * 0.1f;
			}
			if(Input.GetKey("down"))
			{
				player2.transform.position += Vector3.down * 0.1f;	
			}
		
		}
		else if (Network.peerType == NetworkPeerType.Server)
		{
			if(Input.GetKey("up"))
			{
				player1.transform.position += Vector3.up * 0.1f;
			}
			if(Input.GetKey("down"))
			{
				player1.transform.position += Vector3.down * 0.1f;	
			}
			
		}
		
	}
	
	public void CreateScene()
	{
		Wall1 = Instantiate(WallPrefab) as Transform;
		Wall1.transform.position = new Vector3(0,4,0);
		
		Wall2 = Instantiate(WallPrefab) as Transform;
		Wall2.transform.position = new Vector3(0,-4,0);
		
		Colrechts = Instantiate(ColliderPrefab) as Transform;
		Colrechts.transform.position = new Vector3(9.5f,0,0);
		
		Collinks = Instantiate(ColliderPrefab) as Transform;
		Collinks.transform.position = new Vector3(-9.5f,0,0);
	}
	
	// funktioniert noch nicht :-(
	// weiss nicht wie ich die Collider unterscheiden kann
	//tut gar nichts im Moment
	void OnCollisionEnter(Collision Aus)
	{
		if(Aus.rigidbody == Colrechts){
			Debug.Log("rechts");
			pointsServer +=1;
			Destroy(Ball);
			respawn(-1);
		}
		if(Aus.rigidbody == Collinks){
			Debug.Log("links");
			pointsClient +=1;
			Destroy(Ball);
			respawn(1);
		}
	}
	
	
	void OnPlayerConnected()
	{

		Vector3 pos1 = new Vector3(-9,0,0);
		Quaternion rot1 = new Quaternion(0,0,0,0);
		player1 = Network.Instantiate(PlayerPrefab,pos1,rot1,0) as Transform;
		
		Vector3 posBall = new Vector3(0,0,0);
		Quaternion rotBall = new Quaternion(0,0,0,0);
		Ball = Network.Instantiate(BallPrefab,posBall,rotBall,0) as Transform;
		Ball.transform.rigidbody.AddForce(new Vector3(500,0,0));
		
		
	}
	
	void OnConnectedToServer()
	{
		Vector3 pos2 = new Vector3(9,0,0);
		Quaternion rot2 = new Quaternion(0,0,0,0);
		player2 = Network.Instantiate(PlayerPrefab,pos2,rot2,0) as Transform;
	}
	
	void OnPlayerDisconnected()
	{
		Time.timeScale = 0.0f;
	}
	
	void OnGUI()
	{
		var myStyle = new GUIStyle();
		myStyle.normal.textColor = GUI.skin.label.normal.textColor;
		myStyle.fontSize = 50;
		
		
		 if (Network.peerType == NetworkPeerType.Disconnected)
		{
			GUI.Label(new Rect(10, 10, 200, 20), "You are Disconnected");
			
			if (GUI.Button(new Rect(10, 30, 130, 20), "Connect to a Server"))
			{
				Debug.Log("connect as client");
       			Net.Connect(Net.AS_CLIENT);				
			}
			if (GUI.Button(new Rect(10, 50, 130, 20), "Start new Server"))
			{
				Debug.Log("connect as server");
    			Net.Connect(Net.AS_SERVER);
			}
		}
		else
    	{
        	float middle = Screen.width/2;
			
			GUI.Label(new Rect(middle, 10, 300, 20), pointsClient + ":" + pointsServer, myStyle);
			
			
			GUI.Label(new Rect(10, 10, 300, 20), "You are Connected as " + Network.peerType);
			if (GUI.Button(new Rect(10, 30, 120, 20), "Disconnect"))
        	{
            	Network.Disconnect(200);
        	}
		}
	}
	
	// -1 ist links und 1 ist rechts
	public void respawn(int Richtung)
	{
		Vector3 posBall = new Vector3(0,0,0);
		Quaternion rotBall = new Quaternion(0,0,0,0);
		Ball = Network.Instantiate(BallPrefab,posBall,rotBall,0) as Transform;
		int x = 500 * Richtung;
		Ball.transform.rigidbody.AddForce(new Vector3(x,0,0));
	}

}
