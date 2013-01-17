#pragma strict

var onAnimation : AnimationClip;
var offAnimation : AnimationClip;

function Start () {

}

function Update () {

}

function beamOn () {
	animation.Play(onAnimation.name);
}

function beamOff () {
	//transform.localScale = Vector3(1, 0, 1);
	animation.Play(offAnimation.name);
}