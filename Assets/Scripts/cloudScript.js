#pragma strict

#pragma strict

static var cloundNumber : int;

internal var screenPos : Vector3;
internal var offset : Vector3;
internal var positionZ : float;
internal var positionY : float;

internal var isActive : boolean;
internal var isBounding : boolean;
internal var isAlive : boolean;
internal var isAnimate : boolean;
internal var amoutToMove : float;

internal var cloudFadingOn : float;
internal var cloundTintAOn : float;


internal var cloudScript : CS_Cloud;

var cam : Camera;
//var darkShadow : GameObject;

//var positionY : float;
//var spaceY : float;
var moveRate : float;
var deathPoint : float;

var boundRight : float;
var boundLeft : float;
var correction : float;

var cloundLifeRate : float;
var cloudDeath : float;

var onAnimation : AnimationClip;
var offAnimation : AnimationClip;

var cloundLight : Transform;

function Start () {
	cloudScript = GetComponent (CS_Cloud);
	
	positionZ = transform.position.z;
	positionY = transform.position.y;

	cloudFadingOn = cloudScript.Fading;
	cloundTintAOn = cloudScript.Tint.a;
	isActive = false;
	isBounding = false;
	isAlive = true;

	//isAnimate = false;

}

function Update () {
	checkBound();
	moveCloud();
	animateClound();
	//killCloud();

	//print(cloudScript.Tint);
	//print(cloudScript.Tint);	
	//print(cloudScript.Tint);

}

function OnMouseDown () {
	isActive = true;
	isBounding = true;
    moveDown();
    cloudOn();
}


function OnMouseDrag () {
	moveDrag();
}

function OnMouseUp () {
	if(isActive) {
		cloundTintAOn = cloudScript.Tint.a;
		isActive = false;
		isBounding = false;
		cloudOff();
	}
}

// starts position moves calculations
function moveDown () {
	if(isActive) {
		screenPos = cam.WorldToScreenPoint (transform.position);
    	offset = transform.position - cam.ScreenToWorldPoint(Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z));
	} 	
}
// position moves calculations
function moveDrag () {
	if(isActive) {
		var curScreenPos : Vector3 = Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);

		var curPos : Vector3 = cam.ScreenToWorldPoint(curScreenPos) + offset;
		curPos.z = positionZ;
		curPos.y = positionY;
		//print("currentPOS: " + curPos);
		transform.position = curPos;
	}
}

// check if object is in screen bounds
function checkBound () {
	//print("transformPOS: " + transform.position);
	if(transform.position.x >= boundRight && isBounding) {
		OnMouseUp();
		//transform.position.x -= correction;
	} else if(transform.position.x <= boundLeft && isBounding) {
		OnMouseUp();
		//transform.position.x += correction;
	} 
}

// move clouds
function moveCloud () {
	if(!isBounding) {
		amoutToMove = moveRate * Time.deltaTime;
		transform.Translate(Vector3.forward * amoutToMove);
	}
}

// turn on cloud 
function cloudOn () {
	//print("ANIMATING");
	animation.Play(onAnimation.name);
}

// turn off cloud 
function cloudOff () {
	//transform.localScale = Vector3(1, 0, 1);
	animation.Play(offAnimation.name);
}
/*
function killCloud () {
	//print("FUCK YO SHIT" + cloudScript.Tint.a);
	if(cloudScript.Tint.a <= deathPoint){
		isActive = false;
		Destroy(gameObject);
	}
}
*/
function animateClound () {
	//print("ANIMATING");
	var aVelocity : float = 0.0;
	if(isAlive){
		if(animation.isPlaying && isActive) {
			// animating and active
			//print("ANIMATING TINT" + cloudScript.Tint.a);
			//print("ANIMATING FADING" + cloudScript.Fading);
			//print("aVelocity" + aVelocity);
			cloudScript.Fading = Mathf.SmoothDamp(cloudScript.Fading, 1.0, aVelocity, 0.4);
			//print("aVelocity" + aVelocity);
			//cloudScript.Tint.a = Mathf.SmoothDamp(cloudScript.Tint.a, cloundTintAOn, aVelocity, 0.4);
			//print("aVelocity" + aVelocity);
		} else if(animation.isPlaying && !isActive) {
			// animating and not active
			//print("ANIMATING TINT" + cloudScript.Tint.a);
			//print("ANIMATING FADING" + cloudScript.Fading);
			//print("aVelocity" + aVelocity);
			cloudScript.Fading = Mathf.SmoothDamp(cloudScript.Fading, cloudFadingOn, aVelocity, 0.2);
			//print("aVelocity" + aVelocity);
			//cloudScript.Tint.a = Mathf.SmoothDamp(cloudScript.Tint.a, cloundTintAOn, aVelocity, 0.2);
			//print("aVelocity" + aVelocity);
		} else if(!animation.isPlaying && isActive) {
			// not animating and active
			//print("FUCK YO SHIT" + cloudScript.Tint.a);
			cloudScript.Tint.a = Mathf.MoveTowards(cloudScript.Tint.a, cloudDeath, cloundLifeRate * Time.deltaTime);


		}
	}


}
