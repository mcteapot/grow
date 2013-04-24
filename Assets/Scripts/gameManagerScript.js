#pragma strict

//internal var cloudActive : Transform[];
internal var cloudActive  = new ArrayList();

var shadowCorrection : float;

var cloudPrefabs : Transform[];

//var cloudObject01 : Transform;
var cloudShadow : Transform;

var startCloundNum : int;

function Start () {


	//print("arraySize: " + cloudActive.length);
	//print("cloudPrefabs: " + cloudPrefabs.length);
	//cloudActive = new Array();
	cloudActive = createCloundsStart();
	//print(cloudActive.Count);

}



function Update () {
	checkCloundActive();
}

function checkCloundActive () {
	for(var i:int=0; i<cloudActive.Count; i++) {
		var activeClound : Transform = cloudActive[i] as Transform;
		var cloundScript : CloudScript;
		cloundScript = activeClound.GetComponent(CloudScript);
		//var shadowScript : CloudShadow;
		//shadowScript = cloudShadow.GetComponent(CloudShadow);
		//print(cloundScript.isActive);
		//print(cloundScript.cloundTintAOn);
		if(cloundScript.isActive) {
			cloudShadow.position = Vector3(activeClound.position.x + shadowCorrection, activeClound.position.y, activeClound.position.z);
			if(cloundScript.cloundTintAOn <= 0.06) {
				cloudShadow.SendMessage("setActive", false);
				//shadowScript.setActive(false);
				
				print("cloud destoryed");
				//var activeGameObjectCloud : GameObject = cloudActive[i];
				Destroy(activeClound.gameObject);
				cloudActive.RemoveAt(i);
				cloudActive.Add(createCloud());
				break;
			} else {
				cloudShadow.SendMessage("setActive", true);
				//shadowScript.setActive(true);
			}
			break;
		}

		if(i == (cloudActive.Count - 1)) {
			cloudShadow.SendMessage("setActive", false);
			//shadowScript.setActive(false);
		}
	}

}

function createCloud () {
	print("NEW CLOUD BITCHES");
	var numMax : int = cloudPrefabs.length - 1;
	var numb : int = Random.Range(0, 3);
	//var cloud : Transform = Instantiate(cloudPrefabs[numb], getRandomSpawnPosition(), Quaternion.identity);
	var cloud : Transform =  Instantiate(cloudPrefabs[numb], getRandomSpawnPosition(), cloudPrefabs[numb].rotation);
	return cloud;

}

function createCloundsStart () {
	//var i : int = 3;
	var clouds = new ArrayList();
	for(var i:int=0; i<startCloundNum; i++) {
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

function getRandomSpawnPosition () {

	var aVector : Vector3 = Vector3(Random.Range(-15.1, -9.1), Random.Range(10.5, 13.1), Random.Range(21.5, 23.2));
	return aVector;
}


