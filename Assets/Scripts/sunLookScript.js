#pragma strict

var sun : Transform;
var target : Transform;

internal var startRotation : Quaternion;

function Start () {
	startRotation = transform.rotation;
	moveBeam();
	lookAtCamera();
}

function Update () {
	moveBeam();
	lookAtCamera();
}

function lookAtCamera () {
    var relativePos : Vector3 = target.position - transform.position;
    //print("ROTATION: " + Quaternion.LookRotation(relativePos).eulerAngles.y);
    var yRotation : float = Quaternion.LookRotation(relativePos).eulerAngles.y;

    transform.rotation = Quaternion.Euler(Vector3(startRotation.eulerAngles.x, yRotation - 180, startRotation.eulerAngles.z));

}

function moveBeam () {
	transform.position = Vector3(sun.position.x, sun.position.y, sun.position.z);
}
