using UnityEngine;
using System;

public class HealthBar : MonoBehaviour {
	private Texture2D healthWaterTexture;
	private Texture2D healthLightTexture;
	private Texture2D barTexture;

	//private GUIStyle healthBarGUIStyle;
	private Vector2 rect; //dimensions for the bar
	
	private float waterPercentage = 0.0F;
	private float lightPercentage = 0.0F;
	
	public float barHeightRatio;
	public float barWidthRatio;
	public float barMultiplyer = 1.0F;

	public Color healthWaterColor;
	public Color healthLightColor;
	public Color baseBarColor;

	public Vector2 offset; // offset values for adjusting the bar position 
	
	private Vector3 screenPos;
	
	public void setWaterPrecentage (float newPrecent) {
		if(newPrecent >= 0 && newPrecent <= 1) {
			waterPercentage = newPrecent;	
		}
	}
	
	public void setLightPrecentage (float newPrecent) {
		if(newPrecent >= 0 && newPrecent <= 1) {
			lightPercentage = newPrecent;	
		}
	}
	
	void Awake () {
		//set the dimensions for the bar relative to the screen height
		rect.x = (float)Screen.height * barWidthRatio * barMultiplyer;
		rect.y = (float)Screen.height * barHeightRatio * barMultiplyer;
		
		//rect.x =  barWidthRatio;
		//rect.y =  barHeightRatio;
		
		//Debug.Log("X: " + rect.x + " Y: " + rect.y);
		//Debug.Log("ScreenX: " + Screen.width + " ScreenY: " + Screen.height);
		//healthBarGUIStyle = new GUIStyle();
		
		healthWaterTexture = new Texture2D(1, 1);
		healthWaterTexture.SetPixel(0,0, healthWaterColor);
		healthWaterTexture.wrapMode = TextureWrapMode.Clamp;
		healthWaterTexture.Apply();

		healthLightTexture = new Texture2D(1, 1);
		healthLightTexture.SetPixel(0,0, healthLightColor);
		healthLightTexture.wrapMode = TextureWrapMode.Clamp;
		healthLightTexture.Apply();

		barTexture = new Texture2D(1, 1);
		barTexture.SetPixel(0,0, baseBarColor);
		barTexture.wrapMode = TextureWrapMode.Clamp;
		barTexture.Apply();
		
		//setLightPrecentage(0.8F);
		
	}
	
	void OnGUI () {

		//percentage = (float)context.health / (float)context.maxHealth;
		//Rect healthRectOver = new Rect(screenPos.x + offset.x, offset.y + Screen.height - screenPos.y, rect.x, rect.y);
		
		screenPos = Camera.allCameras[0].WorldToScreenPoint(transform.position);
		float xPos = screenPos.x + offset.x;
		float yPos = offset.y + Screen.height - screenPos.y;
		Rect healthWaterRectUnder = new Rect(xPos, yPos, rect.x, rect.y);
		GUI.DrawTexture(healthWaterRectUnder, barTexture, ScaleMode.StretchToFill, true, 10.0F);
		Rect healthWaterRectOver = new Rect(xPos, yPos, rect.x * waterPercentage, rect.y);
		GUI.DrawTexture(healthWaterRectOver, healthWaterTexture, ScaleMode.StretchToFill, true, 10.0F);

		Rect healthLightRectUnder = new Rect(xPos, yPos + rect.y, rect.x, rect.y);
		GUI.DrawTexture(healthLightRectUnder, barTexture, ScaleMode.StretchToFill, true, 10.0F);
		Rect healthLightRectOver = new Rect(xPos, yPos + rect.y, rect.x * lightPercentage, rect.y);
		GUI.DrawTexture(healthLightRectOver, healthLightTexture, ScaleMode.StretchToFill, true, 10.0F);

	}
	
}