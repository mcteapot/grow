using UnityEngine;
using System.Collections;

/* This script is the main lightning/electricity controller.
 * The script adds the deformations to the meshes and also controls their lifetimes. */

public class LightningDeformer : MonoBehaviour
{
	// the mesh that represents glowing of the lightning
	public GameObject glow = null;
	// the multiplier for the size of both ends of the lightning (the area, in which it might start or end)
	public float targetSize = 0.1f;
	// the amplitude of the lightning
	public float lightningAmplitude = 1.0f;
	// the fuziness (roughness) of the lightning
	public float lightningFuzziness = 1.0f;
	// the agility of the lightning
	public float lightningAgility = 1.0f;
	
	// the limits of the lifetime of a single lightning existance period
	public float lifetimeLowerLimit = 0.25f;
	public float lifetimeUpperLimit = 2.5f;
	// the limits of the time, in which the lightning isn't visible
	public float invisibleTimeLowerLimit = 0.1f;
	public float invisibleTimeUpperLimit = 2.5f;
	
	// the probability, with which the lightning disappears after a single existance period
	public float probabilityOfDisappearing = 0.25f;
	
	// should the sound be stopped after disappearing
	// the sound should probably be stopped if it's an electricity discharge and it becomes invisible
	// it should probably not be stopped if it's a real lightning (the sound is heard even after it disappears)
	public bool stopSoundOnDisappear = true;
	
	private Vector3[] primaryVertices = null;
	private Vector3[] primaryGlowVertices = null;
	
	private float timer = 0;
	private float timerEnd = 0;
	private bool isEnabled = true;
	
	// the variables used to randomize and animate the lightning
	private float sinusYOffset = 0;
	private float sinusZOffset = 0;
	private float sinusSpeed = 0;
	
	private float[] randomnessY = null;
	private float[] randomnessZ = null;
	
	private float[] randomnessYCurr = null;
	private float[] randomnessZCurr = null;
	private bool[] randomnessYCurrDirection = null;
	private bool[] randomnessZCurrDirection = null;
	
	
	// Use this for initialization
	void Start ()
	{
		// not let the values be too large (because it looks bad) or lower than zero
		Mathf.Clamp01(targetSize);
		Mathf.Clamp(lightningAmplitude, 0.0f, 5.0f);
		Mathf.Clamp(lightningFuzziness, 0.0f, 5.0f);
		Mathf.Clamp(lightningAgility, 0.0f, 5.0f);
		
		// don't let the lower limit be greater than the upper
		if (lifetimeUpperLimit < 0.0f)
			lifetimeUpperLimit = 0;
		Mathf.Clamp(lifetimeLowerLimit, 0.0f, lifetimeUpperLimit);

		// don't let the lower limit be greater than the upper
		if (invisibleTimeUpperLimit < 0.0f)
			invisibleTimeUpperLimit = 0;
		Mathf.Clamp(invisibleTimeLowerLimit, 0.0f, invisibleTimeUpperLimit);
		
		// get the primary verte positions of the edited meshes
		Mesh mesh = ((MeshFilter)GetComponent(typeof(MeshFilter))).mesh;
		primaryVertices = mesh.vertices;
		
		Mesh glowMesh = ((MeshFilter)glow.GetComponent(typeof(MeshFilter))).mesh;
		primaryGlowVertices = glowMesh.vertices;
		
		// allocate the arrays for the animation related variables
		randomnessY = new float[primaryVertices.Length / 4];
		randomnessZ = new float[primaryVertices.Length / 4];
		randomnessYCurr = new float[primaryVertices.Length / 4];
		randomnessZCurr = new float[primaryVertices.Length / 4];
		randomnessYCurrDirection = new bool[primaryVertices.Length / 4];
		randomnessZCurrDirection = new bool[primaryVertices.Length / 4];
		
		// initialize the lightning
		RecalculateVertices();
		timerEnd = Random.Range(lifetimeLowerLimit, lifetimeUpperLimit);
		timer = 0;
		sinusYOffset = Random.Range(0.0f, 6.5f);
		sinusZOffset = Random.Range(0.0f, 6.5f);
		sinusSpeed = Random.Range(0.5f, 1.5f);
		
		//if (isEnabled)
		//{
			//audio.Play();
		//}
		
	}

	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		if (timer >= timerEnd)
		{
			if (isEnabled)
				isEnabled = Random.Range(0.0f, 1.0f) > probabilityOfDisappearing;
			else
				isEnabled = true;
			
			if (!isEnabled)
			{
				gameObject.renderer.enabled = false;
				glow.renderer.enabled = false;
				
				timer = timer - timerEnd;
				
				timerEnd = Random.Range(invisibleTimeLowerLimit, invisibleTimeUpperLimit);
				
				//if (stopSoundOnDisappear)
					//audio.Stop();
			}
			else
			{
				gameObject.renderer.enabled = true;
				glow.renderer.enabled = true;
				
				RecalculateVertices();
				sinusYOffset = Random.Range(0.0f, 6.5f);
				sinusZOffset = Random.Range(0.0f, 6.5f);
				sinusSpeed = Random.Range(0.5f, 1.5f);
				
				timer = timer - timerEnd;
				
				timerEnd = Random.Range(lifetimeLowerLimit, lifetimeUpperLimit);
				
				//audio.Stop();
				//audio.Play();
			}
		}
		
		
		// apply motion and animation to the lightning
		Mesh mesh = ((MeshFilter)GetComponent(typeof(MeshFilter))).mesh;
		Vector3[] vertices = mesh.vertices;
		Vector3[] normals = mesh.normals;
		Mesh glowMesh = ((MeshFilter)glow.GetComponent(typeof(MeshFilter))).mesh;
		Vector3[] glowVertices = glowMesh.vertices;
		for (int i = 0; i < vertices.Length; i++)
		{
			// animate the whole lightning curve
			vertices[i] += lightningAmplitude * sinusSpeed * Vector3.up * Mathf.Sin(sinusYOffset + Time.time + vertices[i].x * 0.1f) * 0.03f * Mathf.Cos(vertices[i].x / 15f * 1.57f);
			vertices[i] += lightningAmplitude * sinusSpeed * Vector3.forward * Mathf.Sin(sinusZOffset + Time.time + vertices[i].x * 0.1f) * 0.03f * Mathf.Cos(vertices[i].x / 15f * 1.57f);
			
			// animate the whole lightning glow curve
			glowVertices[i] += lightningAmplitude * sinusSpeed * Vector3.up * Mathf.Sin(sinusYOffset + Time.time + glowVertices[i].x * 0.1f) * 0.03f * Mathf.Cos(glowVertices[i].x / 15f * 1.57f);
			glowVertices[i] += lightningAmplitude * sinusSpeed * Vector3.forward * Mathf.Sin(sinusZOffset + Time.time + glowVertices[i].x * 0.1f) * 0.03f * Mathf.Cos(glowVertices[i].x / 15f * 1.57f);

			// make the lightning become thicker over time
			vertices[i] += 0.07f * normals[i] * Time.deltaTime / timerEnd;
			
			// the condition was added to avoid large undesirable deformations
			// if the framerate is low, the deformations aren't needed at all
			if (Time.smoothDeltaTime < 0.05f)
			{
				int index = (int)(Mathf.Clamp((vertices[i].x + 15f) / 30f * randomnessY.Length, 0, randomnessY.Length - 1));
				
				// animate the agility of the lightning
				if ((index > 3) && (index < randomnessY.Length - 4))
				{
					if (randomnessYCurrDirection[index])
						vertices[i].y += lightningAgility * Time.smoothDeltaTime * 2.0f;
					else
						vertices[i].y -= lightningAgility * Time.smoothDeltaTime * 2.0f;
					if (randomnessZCurrDirection[index])
						vertices[i].z += lightningAgility * Time.smoothDeltaTime * 2.0f;
					else
						vertices[i].z -= lightningAgility * Time.smoothDeltaTime * 2.0f;
				}
			}
		}
		
		// the condition was added to avoid large undesirable deformations
		// if the framerate is low, the deformations aren't needed at all
		if (Time.smoothDeltaTime < 0.05f)
		{
			// check the agility parameters (basically vertex distance from the primary position)
			// change the directions if needed
			for (int index = 0; index < randomnessY.Length; index++)
			{
				if (randomnessYCurrDirection[index])
				{
					randomnessYCurr[index] += Time.smoothDeltaTime * 4.5f;
					if (randomnessYCurr[index] >= randomnessY[index])
						randomnessYCurrDirection[index] = false;
				}
				else
				{
					randomnessYCurr[index] -= Time.smoothDeltaTime * 4.5f;
					if (randomnessYCurr[index] <= -randomnessY[index])
						randomnessYCurrDirection[index] = true;
				}
				
				if (randomnessZCurrDirection[index])
				{
					randomnessZCurr[index] += Time.smoothDeltaTime * 4.5f;
					if (randomnessZCurr[index] >= randomnessZ[index])
						randomnessZCurrDirection[index] = false;
				}
				else
				{
					randomnessZCurr[index] -= Time.smoothDeltaTime * 4.5f;
					if (randomnessZCurr[index] <= -randomnessZ[index])
						randomnessZCurrDirection[index] = true;
				}
			}
		}
		
		// apply the deformations to the meshes
		mesh.vertices = vertices;
		glowMesh.vertices = glowVertices;
	
	}
	
	// initialize (or re-initialize) the lightning for its new lifetime period
	void RecalculateVertices ()
	{
		// get the mesh and its vertices
		Mesh mesh = ((MeshFilter)GetComponent(typeof(MeshFilter))).mesh;
		mesh.vertices = primaryVertices;
		Vector3[] vertices = mesh.vertices;
		
		// make it thinner at first so that it could become thicker
		Vector3[] normals = mesh.normals;
		for (int i = 0; i < vertices.Length; i++)
		{
			vertices[i] -= 0.02f * normals[i];
		}
		
		// get the glow mesh and its vertices
		Mesh glowMesh = ((MeshFilter)glow.GetComponent(typeof(MeshFilter))).mesh;
		glowMesh.vertices = primaryGlowVertices;
		Vector3[] glowVertices = glowMesh.vertices;
		
		// initialize and calculate the randomness for the primary lightning deformation
		float[] randomYOffsets = new float[vertices.Length / 4];
		float[] randomZOffsets = new float[vertices.Length / 4];
		
		for (int i = 0; i < randomYOffsets.Length; i++)
		{
			randomYOffsets[i] = Random.Range(-1.0f, 1.0f);
			randomZOffsets[i] = Random.Range(-1.0f, 1.0f);
		}
		
		// set the fuzziness (roughness) of the lightning
		for (int i = 0; i < vertices.Length; i++)
		{
			vertices[i] += lightningFuzziness * 0.13f * Vector3.up * randomYOffsets[(int)(Mathf.Clamp((vertices[i].x + 15f) / 30f * randomYOffsets.Length, 0, randomYOffsets.Length - 1))];
			vertices[i] += lightningFuzziness * 0.13f * Vector3.forward * randomZOffsets[(int)(Mathf.Clamp((vertices[i].x + 15f) / 30f * randomZOffsets.Length, 0, randomZOffsets.Length - 1))];
		}
		for (int i = 0; i < glowVertices.Length; i++)
		{
			glowVertices[i] += lightningFuzziness * 0.06f * Vector3.up * randomYOffsets[(int)(Mathf.Clamp((glowVertices[i].x + 15f) / 30f * randomYOffsets.Length, 0, randomYOffsets.Length - 1))];
			glowVertices[i] += lightningFuzziness * 0.06f * Vector3.forward * randomZOffsets[(int)(Mathf.Clamp((glowVertices[i].x + 15f) / 30f * randomZOffsets.Length, 0, randomZOffsets.Length - 1))];
		}
		
		float vertexDeformationStrength = 0;
		
		float offsetY1 = Random.Range(0.0f, 6.5f);
		float offsetY2 = Random.Range(0.0f, 6.5f);
		float offsetY3 = Random.Range(0.0f, 6.5f);
		
		float offsetZ1 = Random.Range(0.0f, 6.5f);
		float offsetZ2 = Random.Range(0.0f, 6.5f);
		float offsetZ3 = Random.Range(0.0f, 6.5f);
		
		// set the curvature of the lightning and its glow mesh
		for (int i = 0; i < vertices.Length; i++)
		{
			vertexDeformationStrength = Mathf.Cos(vertices[i].x / 15f * 1.57f);
			if (lightningAmplitude != 0.0f)
				vertexDeformationStrength = Mathf.Clamp(vertexDeformationStrength, targetSize / lightningAmplitude, 1.0f);
			else
				vertexDeformationStrength = Mathf.Clamp(vertexDeformationStrength, 0, 1.0f);
			vertices[i] += lightningAmplitude * Vector3.up * Mathf.Sin(offsetY1 + vertices[i].x * 0.1f) * 2.1f * vertexDeformationStrength;
			vertices[i] += lightningAmplitude * Vector3.up * Mathf.Sin(offsetY2 + vertices[i].x * 0.4f) * 0.7f * vertexDeformationStrength;
			vertices[i] += Vector3.up * Mathf.Sin(offsetY1 + vertices[i].x * 1.5f) * 0.14f * Mathf.Sin(offsetY3 + vertices[i].x * 0.2f);
		
			vertices[i] += lightningAmplitude * Vector3.forward * Mathf.Sin(offsetZ1 + vertices[i].x * 0.1f) * 2.1f * vertexDeformationStrength;
			vertices[i] += lightningAmplitude * Vector3.forward * Mathf.Sin(offsetZ2 + vertices[i].x * 0.4f) * 0.7f * vertexDeformationStrength;
			vertices[i] += Vector3.forward * Mathf.Sin(offsetZ1 + vertices[i].x * 1.5f) * 0.54f * Mathf.Sin(offsetZ3 + vertices[i].x * 0.2f);
		
		}
		for (int i = 0; i < glowVertices.Length; i++)
		{
			vertexDeformationStrength = Mathf.Cos(glowVertices[i].x / 15f * 1.57f);
			if (lightningAmplitude != 0.0f)
				vertexDeformationStrength = Mathf.Clamp(vertexDeformationStrength, targetSize / lightningAmplitude, 1.0f);
			else
				vertexDeformationStrength = Mathf.Clamp(vertexDeformationStrength, 0, 1.0f);
			glowVertices[i] += lightningAmplitude * Vector3.up * Mathf.Sin(offsetY1 + glowVertices[i].x * 0.1f) * 2.1f * vertexDeformationStrength;
			glowVertices[i] += lightningAmplitude * Vector3.up * Mathf.Sin(offsetY2 + glowVertices[i].x * 0.4f) * 0.7f * vertexDeformationStrength;
			glowVertices[i] += Vector3.up * Mathf.Sin(offsetY1 + glowVertices[i].x * 1.5f) * 0.14f * Mathf.Sin(offsetY3 + glowVertices[i].x * 0.2f);
		
			glowVertices[i] += lightningAmplitude * Vector3.forward * Mathf.Sin(offsetZ1 + glowVertices[i].x * 0.1f) * 2.1f * vertexDeformationStrength;
			glowVertices[i] += lightningAmplitude * Vector3.forward * Mathf.Sin(offsetZ2 + glowVertices[i].x * 0.4f) * 0.7f * vertexDeformationStrength;
			glowVertices[i] += Vector3.forward * Mathf.Sin(offsetZ1 + glowVertices[i].x * 1.5f) * 0.54f * Mathf.Sin(offsetZ3 + glowVertices[i].x * 0.2f);
		
		}
		
		// apply the deformations to the meshes
		mesh.vertices = vertices;
		glowMesh.vertices = glowVertices;

		// set the agility randomness parameters
		for (int i = 0; i < randomnessY.Length; i++)
		{
			randomnessY[i] = Random.Range(0.05f, 0.25f);
			randomnessZ[i] = Random.Range(0.05f, 0.25f);
			
			randomnessYCurr[i] = 0;
			randomnessZCurr[i] = 0;
			
			randomnessYCurrDirection[i] = Random.Range(0, 2) == 0;
			randomnessZCurrDirection[i] = Random.Range(0, 2) == 0;
		}
		
	}
	
	public float getCurrentLifetime ()
	{
		if (gameObject.renderer.enabled)
			return timer;
		return 0;
	}
	
	public float getCurrentLifetimeEnd ()
	{
		return timerEnd;
	}
	
	public void setLightningAmplitude (float lightningAmplitude)
	{
		this.lightningAmplitude = lightningAmplitude;
		Mathf.Clamp(lightningAmplitude, 0.0f, 5.0f);
	}
	
	public void setLightningFuziness (float lightningFuziness)
	{
		this.lightningFuzziness = lightningFuziness;
		Mathf.Clamp(lightningFuziness, 0.0f, 5.0f);
	}
	
	public void setLightningAgility (float lightningAgility)
	{
		this.lightningAgility = lightningAgility;
		Mathf.Clamp(lightningAgility, 0.0f, 5.0f);
	}

}
