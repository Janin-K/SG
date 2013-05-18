using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public Transform branch;
	public const float SCALE = 0.5F; // scale of every new branch in relation to its parent
	private const int MAX_NESTING_DEPTH = 2;
	
	// Use this for initialization
	void Start () {
		CreateBranch(null, 0);
	}
	

	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void CreateBranch(Transform parentBranch, float segmentRotation) {
		Transform parent;
		Transform newBranch;
		
		if(parentBranch == null) {
			parent = Instantiate(branch) as Transform;
			float y = parent.transform.FindChild("BranchCube").renderer.bounds.size.y * 0.5F;
			parent.transform.position = Vector3.up * y;
			newBranch = parent;
		} else {
			parent = parentBranch;	
			newBranch = Instantiate(branch) as Transform;
			
			newBranch.transform.localScale = new Vector3(SCALE, SCALE, SCALE);
			newBranch.transform.position = ChildPosition(parent, newBranch);
			
			newBranch.transform.RotateAround(ParentTop(parent), Vector3.forward, 45);
			newBranch.transform.RotateAround(ParentTop(parent), Vector3.up, segmentRotation);
			newBranch.transform.parent = parent.transform;
		}
		
		if(NestingDepth(newBranch) < MAX_NESTING_DEPTH) {
			int range = 3; //Random.Range(2, 10);
			float nextSegment = 360 / range;
			
			for(int i=0; i < range; i++) {
				float rot = nextSegment * i;
				CreateBranch(parent, rot);
			}		
		}
		 
		
	}
	
	private Vector3 ParentTop(Transform parent) {
		Transform parentChild = parent.FindChild("BranchCube");
		
		float x = parent.position.x;
		float y = parentChild.renderer.bounds.size.y;
		float z = parent.position.z;
		
		Vector3 vector = new Vector3(x, y, z);
		return vector;
		
	}
	
	private Vector3 ChildPosition(Transform parent, Transform newBranch) {
		Transform parentChild = parent.FindChild("BranchCube");
		Transform newBranchChild = newBranch.FindChild("BranchCube");
		
		float x = parent.position.x;
		float y = parentChild.renderer.bounds.size.y + newBranchChild.renderer.bounds.size.y * 0.5F;
		float z = parent.position.z;
		
		Vector3 vector = new Vector3(x, y, z);
		return vector;
		
	}
	
	private int NestingDepth(Transform instance) {
		if(instance.parent == null) {
			return 1;
		} else {
			int depth = NestingDepth(instance.parent) + 1;
			return depth;	
		}
	}
}
