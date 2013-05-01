using UnityEngine;
using System.Collections;


public class FlowerController : MonoBehaviour {

	public enum FlowerLevels {
		levelOne = 0,
		levelTwo = 1
	}

	public FlowerLevels flowerLevel = FlowerLevels.levelOne;

	public float sunLevelOne = 100.0F;
	public float sunLevelTwo = 50.0F;
	public float waterLevelOne = 100.0F;
	public float waterLevelTwo = 50.0F;
	
	private float sunLevel;
	private float waterLevel;

	private Transform flowerSmall;
	private Transform flowerLarge;

	private Transform flowerLevelUp;

	public bool debugText = false;

	public int getLevel () {
		return (int)flowerLevel;
	}
	
	void Awake () {

		sunLevel = sunLevelOne;
		waterLevel = waterLevelOne;

		for( int i=0;i<transform.childCount;i++) {
			if(transform.GetChild(i).name=="Flower S") {
				flowerSmall=transform.GetChild(i);
			}
			if(transform.GetChild(i).name=="Flower L") {
				flowerLarge=transform.GetChild(i);
			}
			if(transform.GetChild(i).name=="Flower Level") {
				flowerLevelUp=transform.GetChild(i);
			}
		}
	}

	// Use this for initialization
	void Start () {
		// Sets a random rotation
		transform.rotation = Quaternion.Euler(0, Random.Range(-20.0F, 20.0F), 0);
	
	}
	
	// Update is called once per frame
	void Update () {
		if(debugText) {
			levelUp();
		} else {
			levelDown();
		}
	}

	void levelUp () {
		if(flowerLevel == FlowerLevels.levelOne) {
			flowerLevel = FlowerLevels.levelTwo;
			sunLevel = sunLevelTwo;
			waterLevel = waterLevelTwo;
			flowerSmall.animation.Play("flower_off");
			
			flowerLevelUp.gameObject.SetActive(true);
			flowerLevelUp.animation.Play();

			flowerLarge.gameObject.SetActive(true);
			flowerLarge.animation.Play();

			StartCoroutine( hideObject(1.0F, flowerSmall) );


		}
	}

	void levelDown () {
		if(flowerLevel == FlowerLevels.levelTwo) {
			flowerLevel = FlowerLevels.levelOne;
			sunLevel = sunLevelOne;
			waterLevel = waterLevelOne;
			flowerLarge.animation.Play("flower_off");
			
			flowerLevelUp.gameObject.SetActive(false);

			flowerSmall.gameObject.SetActive(true);
			flowerSmall.animation.Play();

			StartCoroutine( hideObject(1.0F, flowerLarge) );


		}
	}

	IEnumerator hideObject (float waitTime, Transform hidObj) {
		yield return new WaitForSeconds(waitTime);
		Debug.Log("HIDOBJECT: " + hidObj.name);
		hidObj.gameObject.SetActive(false);
	}
	

}
