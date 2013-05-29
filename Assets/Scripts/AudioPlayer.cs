using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour {
	
	public AudioClip clip01;
	public AudioClip clip02;
	public AudioClip clip03;
	
	public int playState = 0;
	
	
	public void setPlayState (int state) {
		if(playState == state) {
			return;
		} else {
			playState = state;
			stopAudio();
		}
		
		switch(playState) {
		case 0:
			break;
		case 1:
			if(clip01) {
				audio.clip = clip01;
				audio.Play();	
			}
			break;
		case 2:
			if(clip02) {
				audio.clip = clip02;
				audio.Play();	
			}
			break;
		case 3:
			if(clip03) {
				audio.clip = clip03;
				audio.Play();	
			}
			break;
		default:
			break;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void stopAudio () {
		if(audio.isPlaying) {
			audio.Stop();
		}
	}
	
}
