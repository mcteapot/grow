using UnityEngine;
using System.Collections;

public class DeerController : MonoBehaviour {
	
	public enum DeerLevels {
		preinited = -1,
		inited = 0,
		flowerHit = 1,
		eating = 2,
		scared = 3,
		dead = 4
	}
	
	public enum MovePoint {
		leftBottom = 0,
		rightBottom = 1,
		leftSide = 2,
		rightSide = 3,
		leftBack = 4,
		rightBack = 5,
	}
	
	public MinMax<float>[] enterPointZ = new MinMax<float>[3];
	public MinMax<float>[] enterPointX = new MinMax<float>[4];
	
	public DeerLevels deerLevel = DeerLevels.inited;
	public MovePoint spawnPoint;
	private Vector3 startLocation;
	
	public float walkSpeed;
	public float runSpeed;
	
	private Transform deerColliderObject;
	private DeerCollider deerCollider;
	
	private Animator anim;
	
	public Transform flowerLink;
	
	public MovePoint offPoint;
	public MovePoint quitPoint;
	private Vector3 offLocation;
	public bool willRear = false;
	
	private bool isRear = false;
	private bool isRun = false;
	
 	public Transform endTarget;
	public float rotationSpeed;
    private Quaternion lookRotation;
    private Vector3 rotateDirection;
	
	public bool gameEnd = false;
	
	public bool debuggin = false;
	
	// Public method to start the deer when not debug
	public void setPreInits(Transform newFlowerLink, MovePoint newSpawnPoint) {
		flowerLink = newFlowerLink;
		spawnPoint = newSpawnPoint;
		preInitedCheck();
	}
	
	
	// Use this for initialization
	void Start () {
		deerColliderObject = transform.Find("DeerCollider");
		deerCollider = deerColliderObject.GetComponent<DeerCollider>();
		
		anim = GetComponentInChildren<Animator>();
		
		initBounds();
		
		if(debuggin) {
			
			endTarget = (GameObject.Find("Off Point")).transform;
			
			findStartEndPoint();
			
			transform.position = startLocation;
			
			deerLevel = DeerLevels.inited;
			
			if(deerLevel == DeerLevels.inited) {
				anim.SetBool("Walk", true);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(anim.GetBool("Walk"));
		checkLevel();
		///Debug.Log("Random Range: " + Random.Range(0, 3));
	}
	
	void initBounds () {
		enterPointZ[0] = new MinMax<float>(19.0F, 22.6F); // bottom
		enterPointZ[1] = new MinMax<float>(0.6F, 13.1F); // top side
		enterPointZ[2] = new MinMax<float>(0.3F, 1.1F); // top back
		
		enterPointX[0] = new MinMax<float>(-4.3F, -5.1F); // left side
		enterPointX[1] = new MinMax<float>(34.5F, 35.1F); // right side
		enterPointX[2] = new MinMax<float>(0.9F, 14.0F); // left back
		enterPointX[3] = new MinMax<float>(15.0F, 24.5F); // right back
	}
	
	// Lot of starting shit happends here
	void findStartEndPoint () {
		
		int offNum;
		
		switch(spawnPoint) {
		case MovePoint.leftBottom:
			offNum = Random.Range(0, 3);
			
			switch(offNum) {
			case 0:
				offPoint = MovePoint.rightBottom;
				willRear = (((int)Mathf.Round((Random.value))) == 1) ? true : false;
				break;
			case 1:
				offPoint = MovePoint.rightSide;
				willRear = true;
				break;
			case 2:
				offPoint = MovePoint.rightBack;
				willRear = true;
				break;
			default:
				offPoint = MovePoint.rightSide;
				willRear = true;
				break;
			}
			
			quitPoint = MovePoint.rightBottom;
			
			startLocation = getMovePointPos(spawnPoint);
			offLocation = getMovePointPos(offPoint);
			
			break;
		case MovePoint.rightBottom:
			offNum = Random.Range(0, 3);
			
			switch(offNum) {
			case 0:
				offPoint = MovePoint.leftBottom;
				willRear = (((int)Mathf.Round((Random.value))) == 1) ? true : false;
				break;
			case 1:
				offPoint = MovePoint.leftSide;
				willRear = true;
				break;
			case 2:
				offPoint = MovePoint.leftBack;
				willRear = true;
				break;
			default:
				offPoint = MovePoint.leftSide;
				willRear = true;
				break;
			}
			
			quitPoint = MovePoint.leftBottom;

			startLocation = getMovePointPos(spawnPoint);
			offLocation = getMovePointPos(offPoint);
			
			break;
		case MovePoint.leftSide:
			offPoint = MovePoint.rightBottom;
			
			quitPoint = MovePoint.rightBottom;
			
			startLocation = getMovePointPos(spawnPoint);
			offLocation = getMovePointPos(offPoint);
			
			willRear = true;
			break;
		case MovePoint.rightSide:
			offPoint = MovePoint.leftBottom;
			
			quitPoint = MovePoint.leftBottom;
			
			startLocation = getMovePointPos(spawnPoint);
			offLocation = getMovePointPos(offPoint);
			
			willRear = true;
			break;
		default:
			break;
		}
		
	}
	
	Vector3 getMovePointPos(MovePoint point) {
		Vector3 posVector = new Vector3(0, 0, 0);
		float xPos = 0;
		float zPos = 0;
		switch(point) {
		case MovePoint.leftBottom:
			xPos = Random.Range(enterPointX[0].min, enterPointX[0].max);
			zPos = Random.Range(enterPointZ[0].min, enterPointZ[0].max);
			posVector = new Vector3(xPos, 0, zPos);
			break;
		case MovePoint.rightBottom:
			xPos = Random.Range(enterPointX[1].min, enterPointX[1].max);
			zPos = Random.Range(enterPointZ[0].min, enterPointZ[0].max);
			posVector = new Vector3(xPos, 0, zPos);
			break;
		case MovePoint.leftSide:
			xPos = Random.Range(enterPointX[0].min, enterPointX[0].max);
			zPos = Random.Range(enterPointZ[1].min, enterPointZ[1].max);
			posVector = new Vector3(xPos, 0, zPos);
			break;
		case MovePoint.rightSide:
			xPos = Random.Range(enterPointX[1].min, enterPointX[1].max);
			zPos = Random.Range(enterPointZ[1].min, enterPointZ[1].max);
			posVector = new Vector3(xPos, 0, zPos);
			break;
		case MovePoint.leftBack:
			xPos = Random.Range(enterPointX[2].min, enterPointX[2].max);
			zPos = Random.Range(enterPointZ[2].min, enterPointZ[2].max);
			posVector = new Vector3(xPos, 0, zPos);
			break;
		case MovePoint.rightBack:
			xPos = Random.Range(enterPointX[3].min, enterPointX[3].max);
			zPos = Random.Range(enterPointZ[2].min, enterPointZ[2].max);
			posVector = new Vector3(xPos, 0, zPos);
			break;
		default:
			break;
		}
		
		return posVector;
	}
	
	
	void checkLevel () {
		switch(deerLevel) {
		case DeerLevels.preinited:
			Debug.Log("Living in Preinint");
			break;
		case  DeerLevels.inited:
			checkGameEnd();
			linkFlower();
			break;
		case DeerLevels.flowerHit:
			deerLevel = DeerLevels.eating;
			anim.SetBool("Walk", false);
			break;
		case DeerLevels.eating:
			checkGameEnd();
			checkScared();
			break;
		case DeerLevels.scared:
			flowerOff();
			scaredOff();
			break;
		case DeerLevels.dead:
			break;
		default:
			break;
		}
	}
	
	void checkGameEnd () {
		if(gameEnd) {
			offLocation = getMovePointPos(quitPoint);
			if(deerLevel == DeerLevels.inited || deerLevel == DeerLevels.flowerHit) {
				//anim.SetBool("Walk", false);
				willRear = false;
				deerCollider.deerScared = true;
				deerLevel = DeerLevels.eating;
				
			} else if (deerLevel == DeerLevels.eating) {
				willRear = false;
				deerCollider.deerScared = true;
			}
		}
	}
	void preInitedCheck() {
		if(flowerLink) {
			findStartEndPoint();
			
			transform.position = startLocation;
			
			deerLevel = DeerLevels.inited;
			
			if(deerLevel == DeerLevels.inited) {
				anim.SetBool("Walk", true);
			}
			
		} 
	}
	
	
	void linkFlower () {
		if(flowerLink) {
			if(deerCollider.flowerCollide == false) {
				
				transform.LookAt(flowerLink.position);
				Vector3 tmpVector = Vector3.MoveTowards(transform.position, flowerLink.position, walkSpeed * Time.deltaTime);
				transform.position = new Vector3(tmpVector.x, transform.position.y, tmpVector.z);
				
				//Debug.Log("Deer Distance X" + Mathf.Abs(transform.position.x - flowerLink.position.x));
				float distanceX = Mathf.Abs(transform.position.x - flowerLink.position.x);
				if(distanceX <= 2.323 && deerCollider.flowerEnter) {
					deerCollider.flowerCollide = true;
					flowerLink.GetComponent<FlowerController>().beingEatan = true;
					Debug.Log("Flower COLIDE");
				}
			} else {
				deerLevel = DeerLevels.flowerHit;
			}
		}
	}
	void checkScared () {
		if(deerCollider.deerScared) {
			deerLevel = DeerLevels.scared;
			StartCoroutine(runOffLevel());
		}
	}
	void flowerOff () {
		if(flowerLink && deerCollider.flowerCollide) {
			if(flowerLink.GetComponent<FlowerController>().beingEatan == true) {
				flowerLink.GetComponent<FlowerController>().beingEatan = false;
			}
		}
	}
	
	void scaredOff () {
		if(isRear) {
			rotateToward();
		}  
		if(isRun) {
			//Debug.Log("is RUNNING");
			runToward();	
		}
		
		if(deerCollider.deerJump == true && !gameEnd) {
			anim.SetBool("Jump", true);
			deerCollider.deerJump = false;
			//anim.SetBool("Jump", false);
		}
		
	}
	
	void rotateToward () {
		//find the vector pointing from our position to the target
		if(debuggin) {
			rotateDirection = (endTarget.position - transform.position).normalized;
		} else {
			rotateDirection = (offLocation - transform.position).normalized;	
		}
		
		//create the rotation we need to be in to look at the target
		lookRotation = Quaternion.LookRotation(rotateDirection);
		
		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
		
	}
	
	void runToward () {
		Vector3 tmpVector = new Vector3(0, 0, 0);
		float distanceX = 0.0F;
		if(debuggin) {
			tmpVector = Vector3.MoveTowards(transform.position, endTarget.position, runSpeed * Time.deltaTime);
			distanceX = Mathf.Abs(transform.position.x - endTarget.position.x);
		} else {
			tmpVector = Vector3.MoveTowards(transform.position, offLocation, runSpeed * Time.deltaTime);	
			distanceX = Mathf.Abs(transform.position.x - offLocation.x);
		}
		
		transform.position = new Vector3(tmpVector.x, transform.position.y, tmpVector.z);
		
		if(distanceX <= 0.323) {
			deerLevel = DeerLevels.dead;
			Debug.Log("Deer End");
		}
	}
	
	IEnumerator runOffLevel () {
		float tmpTime = 1.6F;
		if(willRear) {
			anim.SetBool("Rear", true);
			yield return new WaitForSeconds(0.4F);
			isRear = true;
		} else {
			anim.SetBool("Walk", true);
			anim.SetBool("Run", true);
			tmpTime = 0.32F;
		}
		
		if(gameEnd) {
			isRear = true;
			tmpTime = 0.02F;
		}
		
		yield return new WaitForSeconds(tmpTime);
		isRun = true;

	}
	
}
