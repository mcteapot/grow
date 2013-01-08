#pragma strict

internal var screenPos : Vector3;
internal var offset : Vector3;

var cam : Camera;

function Start () {

}


function Update () {

}


function OnMouseDown () {
	print(Input.mousePosition);
    screenPos = cam.WorldToScreenPoint (transform.position);
	print ("target is " + screenPos.x + " pixels from the left");
    offset = transform.position - cam.ScreenToWorldPoint(Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z));
}


function OnMouseDrag () {
	var curScreenPos : Vector3 = Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);

	var curPos : Vector3 = cam.ScreenToWorldPoint(curScreenPos) + offset;
	
	transform.position = curPos;

}


function OnMouseUp () {


}