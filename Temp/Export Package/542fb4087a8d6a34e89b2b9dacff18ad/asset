#pragma strict

public var duration : float = 10.0f;
public var waitDuration : float = 3.0f;
private var m_PassedTime : float;
private var m_Cloud : CS_Cloud;

function OnEnable () {
	m_Cloud = GetComponent.<CS_Cloud> ();
		
	if (m_Cloud != null) {
		StartCoroutine (Fading ());
	} else {
		Debug.LogError ("FadeCloud script is not in a GameObject that contains a CS_Cloud!");
	}
}

function Fading () {
	yield;
	
	while (true) {
		
			// Fade out
		m_PassedTime = 0.0f;
		while (m_PassedTime < duration) {
			m_Cloud.Fading = 1.0f - (m_PassedTime / duration);
			m_PassedTime = m_PassedTime + Time.deltaTime;
			yield;
		}
		m_Cloud.Fading = 0.0f;
		yield (WaitForSeconds (waitDuration));
		
			// Fade in
		m_PassedTime = 0.0f;
		while (m_PassedTime < duration) {
			m_Cloud.Fading = m_PassedTime / duration;
			m_PassedTime = m_PassedTime + Time.deltaTime;
			yield;
		}
		m_Cloud.Fading = 1.0f;
		yield (WaitForSeconds (waitDuration));
	}
}