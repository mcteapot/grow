using UnityEngine;
using System.Collections;

public class SunBeam : MonoBehaviour {
	public AnimationClip onAnimation;
	public AnimationClip offAnimation;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void beamOn () {
		animation.Play(onAnimation.name);
	}

	void beamOff () {
		//transform.localScale = Vector3(1, 0, 1);
		animation.Play(offAnimation.name);
	}
}
