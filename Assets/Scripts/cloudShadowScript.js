#pragma strict

var target : Transform;
var darkRay : Transform;
var darkProj : Projector;

var isActive : boolean;

internal var startRotation : Quaternion;

function Start () {
	lookAtCamera();
	isActive = false;
	startRotation = transform.rotation;
}

function Update () {
	lookAtCamera();
	setScale();
	animateShadow();
}


function lookAtCamera () {
    var relativePos : Vector3 = target.position - transform.position;
    //print("ROTATION: " + Quaternion.LookRotation(relativePos).eulerAngles.y);
    var yRotation : float = Quaternion.LookRotation(relativePos).eulerAngles.y;

    transform.rotation = Quaternion.Euler(Vector3(startRotation.eulerAngles.x, yRotation - 180, startRotation.eulerAngles.z));

}

function setScale () {
	darkRay.localScale.y = transform.position.y;
	//var proj : Projector = GetComponent (Projector);
	//print(darkRay.localScale.y);
	//print(darkProj.farClipPlane);
}

function animateShadow () {
	var aVelocity : float = 0.0;
	if(isActive) {
		darkProj.farClipPlane = Mathf.SmoothDamp(darkProj.farClipPlane, 50, aVelocity, 0.4);

	} else {
		darkProj.farClipPlane = Mathf.SmoothDamp(darkProj.farClipPlane, 0.01, aVelocity, 0.2);

	}
}
/*
function setActive (newActive : boolean) {
	isActive = newActive;
}
*/