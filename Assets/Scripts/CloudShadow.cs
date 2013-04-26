using UnityEngine;
using System.Collections;

public class CloudShadow : MonoBehaviour {
	
	public Transform target;
	public Transform darkRay;
	public Projector darkProj;
	
	public bool isActive;
	
	private Quaternion startRotation;
	
	// Use this for initialization
	void Start () {
		lookAtCamera();
		isActive = false;
		startRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		lookAtCamera();
		setScale();
		animateShadow();
	}
	
	void lookAtCamera () {
		Vector3 relativePos = target.position - transform.position;
		//var relativePos : Vector3 = target.position - transform.position;
	    //print("ROTATION: " + Quaternion.LookRotation(relativePos).eulerAngles.y);
	    float yRotation = Quaternion.LookRotation(relativePos).eulerAngles.y;
		//var yRotation : float = Quaternion.LookRotation(relativePos).eulerAngles.y;
	
	    transform.rotation = Quaternion.Euler( new Vector3(startRotation.eulerAngles.x, yRotation - 180, startRotation.eulerAngles.z));
	
	}
	void setScale () {
		darkRay.localScale = new Vector3(darkRay.localScale.x, transform.position.y, darkRay.localScale.z);
		//var proj : Projector = GetComponent (Projector);
		//print(darkRay.localScale.y);
		//print(darkProj.farClipPlane);
	}
	
	void animateShadow () {
		float aVelocity = 0.0F;
		if(isActive) {
			darkProj.farClipPlane = Mathf.SmoothDamp(darkProj.farClipPlane, 50.0F, ref aVelocity, 0.4F);
	
		} else {
			darkProj.farClipPlane = Mathf.SmoothDamp(darkProj.farClipPlane, 0.01F, ref aVelocity, 0.2F);
	
		}
	}
	
	void setActive  (bool newActive) {
		isActive = newActive;
		if(!isActive){
			//animateShadow();
			transform.position = new Vector3(0, 0, 0);
			if(audio.isPlaying) {
				audio.volume = 0.0F;
				audio.Pause();
			}
		}
		if(newActive && !audio.isPlaying) {
			audio.Play();
			audio.volume = 1.0F;
		}
	}
	
    void OnDestroy() {
		if(audio.isPlaying) {
			audio.volume = 0.0F;
			audio.Pause();
		}
    }

}
