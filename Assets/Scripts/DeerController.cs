using UnityEngine;
using System.Collections;

public class DeerController : MonoBehaviour {
	
	public enum DeerLevels {
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
	
	public float walkSpeed;
	
	private Transform deerColliderObject;
	private DeerCollider deerCollider;
	
	private Animator anim;
	
	public Transform flowerLink;
	
	public MovePoint offPoint;
	private Vector3 offLocation;
	public	bool willRear = false;
	
	// Use this for initialization
	void Start () {
		deerColliderObject = transform.Find("DeerCollider");
		deerCollider = deerColliderObject.GetComponent<DeerCollider>();
		
		anim = GetComponentInChildren<Animator>();
		
		initBounds();
		findStartEndPoint();
		
		if(deerLevel == DeerLevels.inited) {
			anim.SetBool("Walk", true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(anim.GetBool("Walk"));
		checkLevel();
		Debug.Log("Random Range: " + Random.Range(0, 3));
	}
	
	void initBounds () {
		enterPointZ[0] = new MinMax<float>(19.0F, 22.6F); // bottom
		enterPointZ[1] = new MinMax<float>(0.6F, 13.1F); // top side
		enterPointZ[2] = new MinMax<float>(0.3F, 1.1F); // top back
		
		enterPointX[0] = new MinMax<float>(-2.3F, -4.1F); // left side
		enterPointX[1] = new MinMax<float>(30.5F, 32.1F); // right side
		enterPointX[2] = new MinMax<float>(19.5F, 24.5F); // left back
		enterPointX[3] = new MinMax<float>(0.9F, 9.5F); // right back
	}
	
	
	void findStartEndPoint () {
		
		int offNum;
		
		switch(spawnPoint) {
		case MovePoint.leftBottom:
			offNum = Random.Range(0, 3);
			
			switch(offNum) {
			case 0:
				offPoint = MovePoint.rightBottom;
				break;
			case 1:
				offPoint = MovePoint.rightSide;
				break;
			case 2:
				break;
				offPoint = MovePoint.rightBack;
			default:
				offPoint = MovePoint.rightSide;
				break;
			}
			
			willRear = (((int)Mathf.Round((Random.value))) == 1) ? true : false;
			break;
		case MovePoint.rightBottom:
			offNum = Random.Range(0, 3);
			
			switch(offNum) {
			case 0:
				offPoint = MovePoint.leftBottom;
				break;
			case 1:
				offPoint = MovePoint.leftSide;
				break;
			case 2:
				break;
				offPoint = MovePoint.leftBack;
			default:
				offPoint = MovePoint.leftSide;
				break;
			}
			
			willRear = (((int)Mathf.Round((Random.value))) == 1) ? true : false;
			break;
		case MovePoint.leftSide:
			offPoint = MovePoint.rightBottom;
			willRear = true;
			break;
		case MovePoint.rightSide:
			offPoint = MovePoint.leftBottom;
			willRear = true;
			break;
		default:
			break;
		}
		
	}
	
	Vector3 getMovePointPos(MovePoint point) {
		Vector3 posVector = new Vector3(0, 0, 0);
		
		switch(point) {
		case MovePoint.leftBottom:
			break;
		case MovePoint.rightBottom:
			break;
		case MovePoint.leftSide:
			break;
		case MovePoint.rightSide:
			break;
		case MovePoint.leftBack:
			break;
		case MovePoint.rightBack:
		default:
			break;
		}
		
		return posVector;
	}
	
	
	void checkLevel () {
		switch(deerLevel) {
		case  DeerLevels.inited:
			linkFlower();
			break;
		case DeerLevels.flowerHit:
			deerLevel = DeerLevels.eating;
			anim.SetBool("Walk", false);
			break;
		case DeerLevels.eating:
			break;
		case DeerLevels.scared:
			flowerOff();
			break;
		case DeerLevels.dead:
			break;
		default:
			break;
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
				if(distanceX <= 0.223 && deerCollider.flowerEnter) {
					deerCollider.flowerCollide = true;
					flowerLink.GetComponent<FlowerController>().beingEatan = true;
					Debug.Log("Flower COLIDE");
				}
			} else {
				deerLevel = DeerLevels.flowerHit;
			}
		}
	}
	
	void flowerOff () {
		if(flowerLink && deerCollider.flowerCollide) {
			if(flowerLink.GetComponent<FlowerController>().beingEatan == true) {
				flowerLink.GetComponent<FlowerController>().beingEatan = false;
			}
		}
	}
	
	
}
