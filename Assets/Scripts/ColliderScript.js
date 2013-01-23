#pragma strict

var sun : Transform;
internal var basePos : Vector3;

function Start () {
	basePos = Vector3(transform.position.x, transform.position.y, transform.position.z);

	moveCollider();
}

function Update () {
	moveCollider();
}

function moveCollider () {
	//transform.position = Vector3(sun.position.x, sun.position.y, sun.position.z);
	transform.position = Vector3(sun.position.x, basePos.y, basePos.z);
}