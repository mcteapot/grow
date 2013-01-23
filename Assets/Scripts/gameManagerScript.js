#pragma strict

internal var cloudActive : Transform[];

var shadowCorrection : float;

var cloudPrefabs : Transform[];

//var cloudObject01 : Transform;
var cloudShadow : Transform;

function Start () {


	//print("arraySize: " + cloudActive.length);
	//print("cloudPrefabs: " + cloudPrefabs.length);
	//cloudActive = new Array();
	cloudActive = createCloundsStart();
	print(cloudActive.length);

}



function Update () {
	checkCloundActive();
}

function checkCloundActive () {
	for(var i:int=0; i<3; i++) {
		var cloundScript : CloudScript;
		cloundScript = cloudActive[i].GetComponent(CloudScript);
		var shadowScript : CloudShadowScript;
		shadowScript = cloudShadow.GetComponent(CloudShadowScript);
		print(cloundScript.isActive);
		
		if(cloundScript.isActive) {
			cloudShadow.position = Vector3(cloudActive[i].position.x + shadowCorrection, cloudActive[i].position.y, cloudActive[i].position.z);
			shadowScript.setActive(true);
			break;
		}

		if(i == 2) {
			shadowScript.setActive(false);
		}
	}
/*
	var cloundScript : CloudScript;
	cloundScript = cloudObject01.GetComponent(CloudScript);
	var shadowScript : CloudShadowScript;
	shadowScript = cloudShadow.GetComponent(CloudShadowScript);
	//print(cloundScript.isActive);

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

function createCloundsStart () {
	//var i : int = 3;
	var clouds = new Array();
	for(var i:int=0; i<3; i++) {
		//print("VECTORS: " + getRandomStartCloudPosition());
		var numb = Random.Range(0, 3);
		//print(numb);
		//var rotation = Quaternion.identity;
		//rotation.eulerAngles = Vector3(0, 90, 0);
		var cloud : Transform =  Instantiate(cloudPrefabs[numb], getRandomStartCloudPosition(i), cloudPrefabs[numb].rotation);
		//print(cloud.position);
		//Instantiate (prefab, Vector3(i * 2.0, 0, 0), Quaternion.identity);
		clouds.Add(cloud);
	}
	//print(clouds.length);
	return clouds;
}

function getRandomStartCloudPosition (num : int) {
	var firstNum : float;
	var secondNum : float;
	if(num >= 0) {
		firstNum = num * 10.0;
		secondNum = num * 10.0;
	} else {
		return Vector3(0, 0, 0);
	}
	//print(firstNum);
	//print(secondNum);
	var aVector : Vector3 = Vector3(Random.Range((0.5 + firstNum), (3.4 + secondNum)), Random.Range(10.5, 13.1), Random.Range(21.5, 23.2));
	return aVector;
}


