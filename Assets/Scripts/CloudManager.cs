using UnityEngine;
using System.Collections;

public class CloudManager : MonoBehaviour {
	
	public static int cloundNumber;
	
	private Vector3 screenPos;
	private Vector3 offset;
	private float positionZ;
	private float positionY;
	
	private bool isActive;
	private bool isBounding;
	private bool isAnimate;
	private float amoutToMove;
	
	private float cloudFadingOn;
	private float cloundTintAOn;
	
	private CS_Cloud cloudScript;
	
	float moveRate;
	float deathPoint;
	
	float boundRight;
	float boundLeft;
	float correction;
	
	float cloundLifeRate;
	float cloudDeath;
	
	AnimationClip onAnimation;
	AnimationClip offAnimation;
	
	Transform cloundLight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool getIsActive () {
		return isActive;
	}
	
	public float getCloundTintAOn () {
		return cloundTintAOn;
	}
}
