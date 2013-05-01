using UnityEngine;
using System.Collections;

/* This script is used to control the light intensity, depending on the density of the electricity. */

public class LightningLightController : MonoBehaviour
{
	//public GameObject[] lightnings;
	
	private float lightIntensityPrimary = 0;
	private float lightIntensityRandomnessLimit = 0;
	private float lightIntensityRandomnessCurr = 0;
	private int lightIntensityRandomnessChangeDir = 1;
	
	// Use this for initialization
	void Start ()
	{
		lightIntensityPrimary = light.intensity;
		
		lightIntensityRandomnessCurr = lightIntensityPrimary;
		lightIntensityRandomnessLimit = lightIntensityRandomnessChangeDir * Random.Range(0.1f, 0.5f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		// get the lightning deformer scripts to get the lifetimes of the lightnings
		LightningDeformer[] lightningDeformers = gameObject.GetComponentsInChildren<LightningDeformer>();
	
		float lightIntensityCurr = 0;
		int activeLights = 0;
		// set the light intensity, depending on the count of the electricity discharges and also on their lifetimes
		for (int i = 0; i < lightningDeformers.Length; i++)
		{
			lightIntensityCurr += 0.75f + 1.5f * lightningDeformers[i].getCurrentLifetime() / lightningDeformers[i].getCurrentLifetimeEnd();
			if (lightningDeformers[i].getCurrentLifetime() != 0)
				activeLights++;
		}
		
		// if any of the lightnings are enabled, then randomize the intensity to add the feel of dynamics
		if (activeLights > 0)
		{
			lightIntensityRandomnessCurr += 20.0f * lightIntensityRandomnessChangeDir * Time.deltaTime;
			if ( ((lightIntensityRandomnessChangeDir > 0) && (lightIntensityRandomnessCurr >= lightIntensityRandomnessLimit)) || 
				((lightIntensityRandomnessChangeDir < 0) && (lightIntensityRandomnessCurr <= lightIntensityRandomnessLimit)) )
			{
				lightIntensityRandomnessChangeDir = -lightIntensityRandomnessChangeDir;
				lightIntensityRandomnessLimit = lightIntensityRandomnessChangeDir * Random.Range(0.1f, 0.5f);
			}
			
			lightIntensityCurr += lightIntensityRandomnessCurr;
		}
		
		// set the light intensity to a real light
		light.intensity = lightIntensityCurr;
	}
}
