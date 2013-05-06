using UnityEngine;
using System.Collections;

public class BallColor
{
    private Material greenMaterial;
	private Material redMaterial;
	private Renderer renderer;
	private const int MaxLoadingTime = 5;
	public float lerp;
	public BallColor (Material green, Material red, Renderer renderer)
	{
		this.greenMaterial = green;
		this.redMaterial = red;
		this.renderer = renderer;
		renderer.material = greenMaterial;
	}
	
	public void setColor(float loadingDuration)
	{
		lerp = loadingDuration / MaxLoadingTime;	
		renderer.material.Lerp(greenMaterial, redMaterial, lerp);
	}
	
	public float getAcceleration() {
		float speedAcc = lerp * -10000;
		return speedAcc;
	}
}

