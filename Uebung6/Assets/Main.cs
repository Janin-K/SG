using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public Transform PlayerPrefab;
	public Transform WallPrefab;
	public Transform BallPrefab;
	
	public string connectionIP = "127.0.0.1";
	public int connectionPort = 25001;
	
	
	// Use this for initialization
	void Start () {
		CreateScene();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}
	
	
	public void CreateScene()
	{
		Transform player1 = Instantiate(PlayerPrefab) as Transform;
		player1.transform.position = new Vector3(-9,0,0);
		Transform player2 = Instantiate(PlayerPrefab) as Transform;
		player2.transform.position = new Vector3(9,0,0);
		
		Transform Wall1 = Instantiate(WallPrefab) as Transform;
		Wall1.transform.position = new Vector3(0,4,0);
		
		Transform Wall2 = Instantiate(WallPrefab) as Transform;
		Wall2.transform.position = new Vector3(0,-4,0);
		
		Transform Ball = Instantiate(BallPrefab) as Transform;
		Ball.transform.position = new Vector3(0,0,0);
		Ball.transform.rigidbody.AddForce(new Vector3(500,0,0));
	}
	
	void OnGUI()
	{

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
		else if (Network.peerType == NetworkPeerType.Client)
    	{
        	GUI.Label(new Rect(10, 10, 300, 20), "You are Connected as Client");
        	if (GUI.Button(new Rect(10, 30, 120, 20), "Disconnect"))
        	{
            	Network.Disconnect(200);
        	}
    	}
	}

}
