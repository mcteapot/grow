using UnityEngine;
using System.Collections;


public class LightningFromSkyEnabler : MonoBehaviour {
	
	public bool isLightningEnabled = true;

	// Use this for initialization
	void Start () {
		//isLightningEnabledPrev = isLightningEnabled;
		foreach (Transform child in transform) {
			child.gameObject.SetActiveRecursively(isLightningEnabled);
		}
	}
	
	// Update is called once per frame
	void Update () {
		castLightning();
	}
	
	void setIsLightningEnabled (bool light) {
		isLightningEnabled = light;
	}
	
	void castLightning () {
		//if (isLightningEnabled) {
			foreach (Transform child in transform) {
				child.gameObject.SetActiveRecursively(isLightningEnabled);
			}
		
		//}
	}
	

}
