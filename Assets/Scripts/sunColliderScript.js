#pragma strict

var sun : Transform;

function Start () {
	moveCollider();
}

function Update () {
	moveCollider();
}

function moveCollider () {
	transform.position = Vector3(sun.position.x, sun.position.y, sun.position.z);
}