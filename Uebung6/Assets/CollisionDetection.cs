using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider Aus)
	{
		Debug.Log ("huhu");
		Debug.Log("collider" + Aus);
		
		
		
		
		//if(Aus.rigidbody == Colrechts){
		//	Debug.Log("rechts");
			//pointsServer +=1;
			//Destroy(Ball);
			//respawn(-1);
		//}
		//if(Aus.rigidbody == Collinks){
		//	Debug.Log("links");
			//pointsClient +=1;
			//Destroy(Ball);
			//respawn(1);
		//}
	}
}
