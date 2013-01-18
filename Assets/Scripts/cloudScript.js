#pragma strict

#pragma strict

internal var screenPos : Vector3;
internal var offset : Vector3;
internal var positionZ : float;
internal var positionY : float;

internal var isActive : boolean;
internal var isBounding : boolean;
internal var isSetY : boolean;
internal var amoutToMove : float;

var cam : Camera;
var rain : GameObject;
var groundLight : GameObject;

//var positionY : float;
//var spaceY : float;
var moveRate : float;

var boundRight : float;
var boundLeft : float;
var correction : float;

function Start () {
	positionZ = transform.position.z;
	positionY = transform.position.y;
	isActive = false;
	isBounding = false;
	isSetY = false;
	
}

function Update () {
	checkBound();
	moveCloud();
}

function OnMouseDown () {
	isActive = true;
	isBounding = true;
	isSetY = false;	
    moveDown();
}


function OnMouseDrag () {
	moveDrag();
}

function OnMouseUp () {
	if(isActive) {
		isActive = false;
		isBounding = false;
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
		curPos.y = positionY;
		//print("currentPOS: " + curPos);
		transform.position = curPos;
	}
}

// check if object is in screen bounds
function checkBound () {
	print("transformPOS: " + transform.position);
	if(transform.position.x >= boundRight && isBounding) {
		OnMouseUp();
		//transform.position.x -= correction;
	} else if(transform.position.x <= boundLeft && isBounding) {
		OnMouseUp();
		//transform.position.x += correction;
	} 
}

// move clouds
function moveCloud () {
	if(!isBounding) {
		amoutToMove = moveRate * Time.deltaTime;
		transform.Translate(Vector3.forward * amoutToMove);
	}
}