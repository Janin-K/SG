using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public Transform PlayerPrefab;
	public Transform WallPrefab;
	public Transform BallPrefab;
	
	
	// Use this for initialization
	void Start () {
		createScene();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	
	
	
	
	public void createScene()
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
}
