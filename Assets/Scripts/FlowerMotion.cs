using UnityEngine;
using System.Collections;

public class FlowerMotion : MonoBehaviour {
	
	public float speed = 0.2F;
	public float waveOffset = 0.0F;
	
	public float yRangeMax = 1.0F;
	public float yRangeMin = 1.0F;
	
	public float xRangeMax = 1.0F;
	public float xRangeMin = 1.0F;
	
	private float yStartRot = 0.0F;
	private float yRotMax;
	private float yRotMin;
	
	private float xStartRot = 0.0F;
	private float xRotMax;
	private float xRotMin;

	// Use this for initialization
	void Start () {
		waveOffset = (int)Random.Range(0.0F, 3.0F);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate () {
	
		yRotMax = yStartRot + yRangeMax;
		yRotMin = yStartRot - yRangeMin;
		
		xRotMax = xStartRot + xRangeMax;
		xRotMin = xStartRot - xRangeMin;
		
		
		double weightY = Mathf.Cos((Time.time + waveOffset) * speed * 2 * Mathf.PI) + 0.5 + 0.5;
		double weightX = Mathf.Sin((Time.time + waveOffset) * speed * 2 * Mathf.PI) + 0.5 + 0.5;

		float yRot = (float)(yRotMax * weightY + yRotMin * (1 - weightY));
		float xRot = (float)(xRotMax * weightX + xRotMin * (1 - weightX));
		
		transform.localRotation = Quaternion.Euler(xRot, transform.localRotation.y, yRot);
	}
}
