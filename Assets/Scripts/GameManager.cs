using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public ArrayList cloudActive = new ArrayList();
	
	public float shadowCorrection;
	
	Transform[] cloudPrefabs = new Transform[3];
	
	public Transform cloudShadow;
	
	public int startCloundNum;
	
	// Use this for initialization
	void Start () {
	
		//cloudActive = createCloundsStart();
	}
	
	// Update is called once per frame
	void Update () {
		checkCloundActive();
	}
	
	void checkCloundActive () {
		for(int i = 0; i < cloudActive.Count; i++) {
			Transform activeClound = cloudActive[i] as Transform;
				CloudController cloundScript;
				cloundScript = activeClound.GetComponent<CloudController>();
			if(cloundScript.getIsActive()) {
				cloudShadow.position = new Vector3(activeClound.position.x + shadowCorrection, activeClound.position.y, activeClound.position.z);
				if(cloundScript.getCloundTintAOn() <= 0.06) {
					cloudShadow.SendMessage("setActive", false);
					
					Debug.Log("cloud destoryed");
					
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
	
	Transform createCloud () {
		Debug.Log("NEW CLOUD BITCHES");
		int numMax = cloudPrefabs.Length - 1;
		int numb = Random.Range(0, 3);
		Transform cloud =  Instantiate(cloudPrefabs[numb], getRandomSpawnPosition(), cloudPrefabs[numb].rotation) as Transform;
		
		return cloud;		
		
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
		//print(clouds.length);
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
		//print(firstNum);
		//print(secondNum);
		Vector3 aVector = new Vector3(Random.Range((0.5F + firstNum), (3.4F + secondNum)), Random.Range(10.5F, 13.1F), Random.Range(21.5F, 23.2F));
		return aVector;
		
	}
	
	Vector3 getRandomSpawnPosition () {
		Vector3 aVector = new Vector3(Random.Range(-15.1F, -9.1F), Random.Range(10.5F, 13.1F), Random.Range(21.5F, 23.2F));
		return aVector;
	}
}
