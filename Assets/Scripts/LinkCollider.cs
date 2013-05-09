using UnityEngine;
using System.Collections;

public class LinkCollider : MonoBehaviour {
	public Transform follow;
	private Vector3 basePos;
	
	public float minLocation = 6.5F;
	public float maxLocation = 22.5F;
	
	public float centerLocation = 14.5F;
	
	public float offset = 2.0F;
	
	
	
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
		//transform.position = new Vector3(follow.position.x, basePos.y, basePos.z);
		transform.position = offsetPosition();
	}
	
	void getActive () {
		if(follow.gameObject.name == "Sun") {
			isActive = follow.gameObject.GetComponent<SunController>().getIsActive();
		} else if(follow.gameObject.name == "Cloud Dark Shadow") {
			isActive = follow.gameObject.GetComponent<CloudShadow>().getIsActive();
		}
		//Debug.Log(follow.gameObject.name + ": " + isActive);
	}
	
	Vector3 offsetPosition () {
		Vector3 tmpPosition = new Vector3(0, 0, 0);
		
		float leftDistanceLenght = centerLocation - minLocation;
		float rightDistanceLenght = maxLocation - centerLocation;
		
		//Debug.Log("X pos: " + transform.position.x);
		
		if(transform.position.x  == centerLocation) {
			// center location
			//Debug.Log("CenterLocation");
			tmpPosition = new Vector3(follow.position.x , basePos.y, basePos.z);
			
		}else if(transform.position.x < minLocation) {
			// under minlocation
			//Debug.Log("under CenterLocation Min");
			tmpPosition = new Vector3(follow.position.x + offset, basePos.y, basePos.z);
			
		}else if(transform.position.x > maxLocation) {
			// above maxlocation
			tmpPosition = new Vector3(follow.position.x - offset, basePos.y, basePos.z);
			//Debug.Log("bove CenterLocation Max");
			
		}else if(transform.position.x < centerLocation && transform.position.x >= minLocation) {
			// under centerlocation
			float leftDistance = centerLocation - transform.position.x;
			//Debug.Log("under CenterLocation: " + leftDistance);
			float leftPrecent = leftDistance / leftDistanceLenght;
			//Debug.Log("leftPrecent: " + leftPrecent);
			float leftOffset = offset * leftPrecent;
			
			tmpPosition = new Vector3(follow.position.x + leftOffset, basePos.y, basePos.z);
			
		}else if(transform.position.x > centerLocation && transform.position.x <= maxLocation) {
			// above centerlocation
			float rightDistance = transform.position.x - centerLocation;
			//Debug.Log("above Centerlocation: " + rightDistance);
			float rightPrecent = rightDistance / rightDistanceLenght;
			//Debug.Log("rightPrecent: " + rightPrecent);
			float rightOffset = offset * rightPrecent;
			
			tmpPosition = new Vector3(follow.position.x - rightOffset, basePos.y, basePos.z);
			
		}
		
		return tmpPosition;
	}
}
