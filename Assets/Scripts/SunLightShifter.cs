using UnityEngine;
using System.Collections;

public class SunLightShifter : MonoBehaviour {
	
	public float minY = 0.01F;
	public float maxY = 10.0F;
	
	public float minIntesnity = 0.1F;
	public float maxIntensity = 0.9F;
	
	private float newIntensity = 0.9F;

	// Use this for initialization
	void Start () {
		setIntesnety();
	}
	
	// Update is called once per frame
	void Update () {
		setIntesnety();
	}
	
	void setIntesnety () {
		if(transform.position.y >= maxY) {
			newIntensity = maxIntensity;
		}else if(transform.position.y <= minY) {
			newIntensity = minIntesnity;
		}else {
			//Debug.Log("INBETWEEEN STATES");
			float totalLength = maxY - minY;
			float totalIntensity = maxIntensity - minIntesnity;
			
			float currentLength = transform.position.y - minY;
			
			float lightPrecent = currentLength / totalLength;
			float addIntensity = totalIntensity * lightPrecent;
			
			//Debug.Log("INBETWEEEN Precent: " + lightPrecent);
			
			//Debug.Log("AddIntence: " + addIntensity);
			
			
			newIntensity = minIntesnity + addIntensity;
			
			
		}
		
		//light.intensity = newIntensity;
		light.intensity = newIntensity;
	}
}
