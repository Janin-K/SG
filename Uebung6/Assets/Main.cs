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
	public Transform Ball;
	private Transform Colrechts;
	private Transform Collinks;
	public static int pointsClient;
	public static int	pointsServer;
	public static string hostIp = "127.0.0.1";
	
	public const string COLLIDER_SERVER = "colliderServer";
	public const string COLLIDER_CLIENT = "colliderClient";
	
	public int lastValueServer;
	public int lastValueClient;
	
	// Use this for initialization
	void Start () {
		pointsClient = 0;
		pointsServer = 0;
		
		lastValueClient = 0;
		lastValueServer = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (Network.peerType == NetworkPeerType.Client)
		{
			if(Input.GetKey("up"))
			{
				player2.transform.position += Vector3.up * 0.2f;
			}
			if(Input.GetKey("down"))
			{
				player2.transform.position += Vector3.down * 0.2f;	
			}
		
		}
		else if (Network.peerType == NetworkPeerType.Server)
		{
			if(Input.GetKey("up"))
			{
				player1.transform.position += Vector3.up * 0.2f;
			}
			if(Input.GetKey("down"))
			{
				player1.transform.position += Vector3.down * 0.2f;	
			}
			
		}
		
		if(lastValueClient < pointsClient){
			lastValueClient = pointsClient;
			networkView.RPC("SetPointsClient",RPCMode.All, pointsClient);
		}
					
		if(lastValueServer < pointsServer){
			lastValueServer = pointsServer;
			networkView.RPC("SetPointsServer",RPCMode.All, pointsServer);
		}
				
		
		
	}	
		
	[RPC]
	void SetPointsClient(int pointsClientfromServer){
			pointsClient = pointsClientfromServer;
	
	}
	[RPC]
	void SetPointsServer(int pointsServerfromServer){
			pointsServer = pointsServerfromServer;
	
	}
	/*
	[RPC]
	void DestroyBallOfClient(GameObject BallToDestroyOfClient)
	{
		Destroy (BallToDestroyOfClient);
	}
	*/
	
	
	void OnPlayerConnected()
	{
		Vector3 pos1 = new Vector3(-7.5f,0,0);
		Quaternion rot1 = new Quaternion(0,0,0,0);
		player1 = Network.Instantiate(PlayerPrefab,pos1,rot1,0) as Transform;
		
		Vector3 posBall = new Vector3(0,0,0);
		Quaternion rotBall = new Quaternion(0,0,0,0);
		int yForce = Random.Range (-150,150);
		Ball = Network.Instantiate(BallPrefab,posBall,rotBall,0) as Transform;
		Ball.transform.rigidbody.AddForce(new Vector3(500,yForce,0));
		//Ball.tag = "Ball";
		
		Wall1 = Network.Instantiate(WallPrefab,new Vector3(0,4,0),new Quaternion(0,0,0,0),0) as Transform;
		
		Wall2 = Network.Instantiate(WallPrefab,new Vector3(0,-4,0),new Quaternion(0,0,0,0),0) as Transform;
		
		Colrechts = Network.Instantiate(ColliderPrefab,new Vector3(8f,0,0),new Quaternion(0,0,0,0),0) as Transform;
		Colrechts.name = COLLIDER_SERVER;
		
		Collinks = Network.Instantiate(ColliderPrefab,new Vector3(-8f,0,0),new Quaternion(0,0,0,0),0) as Transform;
		Collinks.name = COLLIDER_CLIENT;
	}
	
	void OnConnectedToServer()
	{
		Vector3 pos2 = new Vector3(7.5f,0,0);
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
			
			hostIp = GUI.TextField(new Rect(10,50,130,20),hostIp);
			
			if (GUI.Button(new Rect(10, 30, 130, 20), "Connect to a Server"))
			{
				Net.Connect(Net.AS_CLIENT,hostIp);				
			}
			if (GUI.Button(new Rect(10, 70, 130, 20), "Start new Server"))
			{
    			Net.Connect(Net.AS_SERVER,hostIp);
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
	
}
