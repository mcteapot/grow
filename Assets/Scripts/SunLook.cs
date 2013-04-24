using UnityEngine;
using System.Collections;

public class SunLook : MonoBehaviour {
	public Transform sun;
	public Transform target;
	
	private Quaternion startRotation;
	
	// Use this for initialization
	void Start () {
		startRotation = transform.rotation;
		moveBeam();
		lookAtCamera();
	}
	
	// Update is called once per frame
	void Update () {
		moveBeam();
		lookAtCamera();	
	}
	
	void lookAtCamera () {
	    //var relativePos : Vector3 = target.position - transform.position;
	    Vector3 relativePos = target.position - transform.position;
		//print("ROTATION: " + Quaternion.LookRotation(relativePos).eulerAngles.y);
	    float yRotation = Quaternion.LookRotation(relativePos).eulerAngles.y;
		//var yRotation : float = Quaternion.LookRotation(relativePos).eulerAngles.y;
	
	    transform.rotation = Quaternion.Euler(new Vector3(startRotation.eulerAngles.x, yRotation - 180, startRotation.eulerAngles.z));
	
	}
	
	void moveBeam () {
		transform.position = new Vector3(sun.position.x, sun.position.y, sun.position.z);
	}

}
