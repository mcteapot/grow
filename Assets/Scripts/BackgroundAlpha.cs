using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Color color = transform.gameObject.renderer.material.color;
		color.a = 0.4F;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
