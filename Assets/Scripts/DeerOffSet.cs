using UnityEngine;
using System.Collections;

public class DeerOffSet : MonoBehaviour {

	public float offsetY = 0.0F;
	
	public Vector3 startVect;
	
	// Use this for initialization
	void Start () {
		
		startVect = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
		//setPosition();
	}
	
	// Update is called once per frame
	void Update () {
		setPosition();
	
	}
	
	void setPosition () {
		transform.localPosition = new Vector3(startVect.x, startVect.y + offsetY, startVect.z);		
	}
}
