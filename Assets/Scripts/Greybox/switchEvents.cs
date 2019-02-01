using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchEvents : MonoBehaviour
{
    // public variables ---------------------
    public GameObject m_targetObject;           // The target object on which the switch event will take place
    public float m_stepInterval;                // How much time between each switch 
    public bool m_activateCounter = true;       // Activate the counter/the switch cycle
    [Header("Switch Event Types")]
    public bool m_hideShow;                     // Hide and show an object - event type

    // private variables ---------------------
    private float m_counter = 0.0f;             // The counter variable
    private bool m_fireEvent;                   // Activate or deactivate the event effect


    // --------------------------------------
    // Start is called before update
    // --------------------------------------
    void Start()
    {
        
    }

    // --------------------------------------
    // Update is called once per frame
    // --------------------------------------
    void Update()
    {
        // Make sure time is not stopped and that timer should be running
        if (!globalVariables.timeStopped && m_activateCounter)
            stepCount();


        // When the event is fired
        if (m_fireEvent)
        {
            // Set the fire to false and scan all possible events
            m_fireEvent = false;
            scanEvents();
        }
    }


    // ------------------------------------
	// Methods
	// ------------------------------------
    private void stepCount() 
    {
        // Start the counter
        if (m_counter < m_stepInterval) 
            m_counter += Time.deltaTime;
        else
        {
            // Reset the counter once the step is reached, then fire the event
            m_counter = 0.0f;
            m_fireEvent = true;
        }
    }


    private void scanEvents() 
    {
        // If the event is show hide an object
        if (m_hideShow)
            hideShowEvent();
    }


    private void hideShowEvent() 
    {
        bool currentState = m_targetObject.activeSelf;

        // Set the object unactive or active if the event is fire
        m_targetObject.SetActive(!currentState);
    }

}
