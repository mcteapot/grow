#pragma strict

var target : Transform;

internal var startRotation : Quaternion;

function Start () {
	lookAtCamera();
	startRotation = transform.rotation;
}

function Update () {
	lookAtCamera();
}


function lookAtCamera () {
    var relativePos : Vector3 = target.position - transform.position;
    //print("ROTATION: " + Quaternion.LookRotation(relativePos).eulerAngles.y);
    var yRotation : float = Quaternion.LookRotation(relativePos).eulerAngles.y;

    transform.rotation = Quaternion.Euler(Vector3(startRotation.eulerAngles.x, yRotation - 180, startRotation.eulerAngles.z));

}