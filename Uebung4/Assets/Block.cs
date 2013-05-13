using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	
	public GameObject block;
	private float endPosition = 0F;
	private float prevY;
	private float gap = 5F;
	public GameObject ball;
	GameObject cube = null;
	Vector3 position;
	public string tag;
	public Texture2D tex;
	
	
	public Booster booster; 
	
//	public Powerup powerup; 
	
	// Use this for initialization
	void Start () {
		drawBlock(new Vector3(-4, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
		createBlocks();
	}
	
	public void createBlocks()
	{
		//Debug.Log("block: " + Kugel.poweruprate);	
		if(endPosition - ball.rigidbody.position.x < 20) {
			Debug.Log("kugel pwr " + Kugel.poweruprate);
			float newX = gap * Kugel.poweruprate + endPosition;
			float newY = (1F - Kugel.poweruprate) * 3F;
			
			if(prevY > 0) {
				newY -= prevY;	
			} else {
				newY += prevY;			
			}
			
			position = new Vector3(newX, newY, 0);
			drawBlock(position);
			
		}
		
		foreach(GameObject block in GameObject.FindGameObjectsWithTag("block"))
		{
			if(block.rigidbody.position.x < ball.rigidbody.position.x - 30)
			{
				Destroy(block);
			}
		}
	}
	
	private void drawBlock(Vector3 position) {
		int x_scale = Random.Range(3, 9);
		cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.localScale = new Vector3(x_scale,1,1);
		cube.transform.position = position + new Vector3(cube.renderer.bounds.size.x/2,0,0);
		cube.AddComponent("Rigidbody");
		cube.rigidbody.isKinematic = true;
		cube.rigidbody.useGravity = true;
		cube.tag = "block";
		cube.renderer.material.mainTexture = tex;
		
		
		//endPosition = cube.rigidbody.position.x + x_scale/2;
		endPosition = cube.rigidbody.position.x + cube.renderer.bounds.size.x/2;
		
		//Debug.Log (cube.rigidbody.position.x);
		//Debug.Log(x_scale);
		prevY = cube.rigidbody.position.y;
		//Debug.Log (endPosition);
		booster.SpawnIfAvailable(cube.rigidbody.position.x,prevY);
	}
}
