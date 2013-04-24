using UnityEngine;
using System.Collections;

public class LinkLightSun : MonoBehaviour {
	public Transform sun;
	private Vector3 basePos;
	
	// Use this for initialization
	void Start () {
		basePos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

		moveLight();
	}
	
	// Update is called once per frame
	void Update () {
		moveLight();
	}
	
	void moveLight () {
		if(transform.gameObject.name == "Light Ground Sun") {
			transform.position = new Vector3(sun.position.x, basePos.y, basePos.z);
		} else if (transform.gameObject.name == "Light Directional Sun") {
			transform.position = new Vector3(sun.position.x, sun.position.y, basePos.z);
		}
	}
}
