using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public Transform branch;
	public const float SCALE = 0.5F; // scale of every new branch in relation to its parent
	public int fuse = 0;
	
	// Use this for initialization
	void Start () {
		createBranch(null, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void createBranch(Transform parentBranch, float rotation) {
		Transform parent;
		Transform newBranch;
		
		if (fuse > 1) {
			return; 
		} else {
			fuse++;
		}
		
		if(parentBranch == null) {
			parent = Instantiate(branch) as Transform;
			//parent.transform.position = new Vector3(0, 0, 0);	
			newBranch = parent;
		} else {
			parent = parentBranch;	
			newBranch = Instantiate(branch) as Transform;
			
			float angleZ = 40F;
			float angleY = rotation;
			newBranch.transform.eulerAngles = new Vector3(0, angleY, angleZ);
			
			
			newBranch.transform.localScale = new Vector3(SCALE, SCALE, SCALE);
			newBranch.transform.position = branchBottom(parent, newBranch);
			
			newBranch.transform.parent = parent.transform;
		}
		
		
		
		int range = 2; //Random.Range(2, 10);
		float nextSegment = 360 / range;
		if (newBranch.localScale.y >= 5) {
			Debug.Log("loc scale: " + newBranch.localScale.y);
			for(int i=0; i < range; i++) {
				createBranch(parent, nextSegment * i);
			}	
		}
		 
		
	}
	
	private Vector3 branchBottom(Transform parent, Transform newBranch) {
		float x = parent.position.x;
		float y = parent.renderer.bounds.size.y + newBranch.renderer.bounds.size.y * 0.5F;
		float z = parent.position.z;
		
		Vector3 vector = new Vector3(x, y, z);
		return vector;
	}
}
