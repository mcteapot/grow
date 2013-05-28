using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public enum GameStates {
		gameInit = 0,
		gameStart = 1,
		gameActive = 2,
		gameEnding = 3,
		gameEnd = 4,
		gameInActive = 5
	}
	
	public static GameStates gameState = GameStates.gameInActive;
	
	private ArrayList cloudActive = new ArrayList();
	private ArrayList flowerActive = new ArrayList();
	private ArrayList deerActive = new ArrayList();
	
	public float shadowCorrection;
	
	public Transform flowerPrefab;
	
	public int maxFlowers = 7;
	
	public Transform[] cloudPrefabs = new Transform[3];
	public int maxCloudsActive = 7;
	public int maxCloudsInActive = 3;
	private int maxClounds = 3;
	
	public float nextCloundIncrement = 30.5F;
	private float nextCloundTime = 0.0F;
	
	public Transform cloudShadow;
	
	public int startCloundNum;
	
	public float cloundBoundX = 38.0F;
	
	public float lightningRate = 1.5F;
	private float nextLighting = 0.0F;
	
	
	public Transform deerPrefab;
	
	public int maxDeerActive;
	public float nextDeerIncrement = 10.0F;
	private float nextDeerTime = 0.0F;
	
	
	private bool spawnDeers = false;
	
	public Transform cam;
	public Transform title;
	
	//Game Manage functions
	
	IEnumerator gameLoader (float waitTime) {
		gameState = GameStates.gameStart;
		
		cam.gameObject.animation.Play();
		title.gameObject.animation.Play();
		
		yield return new WaitForSeconds(waitTime);
		nextCloundTime = Time.time + nextCloundIncrement;
		maxClounds = maxCloudsActive;
		flowerGenerate();
		gameState = GameStates.gameActive;
	}
	
	IEnumerator gameEnder (float waitTime) {
		gameState = GameStates.gameEnd;
		
		spawnDeers = false;
			
		killAllFlowers();
		maxClounds = maxCloudsInActive;
		cam.gameObject.animation.Play("camera_off");
		title.gameObject.animation.Play("title_on");
		
		yield return new WaitForSeconds(waitTime);
		title.gameObject.GetComponent<TitleController>().isActive = false;
		gameState = GameStates.gameInActive;
	}
	void Awake () {
		cloudActive = createCloundsStart();	
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("flower count: " + flowerActive.Count);
		checkCloundActive();
		checkCloundBound();
		gameManage();
	}
	
	void gameManage () {
		if(gameState == GameStates.gameInit){
			Debug.Log("GAME INIT");
			StartCoroutine( gameLoader(1.5F) );
		
		}else if(gameState == GameStates.gameStart) {
			//Debug.Log("GAME START");
		
		}else if(gameState == GameStates.gameActive) {
			cloudGenerate();
			checkFlowerActive();
			deerGenerate();
			quitGameKey();
		
		}else if(gameState == GameStates.gameEnding) {
			//Debug.Log("GAME ENDING");
			StartCoroutine( gameEnder(1.5F) );
		
		}else if(gameState == GameStates.gameEnd) {
			//Debug.Log("GAME END");
		
		}else if(gameState == GameStates.gameInActive) {
			//Debug.Log("GAME INACTIVE");
			if(title.gameObject.GetComponent<TitleController>().isActive) {
				gameState = GameStates.gameInit;
				
			}
		}
	}

	
	void checkCloundActive () {
		for(int i = 0; i < cloudActive.Count; i++) {
			Transform activeClound = cloudActive[i] as Transform;
				CloudController cloundScript;
				cloundScript = activeClound.GetComponent<CloudController>();
			
			if(cloundScript.getIsActive()) {
				cloudShadow.position = new Vector3(activeClound.position.x + shadowCorrection, activeClound.position.y, activeClound.position.z);
				if(Input.GetMouseButtonDown(1)) {
					nextLighting = Time.time + lightningRate;
					cloudShadow.SendMessage("setLightning", true);
					cloundScript.lightningFire();
				}
				if(cloundScript.getCloundTintAOn() <= 0.06) {
					cloudShadow.SendMessage("setActive", false);
					cloudShadow.SendMessage("setLightning", false);
					
					Debug.Log("cloud destoryed");
					Destroy(activeClound.gameObject);
					cloudActive.RemoveAt(i);
					
					if(cloudActive.Count <= maxClounds) {
						cloudActive.Add(createCloud());
					}
					
					break;
				} else {
					cloudShadow.SendMessage("setActive", true);
					//shadowScript.setActive(true);
				}
				//if(Time.time > nextLighting) {
				//	nextLighting = Time.time + lightningRate;
				//	cloudShadow.SendMessage("setLightning", false);	
				//}
				//cloudShadow.SendMessage("setLightning", false);
				break;
			}
	
			if(i == (cloudActive.Count - 1)) {
				cloudShadow.SendMessage("setActive", false);
				//shadowScript.setActive(false);
			}

		}
	}
	
	void checkCloundBound () {
		for(int i = 0; i < cloudActive.Count; i++) {
			Transform activeClound = cloudActive[i] as Transform;
			if(activeClound.position.x > cloundBoundX) {
				Destroy(activeClound.gameObject);
				cloudActive.RemoveAt(i);
				cloudActive.Add(createCloud());
				break;
			}
		}
	}
	
	Transform createCloud () {
		//Debug.Log("NEW CLOUD BITCHES");
		int numMax = cloudPrefabs.Length - 1;
		int numb = Random.Range(0, 3);
		Transform cloud =  Instantiate(cloudPrefabs[numb], getRandomSpawnCloudPosition(), cloudPrefabs[numb].rotation) as Transform;
		
		return cloud;		
		
	}
	
	void cloudGenerate () {
		
		if(cloudActive.Count <= maxClounds) {
			if(Time.time > nextCloundTime) {
				//Debug.Log("CREATE generator" + cloudActive.Count);
				//Debug.Log("CREATE FUCKING CLOUD");
        		nextCloundTime = Time.time + nextCloundIncrement;
        		cloudActive.Add(createCloud());
    		}
		}
		
	}
	
	ArrayList createCloundsStart () {
		ArrayList clouds = new ArrayList();
		
		for(int i = 0; i<startCloundNum; i++) {
			//Debug.Log("VECTORS: " + getRandomStartCloudPosition());
			int numb = Random.Range(0, 3);
			//Debug.Log(numb);
			Transform cloud =  Instantiate(cloudPrefabs[numb], getRandomStartCloudPosition(i), cloudPrefabs[numb].rotation) as Transform;
			//Debug.Log(cloud.position);

			clouds.Add(cloud);
		}
		//Debug.Log(clouds.length);
		return clouds;
	}
	
	Transform createFlower (int num) {
		Transform newFlower = Instantiate(flowerPrefab, getRandomSpawnFlowerPosition(num), Quaternion.identity) as Transform;
		return newFlower;
	}
	
	void checkFlowerActive() {
		//Debug.Log("MANAGE PLANTS");
		for(int i = 0; i < flowerActive.Count; i++) {
			Transform activeFlower = flowerActive[i] as Transform;
			FlowerController flowerScript;
			flowerScript = activeFlower.GetComponent<FlowerController>();
			
			if(flowerScript.checkFlowerSpawn()) {
				flowerGenerate();
			}
			if(flowerScript.flowerLevel == FlowerController.FlowerLevels.levelDead) {
				flowerActive.RemoveAt(i);
				//flowerScript.setFlowerDying(true);
				Destroy(activeFlower.gameObject);
				Debug.Log("GAME OVER");
				gameState = GameStates.gameEnding;
				break;
			}
			//Debug.Log("FLOWER LEVEL: " + flowerScript.flowerLevel); 
		}
	}
	
	void flowerGenerate () {
		if(gameState == GameStates.gameStart) {
			flowerActive.Add (createFlower(0));
		} else if(gameState == GameStates.gameActive) {
			flowerActive.Add (createFlower(1));
		}
	}
	
	void killAllFlowers () {
		for(int i = 0; i < flowerActive.Count; i++) {
			Transform activeFlower = flowerActive[i] as Transform;
			FlowerController flowerScript;
			flowerScript = activeFlower.GetComponent<FlowerController>();
			
			flowerScript.setFlowerDying(true);
		
			
		}
		flowerActive.Clear();
	}
	Vector3 getRandomStartCloudPosition( int num ) {
		float firstNum;
		float secondNum;
		
		if(num >= 0) {
			firstNum = num * 10.0F;
			secondNum = num * 10.0F;
		} else {
			return new Vector3(0, 0, 0);
		}
		//Debug.Log(firstNum);
		//Debug.Log(secondNum);
		Vector3 aVector = new Vector3(Random.Range((0.5F + firstNum), (3.4F + secondNum)), Random.Range(10.5F, 13.1F), Random.Range(21.5F, 23.2F));
		return aVector;
		
	}
	
	
	Vector3 getRandomSpawnCloudPosition () {
		Vector3 aVector;
		if(gameState == GameStates.gameActive) {
			aVector = new Vector3(Random.Range(-45.1F, -9.1F), Random.Range(10.5F, 13.1F), Random.Range(21.5F, 23.2F));
		} else {
			aVector = new Vector3(Random.Range(-10.1F, -9.1F), Random.Range(10.5F, 13.1F), Random.Range(21.5F, 23.2F));

		}
		return aVector;
	}
	
	Vector3 getRandomSpawnFlowerPosition (int num) {
		Vector3 aVector;
		if(num == 0) {
			aVector = new Vector3(Random.Range(12.0F, 17.0F), Random.Range(-0.2F, 0.1F), Random.Range(19.0F, 19.5F));
		} else if(num == 1) {
			aVector = new Vector3(Random.Range(7.5F, 21.5F), Random.Range(-0.2F, 0.1F), Random.Range(18.5F, 20.0F));
		} else {
			aVector = new Vector3(Random.Range(7.5F, 21.5F), Random.Range(-0.2F, 0.1F), Random.Range(18.5F, 20.0F));
		}
		return aVector;
	}
	
	void deerGenerate () {
	
		if(spawnDeers == false && flowerActive.Count > 0) {
			Debug.Log("DEER SPAWNED");
			spawnDeers = true;
			Transform newestFlower = flowerActive[flowerActive.Count - 1] as Transform;
			createDeer(newestFlower);
		}
		
		//if(spawnDeers) {
		//	for(int i = 0; i < deerActive.Count; i++) {
		//	}
		//}
	}
	
	Transform createDeer (Transform flower) {
		Transform newDeer = Instantiate(deerPrefab) as Transform;
		
		DeerController deerController;
		deerController = newDeer.GetComponent<DeerController>();
		
		bool left = (flower.position.x > 14.5F) ? true : false;
		bool bottom = (((int)Mathf.Round((Random.value))) == 1) ? true : false;
		
		//determine spawn location
		if(left) {
			// left
			if(bottom) {
				deerController.setPreInits(flower, DeerController.MovePoint.leftBottom);
			} else {
				deerController.setPreInits(flower, DeerController.MovePoint.leftSide);
			}
		} else { 
			//right
			if(bottom) {
				deerController.setPreInits(flower, DeerController.MovePoint.rightBottom);
			} else {
				deerController.setPreInits(flower, DeerController.MovePoint.rightSide);
			}
		}
		
		deerActive.Add(newDeer);
		
		return newDeer;
		
	}
	
	void quitGameKey () {
		if(Input.GetKeyDown("q")) {
			Debug.Log("Game Quit");
			gameState = GameStates.gameEnding;
		}
	}
	
	
	

}
