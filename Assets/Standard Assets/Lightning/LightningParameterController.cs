using UnityEngine;
using System.Collections;

/* This script is used to change the lightning parameters with on-screen controls.
 * Just remove this script from the prefab if you don't need the on-screen controls. */

public class LightningParameterController : MonoBehaviour
{
	// the prefab that is used to instantiate new lightnings if needed
	public GameObject lightningPrefab;
	
	// the variables that are used to control paremeters
	private int lightningCount = 3;
	private int lightningCountPrev = 3;
	private float lightningAmplitude = 1.0f;
	private float lightningFuzziness = 1.4f;
	private float lightningAgility = 1.0f;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		// get the lightning deformer scripts and set the parameters for each of them
		LightningDeformer[] lightningDeformers = gameObject.GetComponentsInChildren<LightningDeformer>();
		
		for (int i = 0; i < lightningDeformers.Length; i++)
		{
			lightningDeformers[i].setLightningAmplitude (lightningAmplitude);
			lightningDeformers[i].setLightningFuziness (lightningFuzziness);
			lightningDeformers[i].setLightningAgility (lightningAgility);
		}
	}
	
	// this is only used to have the on-screen controls and set/get the new values
	void OnGUI ()
	{
		Color guiColor = GUI.color;
		GUI.color = Color.black;
		GUI.Label( new Rect (Screen.width - 162, 10 - 6, 100, 25), "Density" );
		GUI.Label( new Rect (Screen.width - 175, 30 - 6, 100, 25), "Amplitude" );
		GUI.Label( new Rect (Screen.width - 177, 50 - 6, 100, 25), "Fuzziness" );
		GUI.Label( new Rect (Screen.width - 154, 70 - 6, 100, 25), "Agility" );
		GUI.color = guiColor;
		
		lightningCountPrev = lightningCount;
			
		lightningCount = Mathf.RoundToInt(GUI.HorizontalSlider( new Rect (Screen.width - 110, 10, 100, 15), lightningCount, 1, 7 ));
		lightningAmplitude = GUI.HorizontalSlider( new Rect (Screen.width - 110, 30, 100, 15), lightningAmplitude, 0.0f, 4.0f );
		lightningFuzziness = GUI.HorizontalSlider( new Rect (Screen.width - 110, 50, 100, 15), lightningFuzziness, 0.0f, 5.0f );
		lightningAgility = GUI.HorizontalSlider( new Rect (Screen.width - 110, 70, 100, 15), lightningAgility, 0.0f, 5.0f );
	
		if (GUI.Button( new Rect(Screen.width - 110, 95, 100, 20), "Preset 1" ))
		{
			lightningCount = 3;
			lightningAmplitude = 1.0f;
			lightningFuzziness = 1.4f;
			lightningAgility = 1.0f;
		}
		
		if (GUI.Button( new Rect(Screen.width - 110, 120, 100, 20), "Preset 2" ))
		{
			lightningCount = 4;
			lightningAmplitude = 0.0f;
			lightningFuzziness = 1.7f;
			lightningAgility = 1.7f;
		}
		
		if (lightningCountPrev != lightningCount)
		{
			if (lightningCount > lightningCountPrev)
			{
				for (int i = 0; i < lightningCount - lightningCountPrev; i++)
				{
					Transform lightning = transform.FindChild("Lightning");
					GameObject lightningNew = (GameObject)GameObject.Instantiate(lightningPrefab, lightning.transform.position, lightning.transform.rotation);
					lightningNew.transform.parent = transform;
					lightningNew.name = "Lightning";
				}
			}
			else
			{
				for (int i = 0; i < lightningCountPrev - lightningCount; i++)
				{
					Transform lightning = transform.FindChild("Lightning");
					if (lightning != null)
					{
						DestroyImmediate(lightning.gameObject);
					}
				}
			}
		}
	}
}
