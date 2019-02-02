using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class intervalInputs : MonoBehaviour
{
    // public variables ---------------------
    public bool m_canStop;								// If the player can stop time
	public bool m_timeStop;								// State of time (reality/interval)
	[Space(10)]		
	public PostProcessProfile m_intervalProfile;		// Post Processing profile for the Interval
	public PostProcessProfile m_defaultProfile;			// Post Processing profile for Reality
	public GameObject m_proximityObject;
	public bool m_canInteract;							// If the player can interact with env.

	// private variables ------------------
	private PostProcessVolume m_currentProfile;			// Current Post Processing profile in use
	private GameObject m_ppSetting;						// Post Processing setting object
	private float m_counter = 0.0f;						// Counter when in the interval

    // --------------------------------------
    // Start is called before update
    // --------------------------------------
    void Start()
    {
        // Default time is stop
		m_timeStop = false;

		// Get the default ppp profile
		m_ppSetting = GameObject.FindGameObjectWithTag("PostProcessSetting");
		m_currentProfile = m_ppSetting.GetComponent<PostProcessVolume>();
    }

    // --------------------------------------
    // Update is called once per frame
    // --------------------------------------
    void Update()
    {
        // If left btn mouse is  pressed
		if (Input.GetButtonDown("Fire1") && m_canStop) 
        {
			// Invert the state of time
			m_timeStop = !m_timeStop;
			// Refresh the counter 
			m_counter = 0.00f;
		}


		// If time is stopped
		if (m_timeStop) 
        {
		    // Change the post processing profile of the camera
			m_currentProfile.profile = m_intervalProfile;
			// Set the global variable
			globalVariables.timeStopped = m_timeStop;

			// Start a counter
			startCounter();
		} 
		else // If time is normal
        {
			m_currentProfile.profile = m_defaultProfile;
			// Set the global variable
			globalVariables.timeStopped = m_timeStop;
		}
    }


    // ------------------------------------
	// Methods
	// ------------------------------------
    private void startCounter() 
    {
		// Start the counter
		m_counter += Time.deltaTime; 
	}



}
