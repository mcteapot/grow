using UnityEngine;
using System.Collections;

public class SunGlow : MonoBehaviour {
	
	public Transform sun;
	
	public Color maxColor;
	public Color minColor;
	
	public float colorRate = 0.5F;
	
	private bool sunActive = false;

	// Use this for initialization
	void Start () {
		getSunActive();
		changeColor();
	}
	
	// Update is called once per frame
	void Update () {
		getSunActive();
		changeColor();
	}
	
	void getSunActive () {
		sunActive = sun.GetComponent<SunController>().getIsActive();
	}
	
	void changeColor () {
		Color sunColor = renderer.material.color;
		Debug.Log ("SUN GLOW: " + sunColor);
		if(sunActive) {
			sunColor = Color.Lerp(sunColor, maxColor, colorRate * Time.time);
			Debug.Log ("COLOR CHANGE");
		} else {
			sunColor = Color.Lerp(sunColor, minColor, colorRate * Time.time);
			Debug.Log ("COLOR CHANGE");	
		}
		renderer.material.color = sunColor;
	}
}
