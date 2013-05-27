using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour {
	
	public Transform BallPrefab;
	private Transform Ball;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider Aus)
	{
	if(Aus.name == "colliderServer")
		{
			Main.pointsServer+=1;
			respawn(-1);
		}
		
	if(Aus.name == "colliderClient")
		{
			Main.pointsClient +=1;
			respawn(1);
		}
	}
	
	public void respawn(int Richtung)
	{
		Vector3 posBall = new Vector3(0,0,0);
		this.transform.position = posBall;
		
		
		this.transform.rigidbody.velocity = Vector3.zero;
		this.transform.rigidbody.angularVelocity = Vector3.zero;
		this.transform.rigidbody.inertiaTensorRotation = Quaternion.identity;
		
		int x = 500 * Richtung;
		int y = Random.Range (-150,150);
		this.transform.rigidbody.AddForce(new Vector3(x,y,0));
	}

}
