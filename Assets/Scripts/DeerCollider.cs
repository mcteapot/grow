using UnityEngine;
using System.Collections;

public class DeerCollider : MonoBehaviour {
	
	public bool flowerCollide = false;
	
	public bool flowerEnter = false;
	
	public bool deerScared = false;
	
	public bool deerJump = false;
	public bool deerJumped = false;
	
	private GameObject cloundShadowObject;
	
	private CloudShadow cloundShadow;
	
	// Use this for initialization
	void Start () {
		
		cloundShadowObject = GameObject.Find("Cloud Dark Shadow");
		
		if(cloundShadowObject) {
			cloundShadow = cloundShadowObject.GetComponent<CloudShadow>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider otherObject) {
		//Debug.Log("Collder : " + otherObject.tag);
		if(otherObject.tag == "flower" && !deerScared) {
			flowerEnter = true;
			Debug.Log("DEER Flower ENTER");
		}
		if(otherObject.tag == "jump" && deerScared && !deerJumped) {
			deerJump = true;
			deerJumped = true;
		}	
	}
	
	void OnTriggerStay (Collider otherObject) {
		//if(otherObject.tag == "flower" && !deerScared) {
		//	flowerEnter = true;
		//	Debug.Log("DEER Flower ENTER");
		//}
		
		if(otherObject.tag == "cloud" && flowerCollide && !deerScared) {
			//Debug.Log("Cloud COLIDE");
			if(cloundShadow.lightningActive) {
				Debug.Log("DEER LIGHTNING SCARED");
				deerScared = true;
			}
		}
	}
	
	void OnTriggerExit (Collider otherObject) {
		if(otherObject.tag == "flower" && !deerScared) {
			//Debug.Log("Flower ENTER Exit");
			flowerEnter = false;
		}
		if(otherObject.tag == "flower" && !deerScared) {
			Debug.Log("Flower COLIDE Exit");
			flowerCollide = false;
			//otherObject.GetComponent<FlowerController>().beingEatan = false;
			//deerScared = true;
		}	
	}
}
