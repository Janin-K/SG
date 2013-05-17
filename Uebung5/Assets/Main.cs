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
	
	private void createBranch(Transform parentBranch, float segmentRotation) {
		Transform parent;
		Transform newBranch;
		
		
		Debug.Log("seg rot param: " + segmentRotation);
		if(parentBranch == null) {
			Debug.Log("first");
			parent = Instantiate(branch) as Transform;
			float y = parent.transform.FindChild("BranchCube").renderer.bounds.size.y * 0.5F;
			parent.transform.position = Vector3.up * y;
			newBranch = parent;
		} else {
			Debug.Log("after first");
			parent = parentBranch;	
			newBranch = Instantiate(branch) as Transform;
			
			float angleZ = 45F;
			float angleY = segmentRotation;
			Debug.Log("rot " + segmentRotation);
			newBranch.transform.eulerAngles = new Vector3(0, angleY, angleZ);
						
			newBranch.transform.localScale = new Vector3(SCALE, SCALE, SCALE);
			newBranch.transform.position = branchBottom(parent, newBranch);

			newBranch.transform.parent = parent.transform;
			
			if (fuse > 2) {
				return; 
			} else {
				fuse++;
			}
		}
		
		
		
		int range = 2; //Random.Range(2, 10);
		float nextSegment = 360 / range;
		Debug.Log("loc scale: " + newBranch.FindChild("BranchCube").localScale.y);
		
		for(int i=0; i < range; i++) {
			float rot = nextSegment * i;
			Debug.Log("for rot " +  rot);
			createBranch(parent, rot);
		}	 
		
	}
	
	private Vector3 branchBottom(Transform parent, Transform newBranch) {
		Transform child = parent.FindChild("BranchCube");
		float rotation = newBranch.transform.eulerAngles.z;
		
		float hypotenuse = newBranch.FindChild("BranchCube").renderer.bounds.size.y * 0.5F;
		float deltaX = Mathf.Sin(rotation) * hypotenuse;
		float deltaY = deltaX / Mathf.Tan(rotation);
		
		float x = parent.position.x - deltaX;
		float y = child.renderer.bounds.size.y + deltaY;
		float z = parent.position.z;
		
		Vector3 vector = new Vector3(x, y, z);
		return vector;
		
	}
}
