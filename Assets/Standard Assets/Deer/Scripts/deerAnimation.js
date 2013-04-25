#pragma strict

var onAnimation : AnimationClip;


function Start () {
	animation.AddClip(onAnimation, "walk_1");
}

function Update () {
	animation.Play(onAnimation.name);
}