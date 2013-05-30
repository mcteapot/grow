using UnityEngine;
using System.Collections;

public class FadeIntoSceen : MonoBehaviour {
	
	public Texture fadeTexture;
	public Color startColor = new Color(0, 0, 0, 1);
	public Color endColor = new Color(0, 0, 0, 0);
	
	public float duration = 3.0F;
	
	private Color currentColor;

	// Use this for initialization
	void Start () {
	
		currentColor = startColor;
		Destroy(gameObject, duration + 1.0F);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		GUI.depth = -10;
		
		GUI.color = currentColor;
		
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
		
	}
	
	void FixedUpdate () {
		
		currentColor = Color.Lerp(startColor, endColor, Time.time/duration);
	}
}
