#pragma strict

internal var screenPos : Vector3;
internal var offset : Vector3;
internal var positionZ : float;

internal var isActive : boolean;
internal var isSetY : boolean;
internal var amoutToMove : float;

var cam : Camera;
var beam : GameObject;
var groundLight : GameObject;

var positionY : float;
var spaceY : float;
var moveRate : float;

var boundTop : float;
var boundBottom : float;
var boundRight : float;
var boundLeft : float;
var correction : float;

function Start () {
	positionZ = transform.position.z;
	isActive = false;
	isSetY = false;
	
	
}


function Update () {
	checkBound();
	returnPosition(transform.position.y);
}


function OnMouseDown () {
	isActive = true;
	isSetY = false;
	beamActive(true);
    moveDown();
}


function OnMouseDrag () {
	moveDrag();
}

function OnMouseUp () {
	if(isActive) {
		beamActive(false);
		isActive = false;
	}
}

// starts position moves calculations
function moveDown () {
	if(isActive) {
		screenPos = cam.WorldToScreenPoint (transform.position);
    	offset = transform.position - cam.ScreenToWorldPoint(Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z));
	} 	
}
// position moves calculations
function moveDrag () {
	if(isActive) {
		var curScreenPos : Vector3 = Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);

		var curPos : Vector3 = cam.ScreenToWorldPoint(curScreenPos) + offset;
		curPos.z = positionZ;
		//print("currentPOS: " + curPos);
		transform.position = curPos;
	}
}

// check if object is in screen bounds
function checkBound () {
	//print("transformPOS: " + transform.position);
	if(transform.position.x >= boundRight) {
		OnMouseUp();
		transform.position.x -= correction;
	} else if(transform.position.x <= boundLeft) {
		OnMouseUp();
		transform.position.x += correction;
	} else if(transform.position.y >= boundTop) {
		OnMouseUp();
	} else if(transform.position.y <= boundBottom) {
		OnMouseUp();
	}
}
// moves sun back to set Y position
function returnPosition (currentPositionY : float) {
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
function checkY (currentPositionY : float) {
	if(positionY >= (currentPositionY - spaceY) && positionY <= (currentPositionY + spaceY)) {
		isSetY = true;
	} 
}

// activates and deactivets beam
function beamActive (beamState : boolean) {
	if(beamState) {
		beam.SendMessage("beamOn", SendMessageOptions.RequireReceiver);
		groundLight.SendMessage("beamOn", SendMessageOptions.RequireReceiver);
	} else if(!beamState) {
		beam.SendMessage("beamOff", SendMessageOptions.RequireReceiver);
		groundLight.SendMessage("beamOff", SendMessageOptions.RequireReceiver);
	}
	
}
