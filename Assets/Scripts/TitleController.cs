using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour {
	
	
	public bool isActive = false;
	private bool checkClick = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown () {
		checkClick = true;
	}
	void OnMouseUp () {
		if(checkClick) {
			//Debug.Log("MOUSE CLICK"); 
			isActive = true;
			checkClick = false;
		}
	}
	
	
}
