using UnityEngine;
using System.Collections;

public class DeerCollider : MonoBehaviour {
	
	public bool flowerCollide = false;
	
	public bool flowerEnter = false;
	
	public bool deerScared = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider otherObject) {
		if(otherObject.tag == "flower" && !deerScared) {
			flowerEnter = true;
		}

	}
/*	
	void OnTriggerStay (Collider otherObject) {
		if(otherObject.tag == "flower" && flowerCollide) {
			Debug.Log("Flower COLIDE");
			//flowerCollide = true;
			//otherObject.GetComponent<FlowerController>().beingEatan = true;
		}
	}
*/	
	void OnTriggerExit (Collider otherObject) {
		if(otherObject.tag == "flower") {
			flowerEnter = false;
		}
		if(otherObject.tag == "flower" && !deerScared) {
			Debug.Log("Flower COLIDE Exit");
			flowerCollide = false;
			//otherObject.GetComponent<FlowerController>().beingEatan = false;
			deerScared = true;
		}	
	}
}
