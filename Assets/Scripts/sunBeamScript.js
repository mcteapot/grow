#pragma strict

var target : Transform;

function Start () {
	lookAtCamera();
}

function Update () {
	lookAtCamera();
}

function lookAtCamera () {
    var relativePos : Vector3 = target.position - transform.position;
    //print("ROTATION: " + Quaternion.LookRotation(relativePos).eulerAngles.y);
    var yRotation : float = Quaternion.LookRotation(relativePos).eulerAngles.y;

    transform.rotation = Quaternion.Euler(Vector3(0, yRotation - 180, 0));

}
