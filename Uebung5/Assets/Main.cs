using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public Transform branch;
	public const float SCALE = 0.7F; // scale of every new branch in relation to its parent
	private const int MAX_NESTING_DEPTH = 5;
	private const int Z_ROTATION_MIN = 20;
	private const int Z_ROTATION_MAX = 60;

	
	// Use this for initialization
	void Start () {
		CreateBranch(null);
	}
	

	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void CreateBranch(Transform parentBranch, float segmentRotation = 0, int segmentCount = 0) {
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
			
			newBranch.transform.localScale = parent.lossyScale * SCALE;
			newBranch.transform.position = ChildPosition(parent, newBranch);
			
			
			float variance = 360f/segmentCount;
			float delta = variance * 0.4f;
			float yRotation = segmentRotation + Random.Range(-delta, delta);
			
			newBranch.transform.RotateAround(ParentTop(parent), Vector3.forward, Random.Range(Z_ROTATION_MIN, Z_ROTATION_MAX));
			newBranch.transform.RotateAround(ParentTop(parent), Vector3.up, yRotation);
			newBranch.transform.parent = parent.transform;
		}
		
		if(NestingDepth(newBranch) < MAX_NESTING_DEPTH) {
			int range = Random.Range(2, 10);
			float nextSegment = 360 / range;
			
			for(int i=0; i < range; i++) {
				float rotation = nextSegment * i;
				CreateBranch(newBranch, rotation, range);
			}		
		}
		 
		
	}
	
	private Vector3 ParentTop(Transform parentContainer) {
		Transform parentObject = parentContainer.FindChild("BranchCube");
		Vector3	startposition = parentContainer.transform.position;
		
		Vector3 endposition = startposition + (parentContainer.transform.up.normalized * parentObject.transform.renderer.bounds.size.y / 2);
		
		return endposition;
		
	}
	
	private Vector3 ChildPosition(Transform parentContainer, Transform newBranch) {
		Transform newBranchObject = newBranch.FindChild("BranchCube");
		
		Vector3 center = ParentTop(parentContainer);
		//Debug.Log ("center" + center);
		//Debug.Log("magnitude" + newBranchObject.transform.up.magnitude);
		Vector3 newPosition = center + newBranchObject.transform.up.normalized * newBranchObject.transform.renderer.bounds.size.y/2;
		//Debug.Log ("newPos" + newPosition);

		return newPosition;
		
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
