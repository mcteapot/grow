using UnityEngine;
using System.Collections;

public class LinkCollider : MonoBehaviour {
	public Transform follow;
	private Vector3 basePos;
	
	private bool isActive = false;
	
	public bool getIsActive () {
		return isActive;
	}
	// Use this for initialization
	void Start () {
		basePos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

		moveCollider();
	}
	
	// Update is called once per frame
	void Update () {
		moveCollider();
		getActive();
	}
	
	void moveCollider () {
		//transform.position = Vector3(sun.position.x, sun.position.y, sun.position.z);
		transform.position = new Vector3(follow.position.x, basePos.y, basePos.z);
	}
	
	void getActive () {
		if(follow.gameObject.name == "Sun") {
			isActive = follow.gameObject.GetComponent<SunController>().getIsActive();
		} else if(follow.gameObject.name == "Cloud Dark Shadow") {
			isActive = follow.gameObject.GetComponent<CloudShadow>().getIsActive();
		}
		//Debug.Log(follow.gameObject.name + ": " + isActive);
	}
}
