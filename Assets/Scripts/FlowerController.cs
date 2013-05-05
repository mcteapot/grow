using UnityEngine;
using System.Collections;


public class FlowerController : MonoBehaviour {

	public enum FlowerLevels {
		levelOne = 0,
		levelTwo = 1
	}

	public FlowerLevels flowerLevel = FlowerLevels.levelOne;

	public float sunLevelMaxOne = 100.0F;
	public float sunLevelMaxTwo = 150.0F;
	public float waterLevelMaxOne = 100.0F;
	public float waterLevelMaxTwo = 150.0F;
	
	public float startSunLevel = 50.0F;
	public float startWaterLevel = 50.0F;
	
	public float levelIncreaseRate = 10.0F;
	public float levelDecreaseOneRate = 10.0F;
	public float levelDecreaseTwoRate = 10.0F;
	
	public float decreaseLevelTime = 10.0F;
	
	
	private float sunLevel;
	private float waterLevel;
	
	private bool levelIncrease = false;
	private bool levelDecrease = false;
	private bool beingEatan = false;

	private Transform flowerSmall;
	private Transform flowerLarge;

	private Transform flowerLevelUp;
	
	private Transform lifeBar;
	
	private ParticleSystem growParticle; 
	
	//public bool debugText = false;

	public int getLevel () {
		return (int)flowerLevel;
	}
	
	void Awake () {

		resetLevels(0);

		for( int i=0;i<transform.childCount;i++) {
			switch(transform.GetChild(i).name) {
			case "Flower S":
				flowerSmall = transform.GetChild(i);
				break;
			case "Flower L":
				flowerLarge = transform.GetChild(i);
				break;
			case "Flower Level":
				flowerLevelUp = transform.GetChild(i);
				break;
			case "Growl Particle System":
				Transform tmp;
				tmp = transform.GetChild(i);
				growParticle = tmp.particleSystem;
				break;
			case "Life Bar":
				lifeBar = transform.GetChild(i);
				break;
			default:
				break;
			}
		}
	}

	// Use this for initialization
	void Start () {
		// Sets a random rotation
		transform.rotation = Quaternion.Euler(0, Random.Range(-20.0F, 20.0F), 0);
		growParticle.enableEmission = false;
		StartCoroutine( decreseLevelCheck(decreaseLevelTime) );
	
	}
	
	// Update is called once per frame
	void Update () {
		if(flowerLevel == FlowerLevels.levelOne) {
			if(sunLevel >= sunLevelMaxOne && waterLevel >= waterLevelMaxOne) {
				levelUp();
			}
		} else if(flowerLevel == FlowerLevels.levelTwo) {
			if(sunLevel <= 0.0F && waterLevel <= 0.0F) {
				levelUp();
			}
		}
		setPrecent();
		levelParticle(levelIncrease);
		decreaseLevel();
	}
	
	void OnTriggerStay(Collider otherObject) {
		//Debug.Log("TRIGGER");
		
		bool tmpActive = otherObject.transform.parent.gameObject.GetComponent<LinkCollider>().getIsActive();
		levelIncrease = tmpActive;
		
		if(otherObject.tag == "cloud") {
			//Debug.Log ("CLOUND COLLIDE");
			if(levelIncrease) {
				levelDecrease = false;
				Debug.Log("LevelDecrease: " + levelDecrease);
				levelIncreaseWater();
			}
			
		}
		if(otherObject.tag == "sun") {
			//Debug.Log("SUN COLLIDE");
			if(levelIncrease) {
				levelDecrease = false;
				Debug.Log("LevelDecrease: " + levelDecrease);
				levelIncreaseLight();
			}
		}

		//Debug.Log("Is ACTIVE: " + tmpActive);
		
	}
	
	void OnTriggerExit(Collider otherObject) {
		levelIncrease = false;
		Debug.Log("ELVIS HAS LEFT THE BUILDING");
		StartCoroutine( decreseLevelCheck(decreaseLevelTime) );
	}
	
	void levelIncreaseWater () {
		//cloundTintTemp.a = Mathf.MoveTowards(cloudScript.Tint.a, cloudDeath, cloundLifeRate * Time.deltaTime);
		//Debug.Log ("Water Increasing: " + waterLevel);
		if(flowerLevel == FlowerLevels.levelOne) {
			float tmpLevel = Mathf.MoveTowards(waterLevel, waterLevelMaxOne, levelIncreaseRate * Time.deltaTime);
			waterLevel = Mathf.Clamp(tmpLevel, 0.0F, waterLevelMaxOne);
		} else if(flowerLevel == FlowerLevels.levelTwo) {
			float tmpLevel = Mathf.MoveTowards(waterLevel, waterLevelMaxTwo, levelIncreaseRate * Time.deltaTime);
			waterLevel = Mathf.Clamp(tmpLevel, 0.0F, waterLevelMaxTwo);
		}
	}
	
	void levelIncreaseLight () {
		//Debug.Log ("Light Increasing");
		if(flowerLevel == FlowerLevels.levelOne) {
			float tmpLevel = Mathf.MoveTowards(sunLevel, sunLevelMaxOne, levelIncreaseRate * Time.deltaTime);
			sunLevel = Mathf.Clamp(tmpLevel, 0.0F, sunLevelMaxOne);
		} else if(flowerLevel == FlowerLevels.levelTwo){
			float tmpLevel = Mathf.MoveTowards(sunLevel, sunLevelMaxTwo, levelIncreaseRate * Time.deltaTime);
			sunLevel = Mathf.Clamp(tmpLevel, 0.0F, sunLevelMaxTwo);
		}
	}
	
	void decreaseLevel () {
		if(levelDecrease) {
			float tmpWaterMaxLevel = 100.0F;
			float tmpSunMaxLevel = 100.0F;
			
			if(flowerLevel == FlowerLevels.levelOne) {
				tmpWaterMaxLevel = waterLevelMaxOne;
				tmpSunMaxLevel = sunLevelMaxOne;
			
				float tmpWaterLevel = Mathf.MoveTowards(waterLevel, 0.0F, levelDecreaseOneRate * Time.deltaTime);
				float tmpSunLevel = Mathf.MoveTowards(sunLevel, 0.0F, levelDecreaseOneRate * Time.deltaTime);
			
				waterLevel = Mathf.Clamp(tmpWaterLevel, 0.0F, tmpWaterMaxLevel);
				sunLevel = Mathf.Clamp(tmpSunLevel, 0.0F, tmpSunLevel);
			
			} else if(flowerLevel == FlowerLevels.levelTwo){
				tmpWaterMaxLevel = waterLevelMaxTwo;
				tmpSunMaxLevel = sunLevelMaxTwo;

				float tmpWaterLevel = Mathf.MoveTowards(waterLevel, 0.0F, levelDecreaseOneRate * Time.deltaTime);
				float tmpSunLevel = Mathf.MoveTowards(sunLevel, 0.0F, levelDecreaseOneRate * Time.deltaTime);
			
				waterLevel = Mathf.Clamp(tmpWaterLevel, 0.0F, tmpWaterMaxLevel);
				sunLevel = Mathf.Clamp(tmpSunLevel, 0.0F, tmpSunLevel);
			}
			
			Debug.Log("LEVEL IS DECREASING");
		}
	}
	
	void levelUp () {
		if(flowerLevel == FlowerLevels.levelOne) {
			flowerLevel = FlowerLevels.levelTwo;
			
			resetLevels(2);
			
			flowerSmall.animation.Play("flower_off");
			
			flowerLevelUp.gameObject.SetActive(true);
			flowerLevelUp.animation.Play();

			flowerLarge.gameObject.SetActive(true);
			flowerLarge.animation.Play();

			StartCoroutine( hideObject(1.0F, flowerSmall) );
			
			levelParticle(true);

		}
	}

	void levelDown () {
		if(flowerLevel == FlowerLevels.levelTwo) {
			flowerLevel = FlowerLevels.levelOne;
			
			resetLevels(1);
			
			flowerLarge.animation.Play("flower_off");
			
			flowerLevelUp.gameObject.SetActive(false);

			flowerSmall.gameObject.SetActive(true);
			flowerSmall.animation.Play();

			StartCoroutine( hideObject(1.0F, flowerLarge) );
			
			levelParticle(false);


		}
	}
	
	void setPrecent () {
		if(flowerLevel == FlowerLevels.levelOne) {
			lifeBar.SendMessage("setWaterPrecentage", Mathf.Clamp01(waterLevel / waterLevelMaxOne));
			lifeBar.SendMessage("setLightPrecentage", Mathf.Clamp01(sunLevel / sunLevelMaxOne));
		}
		if(flowerLevel == FlowerLevels.levelTwo) {
			lifeBar.SendMessage("setWaterPrecentage", Mathf.Clamp01(waterLevel / waterLevelMaxTwo));
			lifeBar.SendMessage("setLightPrecentage", Mathf.Clamp01(sunLevel / sunLevelMaxTwo));
		}
	}
	

	void levelParticle (bool newState) {
		growParticle.enableEmission = newState;
	}
	
	void resetLevels(int level) {
		switch(level) {
		case 0:
			sunLevel = startSunLevel;
			waterLevel = startWaterLevel;
			break;
		case 1:
			sunLevel = sunLevelMaxOne - startSunLevel;
			waterLevel = waterLevelMaxOne - startWaterLevel;
			break;
		case 2:
			sunLevel = sunLevelMaxOne + startSunLevel;
			waterLevel = waterLevelMaxOne + startWaterLevel;
			break;
		default:
			break;
		}
	}

	IEnumerator hideObject (float waitTime, Transform hidObj) {
		yield return new WaitForSeconds(waitTime);
		Debug.Log("HIDOBJECT: " + hidObj.name);
		hidObj.gameObject.SetActive(false);
	}
	
	IEnumerator decreseLevelCheck(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		if(!levelIncrease) {
			levelDecrease = true;
			Debug.Log("LevelDecrease: " + levelDecrease);
		}
	}
	
	

}
