using UnityEngine;
using System.Collections;

public class GameControl {
	private System.Collections.Generic.Dictionary<int, Vector3> initialPositions;
	
	public GameControl() {
		saveInitialPositions();
	}
	
	private void saveInitialPositions() {
		initialPositions = new System.Collections.Generic.Dictionary<int, Vector3>();
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("respawn")) {
			initialPositions.Add(obj.GetInstanceID(), obj.rigidbody.position);
		}
	}
	
	public bool CheckForReset(MonoBehaviour ball){
		if(ball.rigidbody.position.y < 0){
			return true;
		}
		return false;
	}
	
	public void Reset(Object ball) {
		Quaternion nullRotation = new Quaternion(0, 0, 0, 1);
		Vector3 nullVector = new Vector3(0, 0, 0);
		
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("respawn")) {
			obj.rigidbody.position = initialPositions[obj.GetInstanceID()];
			obj.rigidbody.rotation = nullRotation;
			obj.rigidbody.velocity = nullVector;
			obj.rigidbody.angularVelocity = nullVector;
		}
	}
}
