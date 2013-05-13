using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour {

	private bool catched = false; 
	public float collected_powerups = 0F;
	public float all_powerups = 1.0F;
	public float poweruprate = 0f;
	public Vector3 offset;
	public float recycleOffset, spawnChance;
	public GameObject ball;
	public float posx;
	public float posy;
	public float test;
	
	// Use this for initialization
	void Start () {
		gameObject.active = true;
		all_powerups = 1F;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.posx < ball.rigidbody.position.x - 10) {
			gameObject.active = false;
		}
		//powerup muss deaktiviert werden, wenn Spieler zu weit weg ist -> wenn nicht eingesammelt und daran vorbei ist dieses sonst immer noch
		//aktiv und kein neues kann erzeugt werden
		
		
	}
	
	public void SpawnIfAvailable(float positionx, float positiony){
		if(gameObject.active || spawnChance <= Random.Range(0f, 50f)) {
			return;
		}
		
		gameObject.active = true;
		posx = positionx;
		posy = positiony + 3f;
		Vector3 vector = new Vector3(posx, posy, 0);
		transform.localPosition = vector;
		all_powerups++; 
		print ("erstellte powerups: " + all_powerups);
		catched = false; 
		
	}
	void OnTriggerEnter () {;
		collected_powerups++; 
		print("collected powerups: " + collected_powerups);
		catched = true; 
		//kugel.AddBoost();
		gameObject.active = false;
		test = CountPowerupRate();
		print ("poweruprate: " + test);
	}
	
	public float CountPowerupRate(){
		/*if(all_powerups >=3) {
			poweruprate = collected_powerups/all_powerups;
			return poweruprate;
		}
		float nopowerrate = 0f;
		return nopowerrate;
		*/
		poweruprate = collected_powerups/all_powerups;	
		Debug.Log("booster: " + poweruprate);
		return poweruprate;
	}
	
}
