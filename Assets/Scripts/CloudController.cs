using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour {
	
	public static int cloundNumber;
	
	private Vector3 screenPos;
	private Vector3 offset;
	private float positionZ;
	private float positionY;
	
	private bool isActive;
	private bool isBounding;
	private bool isAnimate;
	private float amoutToMove;
	
	private float cloudFadingOn;
	private float cloundTintAOn;
	
	private bool isLightning = false;
	
	private CS_Cloud cloudScript;
	
	public Camera cam;
	
	public float moveRate;
	public float deathPoint;
	
	public float boundRight;
	public float boundLeft;
	public float correction;
	
	public float cloundLifeRate;
	public float cloudDeath;
	
	public AnimationClip onAnimation;
	public AnimationClip offAnimation;
	
	public Transform cloundLight;
	
	public bool getIsActive () {
		return isActive;
	}
	
	public float getCloundTintAOn () {
		return cloundTintAOn;
	}
	
	public void lightningFire () {
		cloundLifeRate = 2.5F;
		isLightning = true;
		transform.GetChild(0).animation.Play("cloud_light_on_anim", PlayMode.StopAll);
	}
	
	// Use this for initialization
	void Start () {
		cloudScript = GetComponent<CS_Cloud>();
		
		positionZ = transform.position.z;
		positionY = transform.position.y;
	
		cloudFadingOn = cloudScript.Fading;
		cloundTintAOn = cloudScript.Tint.a;
		isActive = false;
		isBounding = false;
	
		cloudScript.Sun = cloundLight;
	//isAnimate = false;
	}
	
	// Update is called once per frame
	void Update () {
		checkBound();
		moveCloud();
		animateClound();
		gameEnding();
	}
	
	void gameEnding () {
		if(isActive && GameManager.gameState == GameManager.GameStates.gameEnding) {
			OnMouseUp();
		}
	}
	
	void OnMouseDown () {
		if(GameManager.gameState == GameManager.GameStates.gameActive) {
			isActive = true;
			isBounding = true;
		    moveDown();
		    cloudOn();
		}
	}
	
	
	void OnMouseDrag () {
		if(GameManager.gameState == GameManager.GameStates.gameActive) {
			moveDrag();
		}
	}
	
	void OnMouseUp () {
		if(isActive && !isLightning) {
			isActive = false;
			isBounding = false;
			cloudOff();
		}
	}
	
	void moveDown () {
		if(isActive) {
			screenPos = cam.WorldToScreenPoint(transform.position);
			offset = transform.position - cam.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z));
		} 
	}
	
	void moveDrag () {
		if(isActive && !isLightning) {
			Vector3 curScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);
	
			Vector3 curPos = cam.ScreenToWorldPoint(curScreenPos) + offset;
			curPos.z = positionZ;
			curPos.y = positionY;
			//print("currentPOS: " + curPos);
			transform.position = curPos;
		}
		
	}
	
	void checkBound () {
		//Debug.Log("transformPOS: " + transform.position);
		if(transform.position.x >= boundRight && isBounding) {
			OnMouseUp();
			//transform.position.x -= correction;
		} else if(transform.position.x <= boundLeft && isBounding) {
			OnMouseUp();
			//transform.position.x += correction;
		} 
	}
	
	void moveCloud () {
		if(!isBounding) {
			amoutToMove = moveRate * Time.deltaTime;
			transform.Translate(Vector3.forward * amoutToMove);
		}
	}
	
	void cloudOn () {
		animation.Play(onAnimation.name);
	}
	
	void cloudOff () {
		animation.Play(offAnimation.name);
	}
	
	void animateClound () {
		//Debug.Log("ANIMATING");
		float aVelocity = 0.0F;
	
		if(animation.isPlaying && isActive) {
			// animating and active
			//Debug.Log("ANIMATING TINT" + cloudScript.Tint.a);
			//Debug.Log("ANIMATING FADING" + cloudScript.Fading);
			//Debug.Log("aVelocity" + aVelocity);
			cloudScript.Fading = Mathf.SmoothDamp(cloudScript.Fading, 1.0F, ref aVelocity, 0.4F);
			//Debug.Log("aVelocity" + aVelocity);
			//cloudScript.Tint.a = Mathf.SmoothDamp(cloudScript.Tint.a, cloundTintAOn, aVelocity, 0.4);
			//Debug.Log("aVelocity" + aVelocity);
		} else if(animation.isPlaying && !isActive) {
			// animating and not active
			//Debug.Log("ANIMATING TINT" + cloudScript.Tint.a);
			//Debug.Log("ANIMATING FADING" + cloudScript.Fading);
			//Debug.Log("aVelocity" + aVelocity);
			cloudScript.Fading = Mathf.SmoothDamp(cloudScript.Fading, cloudFadingOn, ref aVelocity, 0.2F);
			//Debug.Log("aVelocity" + aVelocity);
			//cloudScript.Tint.a = Mathf.SmoothDamp(cloudScript.Tint.a, cloundTintAOn, aVelocity, 0.2);
			//Debug.Log("aVelocity" + aVelocity);
		} else if((!animation.isPlaying && isActive) || isLightning) {
			// not animating and active
			//Debug.Log("FUCK YO SHIT" + cloudScript.Tint.a);
			
			Color cloundTintTemp = cloudScript.Tint;
			cloundTintTemp.a = Mathf.MoveTowards(cloudScript.Tint.a, cloudDeath, cloundLifeRate * Time.deltaTime);
			cloudScript.Tint = cloundTintTemp;
			cloundTintAOn = cloudScript.Tint.a;
	
		}
		
	}
	
}
