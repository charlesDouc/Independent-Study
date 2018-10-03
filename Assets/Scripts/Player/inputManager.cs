using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class inputManager : MonoBehaviour {
	
	// public variables -------------------
	public bool m_canStop;
	public bool m_timeStop;
	public float m_counter = 0.0f;
	[Space(10)]
	public PostProcessProfile m_stopTimeProfile;
	public PostProcessProfile m_defaultProfile;

	// private variables ------------------
	private PostProcessVolume m_currentProfile;
	private GameObject m_ppSetting;

	// ------------------------------------
	// Use this for initialization
	// ------------------------------------
	void Start () {
		// Default time is stop
		m_timeStop = false;

		// Get the default ppp profile
		m_ppSetting = GameObject.FindGameObjectWithTag("PostProcessSetting");
		m_currentProfile = m_ppSetting.GetComponent<PostProcessVolume>();
	}
	

	// ------------------------------------
	// Update is called once per frame
	// ------------------------------------
	void Update () {
		// If left btn mouse is  pressed
		if (Input.GetMouseButtonDown(0) && m_canStop) {
			// Invert the state of time
			m_timeStop = !m_timeStop;

			// Refresh the counter
			refreshCounter();
		}


		// If time is stopped
		if (m_timeStop) {
				// Change the post processing profile of the camera
				m_currentProfile.profile = m_stopTimeProfile;
				//Debug.Log(m_currentProfile);

				// Set the global variable
				globalVariables.timeStopped = m_timeStop;

				// Start a counter
				startCounter();
		// If time is normal
		} else {
				// If time is not stopped
				m_currentProfile.profile = m_defaultProfile;
				// Set the global variable
				globalVariables.timeStopped = m_timeStop;
				//Debug.Log(m_currentProfile);
		}	
	}


	// ------------------------------------
	// Methods
	// ------------------------------------
	public void startCounter() {
		// Start the counter when the time is stopped
		m_counter += Time.deltaTime; 
		// Debug.Log("Time counting : " + Mathf.Ceil(m_counter));
	}

	public void refreshCounter() {
		// Refresh the counter when called
		m_counter = 0.00f;
	}
}
