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
	
	public GameStates gameState = GameStates.gameInit;
	
	private ArrayList cloudActive = new ArrayList();
	private ArrayList flowerActive = new ArrayList();
	
	public float shadowCorrection;
	
	public Transform[] cloudPrefabs = new Transform[3];
	public int maxClounds = 7;
	
	public float nextCloundIncrement = 30.5F;
	private float nextCloundTime = 0.0F;
	
	public Transform cloudShadow;
	
	public int startCloundNum;
	
	public float cloundBoundX = 38.0F;
	
	public float lightningRate = 1.5F;
	private float nextLighting = 0.0F;
	
	public Transform cam;
	public Transform title;
	
	//Game Manage functions
	
	IEnumerator gameLoader (float waitTime) {
		gameState = GameStates.gameStart;
		cam.gameObject.animation.Play();
		title.gameObject.animation.Play();
		yield return new WaitForSeconds(waitTime);
		gameState = GameStates.gameActive;
	}
	
	IEnumerator gameEnder (float waitTime) {
		gameState = GameStates.gameEnd;
		cam.gameObject.animation.Play("camera_off");
		title.gameObject.animation.Play("title_on");
		yield return new WaitForSeconds(waitTime);
		gameState = GameStates.gameInActive;
	}
	// Use this for initialization
	void Start () {
	
		cloudActive = createCloundsStart();
		nextCloundTime = Time.time + nextCloundIncrement;
	}
	
	// Update is called once per frame
	void Update () {
		checkCloundActive();
		checkCloundBound();
		cloudGenerate();
		gameManage();
	}
	
	void gameManage () {
		if(gameState == GameStates.gameInit){
			Debug.Log("GAME INIT");
			StartCoroutine( gameLoader(1.5F) );
		}else if(gameState == GameStates.gameStart) {
			//Debug.Log("GAME START");
		}else if(gameState == GameStates.gameActive) {
			managePlants();
		}else if(gameState == GameStates.gameEnding) {
			//Debug.Log("GAME ENDING");
			StartCoroutine( gameEnder(1.5F) );
		}else if(gameState == GameStates.gameEnd) {
			//Debug.Log("GAME END");
		}else if(gameState == GameStates.gameInActive) {
			//Debug.Log("GAME INACTIVE");
			if(title.gameObject.GetComponent<TitleController>().isActive) {
				title.gameObject.GetComponent<TitleController>().isActive = false;
				gameState = GameStates.gameInit;
				
			}
		}
	}
	
	void managePlants() {
		Debug.Log("MANAGE PLANTS");
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
					
					Debug.Log("cloud destoryed");
					cloudShadow.SendMessage("setLightning", false);
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
		Transform cloud =  Instantiate(cloudPrefabs[numb], getRandomSpawnPosition(), cloudPrefabs[numb].rotation) as Transform;
		
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
	
	Vector3 getRandomSpawnPosition () {
		Vector3 aVector = new Vector3(Random.Range(-45.1F, -9.1F), Random.Range(10.5F, 13.1F), Random.Range(21.5F, 23.2F));
		return aVector;
	}
	

}
