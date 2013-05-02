using UnityEngine;
using System.Collections;

public class SunController : MonoBehaviour {
	
	private Vector3 screenPos;
	private Vector3 offset;
	private float positionZ;
	
	private bool isActive;
	private bool isSetY;
	private float amoutToMove;
	
	public Camera cam;
	public GameObject beam;
	public GameObject groundLight;
	
	public float positionY;
	public float spaceY;
	public float moveRate;
	
	public float boundTop;
	public float boundBottom;
	public float boundRight;
	public float boundLeft;
	public float correction;
	
	public bool getIsActive() {
		return isActive;
	}

	// Use this for initialization
	void Start () {
		positionZ = transform.position.z;
		isActive = false;
		isSetY = false;
	}
	
	// Update is called once per frame
	void Update () {
		checkBound();
		returnPosition(transform.position.y);
	}
	
	void OnMouseDown () {
		isActive = true;
		isSetY = false;
		beamActive(true);
	    moveDown();
		audio.Play();

	}
	
	
	void OnMouseDrag () {
		moveDrag();
	}
	
	void OnMouseUp () {
		if(isActive) {
			beamActive(false);
			isActive = false;
			audio.Stop();
		}
	
	}
	
	// starts position moves calculations
	void moveDown () {
		if(isActive) {
			screenPos = cam.WorldToScreenPoint (transform.position);
			offset = transform.position - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z));
		} 
	
	}
	// position moves calculations
	void moveDrag () {
		if(isActive) {
			Vector3 curScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);
	
			Vector3 curPos = cam.ScreenToWorldPoint(curScreenPos) + offset;
			curPos.z = positionZ;
			//print("currentPOS: " + curPos);
			transform.position = curPos;
		}
	}
	
	// check if object is in screen bounds
	void checkBound () {
		//print("transformPOS: " + transform.position);
		if(transform.position.x >= boundRight) {
			OnMouseUp();
			Vector3 tempVector = transform.position;
			tempVector.x -= correction;
			transform.position = tempVector;
		} else if(transform.position.x <= boundLeft) {
			OnMouseUp();
			Vector3 tempVector = transform.position;
			tempVector.x += correction;
			transform.position = tempVector;
		} else if(transform.position.y >= boundTop) {
			OnMouseUp();
		} else if(transform.position.y <= boundBottom) {
			OnMouseUp();
		}
	}
	// moves sun back to set Y position
	void returnPosition (float currentPositionY) {
		if(!isActive && !isSetY) {
			checkY(currentPositionY);
			amoutToMove = moveRate * Time.deltaTime;
			
			if(currentPositionY <= positionY){
				transform.Translate(Vector3.up * amoutToMove);
			}
			if(currentPositionY >= positionY){
				transform.Translate(Vector3.up * (-amoutToMove));
			}
		}
	}
	
	// if in Y position stops movement
	void checkY (float currentPositionY) {
		if(positionY >= (currentPositionY - spaceY) && positionY <= (currentPositionY + spaceY)) {
			isSetY = true;
		} 
	}
	
	// activates and deactivets beam
	void beamActive (bool beamState) {
		if(beamState) {
			beam.SendMessage("beamOn", SendMessageOptions.RequireReceiver);
			groundLight.SendMessage("beamOn", SendMessageOptions.RequireReceiver);
		} else if(!beamState) {
			beam.SendMessage("beamOff", SendMessageOptions.RequireReceiver);
			groundLight.SendMessage("beamOff", SendMessageOptions.RequireReceiver);
		}
	}
}
