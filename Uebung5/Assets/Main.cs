using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public Transform branch;
	public const float SCALE = 0.7F; // scale of every new branch in relation to its parent
	private const int MAX_NESTING_DEPTH = 4;
	private const int Z_ROTATION_MIN = 20;
	private const int Z_ROTATION_MAX = 60;

	
	// Use this for initialization
	void Start () {
		System.Collections.Generic.List<Vector2> spawnPoints = UniformPoissonDiskSampler.SampleCircle(new Vector2(0, 0), 40F, 20F);
		
		foreach( Vector2 element in spawnPoints) {
			Vector2? initPos = element;
			CreateBranch(trunkPosition: initPos);	
		}
	}
	
	
	private void CreateBranch(Transform parentBranch = null, float segmentRotation = 0, int segmentCount = 0, Vector2? trunkPosition = null) {
		Transform parent;
		Transform newBranch;
		
		if(trunkPosition != null) {
			parent = Instantiate(branch) as Transform;
			float y = parent.transform.FindChild("BranchCube").renderer.bounds.size.y * 0.5F;
			parent.transform.position = new Vector3(trunkPosition.Value.x, y, trunkPosition.Value.y);
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
		float correction = 0.01F * parentContainer.transform.eulerAngles.z;
		Vector3 endposition = startposition + (parentContainer.transform.up.normalized * (parentObject.renderer.bounds.size.y / 2  + correction));
		
		return endposition;
		
	}
	
	private Vector3 ChildPosition(Transform parentContainer, Transform newBranch) {
		Transform newBranchObject = newBranch.FindChild("BranchCube");
		
		Vector3 center = ParentTop(parentContainer);
		float correction = 0.01F * newBranch.transform.eulerAngles.z;
		Vector3 newPosition = center + newBranchObject.transform.up.normalized * (newBranchObject.renderer.bounds.size.y / 2 + correction);
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
