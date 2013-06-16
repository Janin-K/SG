using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Transform player;
	public PhysicMaterial bounce;
	public Transform agressor1;
	public Transform agressor2;
	public Transform agressor3;
	
	private Transform ag1;

	// Use this for initialization
	void Start () {
		ag1 = Instantiate(agressor1) as Transform;
		
		
		ag1.transform.rigidbody.AddForce(new Vector3(Random.Range(-100,100),Random.Range(-100,100),0));
	}
	
	// Update is called once per frame
	void Update () {
		
		float deltaX = Input.GetAxis("Mouse X");
		float deltaY = Input.GetAxis("Mouse Y");

	    player.transform.position += new Vector3(deltaX,deltaY,0);
	 
		
	}
}
