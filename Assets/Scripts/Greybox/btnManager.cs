using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnManager : MonoBehaviour
{
    // public variables ---------------------
    [Header("Changing Materials")]
    public Material m_red;              // Material when the btn is off
    public Material m_green;            // Material when the btn is on
    [Header("Events")]
    public bool m_on = false;           // State of the btn on/off
    public bool m_doorBtn;              // If the btn serves for a door
    public bool m_canonBtn;             // If the btn serves for a canon
    public bool m_sendSignal;           // Colour rooms sender of signal
    public bool m_catchSignal;          // Colour rooms receiver of signal
    [Header("Special Data")]
    public float m_counterLimit;        // Float for the limit of the counter
    public GameObject m_target;         // Specific target (door, trigger, object)
    public GameObject m_receiver;       // Receiver object to a signal


    // private variables ---------------------
    private float m_timer = 0.0f;       // Timer when btn is activated
    private bool m_oneShot;             // Only do the effect once after activation
    private bool m_fixedBtn = false;    // If the btn is fixed on a state


    // --------------------------------------
    // Start is called before update
    // --------------------------------------
    void Start()
    {
        // Get the btn colour at the start depending if it's on or not
        if (!m_on)
            gameObject.GetComponent<Renderer>().material = m_red;
        else
            gameObject.GetComponent<Renderer>().material = m_green;

        // Set the one shot to false as default
        m_oneShot = false;

        // If the target is a door
        if (m_doorBtn && m_on && m_target != null)
            m_target.SetActive(true);
    }

    // --------------------------------------
    // Update is called once per frame
    // --------------------------------------
    void Update()
    {   
        // Run the timer and effect if btn is activated
        if (m_on && !m_fixedBtn)
        {
            runTimer();

            // check if the effect is a one shot deal
            if (!m_oneShot)
                btnEffect();
        } 
        else if (!m_on && !m_fixedBtn) // if off, reset                      
            Start(); 

        // If the btn is now complete and don't change anymore
        if (m_fixedBtn)
            fixedEffect();
        
    }

    // ------------------------------------
	// Methods
	// ------------------------------------
    private void runTimer() 
    {
        // Get the counter running while in reality
        if (!globalVariables.timeStopped)     
            m_timer += Time.deltaTime;
        
        // Stop the btn effect and reset the timer afterr passsing the limit
        if (m_timer > m_counterLimit)
        {
            m_timer = 0.0f;
            m_on = !m_on;
        }
    }

    private void btnEffect()
    {
        // Change colour of the btn
        gameObject.GetComponent<Renderer>().material = m_green;

        // If the target is a door
        if (m_doorBtn)
            m_target.SetActive(false);

        // If the target is a canon
        if (m_canonBtn && m_target)
        {
            // Only shot once
            m_oneShot = true;
            m_target.GetComponent<canonController>().m_fire = true;
        }

        // If the btn is for sending signal 
        if (m_sendSignal)
        {
            // Get the data of the receiver
            if (m_receiver.GetComponent<btnManager>().m_on)
            {
                // Deactivate the target and change the state of the btn to fixed
                m_target.SetActive(false);
                m_fixedBtn = true;

                // Fixed the btn of the receiver too
                m_receiver.GetComponent<btnManager>().m_fixedBtn = true;
            }
        }
    }

    private void fixedEffect()
    {
        // Change colour of the btn
        gameObject.GetComponent<Renderer>().material = m_green;
    }
}
