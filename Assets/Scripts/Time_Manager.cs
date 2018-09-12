using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Time_Manager : MonoBehaviour {
	
	// public variables -------------------
	public bool m_timeStop;
	public PostProcessingProfile m_stopTimeProfile;
	public PostProcessingProfile m_defaultProfile;

	// private variables ------------------
	private PostProcessingBehaviour m_currentProfile;
	private GameObject m_mainCam;

	// ------------------------------------
	// Use this for initialization
	// ------------------------------------
	void Start () {
		// Default time is stop
		m_timeStop = false;

		// Get the default ppp profile
		m_mainCam = GameObject.FindGameObjectWithTag("MainCamera");
		m_currentProfile = m_mainCam.GetComponent<PostProcessingBehaviour>();
	}
	

	// ------------------------------------
	// Update is called once per frame
	// ------------------------------------
	void Update () {
		// If left btn mouse is  pressed
		if (Input.GetMouseButtonDown(0)) {
			// Invert the state of time
			m_timeStop = !m_timeStop;

			// Change the post processing profile of the camera
			if (m_timeStop) {
				// If time is stopped
				m_currentProfile.profile = m_stopTimeProfile;
				Debug.Log(m_currentProfile);
			} else {
				// If time is not stopped
				m_currentProfile.profile = m_defaultProfile;
				Debug.Log(m_currentProfile);
			}
		}		
	}
}
