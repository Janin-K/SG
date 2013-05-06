using UnityEngine;
using System.Collections;


public class Scoreboard
{
	private Rect guiLayoutRect;
	private float gameLength = 50F;
	public float startTime;
	private int fallenPins;
	private int roundPoints = 0;
	
	public Scoreboard ()
	{
		startTime = Time.time;
		
		float areaWidth = 150;
		float areaHeight = 100;
		guiLayoutRect = new Rect(Screen.width-areaWidth,0,areaWidth,areaHeight);
		
	}
	

	public void Update() 
	{
		int sum = fallenPins + roundPoints;
		if(TimeLeft() > 0)
		{
			GUILayout.BeginArea(guiLayoutRect);
			GUILayout.Label("Your game status");
			GUILayout.Label("Your points " + sum);
			GUILayout.Label("Time left " + TimeLeft().ToString("F2") + " sec");
			GUILayout.EndArea();
		}
		else 
		{
			GUILayout.BeginArea(guiLayoutRect);
			GUILayout.Label("Game Over");
			GUILayout.Label("Your scored " + sum + " points");
			GUILayout.EndArea();
			Time.timeScale = 0.0F;
			
		}
	}
	
	public void setFallenPins(int pinCount) {
		fallenPins = pinCount;
	}
	
	public void StoreRoundPoints(int pinCount) {
		roundPoints += pinCount;
	}
	
	private float TimeLeft() {
		float timeLeft = gameLength - (Time.time - startTime);
		return timeLeft;
	}
}
