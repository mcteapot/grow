#pragma strict

var cloudObject01 : Transform;
var cloudShadow : Transform;

function Start () {


}



function Update () {
	checkCloundActive();
}

function checkCloundActive() {
	var cloundScript : CloudScript;
	cloundScript = cloudObject01.GetComponent(CloudScript);
	var shadowScript : CloudShadowScript;
	shadowScript = cloudShadow.GetComponent(CloudShadowScript);
	//print(cloundScript.isActive);
	/*
	if(cloundScript.getActive()) {
		shadowScript.setActive(true);
		cloudShadow.position = cloudObject01.position;
	} else {
		shadowScript.setActive(false);
	}
	*/
	//cloudObject01.SendMessage("getActive", SendMessageOptions.RequireReceiver);
	//print(cloudObject01.SendMessage("getActive", SendMessageOptions.RequireReceiver));
}



