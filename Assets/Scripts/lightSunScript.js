#pragma strict

var sun : Transform;
internal var basePos : Vector3;

function Start () {
	basePos = Vector3(transform.position.x, transform.position.y, transform.position.z);
	//print("BASEPOS: " + basePos);
	//print("SUNPOS: " + sun.position);
	moveLight();
}

function Update () {
	moveLight();
}

function moveLight () {
	if(transform.gameObject.name == "Light Ground Sun") {
		transform.position = Vector3(sun.position.x, basePos.y, basePos.z);
	} else if (transform.gameObject.name == "Light Directional Sun") {
		transform.position = Vector3(sun.position.x, sun.position.y, basePos.z);
	}

}