using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public Transform branch;
	public const float SCALE = 0.5F; // scale of every new branch in relation to its parent
	private const int MAX_NESTING_DEPTH = 2;
	
	// Use this for initialization
	void Start () {
		CreateBranch(null, 0);
		
		/*	
		Transform parent = Instantiate(branch) as Transform;
		float y = parent.transform.FindChild("BranchCube").renderer.bounds.size.y * 0.5F;
		parent.transform.position = Vector3.up * y;
		
		Transform firstChild = Instantiate(branch) as Transform;
		
		float placementY = parent.transform.FindChild("BranchCube").renderer.bounds.size.y + firstChild.transform.FindChild("BranchCube").renderer.bounds.size.y * 0.5F;
		Vector3 top = new Vector3(parent.position.x, placementY, parent.position.z);
		
		firstChild.transform.position = top;
		firstChild.transform.parent = parent.transform;
		
		float topY = parent.transform.FindChild("BranchCube").renderer.bounds.size.y;
		Vector3 topVector = parent.transform.position;
		topVector.y = topY;
		
		firstChild.transform.RotateAround(topVector, new Vector3(1, 0, 1), 45);
		//firstChild.transform.RotateAround(topVector, Vector3.up, 45);
		
		Transform secondChild = Instantiate(branch) as Transform;
		secondChild.transform.parent = firstChild.transform;
		
		Debug.Log("parentcount " + NestingDepth(parent));
		 */
		
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
			
			float angleZ = 45F;
			float angleY = segmentRotation;
			
			//newBranch.transform.eulerAngles = new Vector3(0, angleY, angleZ);
			newBranch.transform.localScale = new Vector3(SCALE, SCALE, SCALE);
			newBranch.transform.position = ChildPosition(parent, newBranch);
			newBranch.transform.RotateAround(ParentTop(parent), Vector3.up, segmentRotation);
			newBranch.transform.RotateAround(ParentTop(parent), Vector3.forward, 45);
			newBranch.transform.parent = parent.transform;
		}
		
		if(NestingDepth(newBranch) < MAX_NESTING_DEPTH) {
			int range = 2; //Random.Range(2, 10);
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
