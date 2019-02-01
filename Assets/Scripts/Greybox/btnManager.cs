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
    [Header("Special Data")]
    public float m_counterLimit;        // Float for the limit of the counter
    public GameObject m_target;         // Specific target (door, trigger, object)


    // private variables ---------------------
    private float m_timer = 0.0f;       // Timer when btn is activated
    private bool m_oneShot;             // Only do the effect once after activation


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
        if (m_doorBtn && m_target != null)
            m_target.SetActive(true);
    }

    // --------------------------------------
    // Update is called once per frame
    // --------------------------------------
    void Update()
    {   
        // Run the timer and effect if btn is activated
        if (m_on)
        {
            runTimer();

            // check if the effect is a one shot deal
            if (!m_oneShot)
                btnEffect();
        } 
        else if (!m_on) // if off, reset                      
            Start(); 
        
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
        if (m_doorBtn && m_target)
            m_target.SetActive(false);

        // If the target is a canon
        if (m_canonBtn && m_target)
        {
            // Only shot once
            m_oneShot = true;
            m_target.GetComponent<canonController>().m_fire = true;
        }
    }
}
