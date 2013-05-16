using UnityEngine;
using System.Collections;

public class DeerController : MonoBehaviour {
	
	public Transform flowerLink;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(flowerLink.position);
	}
}
