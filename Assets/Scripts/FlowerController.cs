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
	
	private float sunLevel;
	private float waterLevel;

	private Transform flowerSmall;
	private Transform flowerLarge;

	private Transform flowerLevelUp;
	
	private Transform lifeBar;
	
	private ParticleSystem growParticle; 

	public bool debugText = false;

	public int getLevel () {
		return (int)flowerLevel;
	}
	
	void Awake () {

		resetLevels();

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
	
	}
	
	// Update is called once per frame
	void Update () {
		if(sunLevel >= sunLevelMaxOne && waterLevel >= waterLevelMaxOne) {
			levelUp();
		} else {
			levelDown();
		}
		setPrecent();
	}
	
	void OnTriggerStay(Collider otherObject) {
		Debug.Log("TRIGGER");
		if(otherObject.tag == "cloud") {
			Debug.Log ("CLOUND COLLIDE");
			
		}
		if(otherObject.tag == "sun") {
			Debug.Log("SUN COLLIDE");
		}
	}
	
	void levelUp () {
		if(flowerLevel == FlowerLevels.levelOne) {
			flowerLevel = FlowerLevels.levelTwo;
			
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
			lifeBar.SendMessage("setWaterPrecentage", waterLevel / waterLevelMaxOne);
			lifeBar.SendMessage("setLightPrecentage", sunLevel / sunLevelMaxOne);
		}
		if(flowerLevel == FlowerLevels.levelTwo) {
			lifeBar.SendMessage("setWaterPrecentage", waterLevel / waterLevelMaxTwo);
			lifeBar.SendMessage("setLightPrecentage", sunLevel / sunLevelMaxTwo);
		}
	}
	

	void levelParticle (bool newState) {
		growParticle.enableEmission = newState;
	}
	
	void resetLevels() {
		sunLevel = startSunLevel;
		waterLevel = startWaterLevel;
	}

	IEnumerator hideObject (float waitTime, Transform hidObj) {
		yield return new WaitForSeconds(waitTime);
		Debug.Log("HIDOBJECT: " + hidObj.name);
		hidObj.gameObject.SetActive(false);
	}
	

}
