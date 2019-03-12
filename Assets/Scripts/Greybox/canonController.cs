using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canonController : MonoBehaviour
{
    // public variables ---------------------
    public GameObject m_projectile;             // Prefab object that will serve as the projectile
    public float m_launchForce = 15f;           // Launch force from the canon 
    public bool m_fire;                         // Fire or not the canon
    public bool m_fireTimer;                    // Fire based on a timer
    public float m_fireInterval;                // If it's a timer canon, what's the time interval

    // private variables ---------------------
    private float m_counter = 0.0f;                      // Time counter for the canon

    

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
        // Check if the canon can fire
        if (m_fire && !globalVariables.timeStopped)
            fire();

        // Check if the canon reacts to a timer
        if (m_fireTimer && !globalVariables.timeStopped)
            startCanonCounter();
        
    }


    // ------------------------------------
	// Methods
	// ------------------------------------
    public void fire() 
    {
        // Instanciate a projectile from the prefab gameObject
        GameObject firedObject = Instantiate(m_projectile, transform.position, transform.rotation);
        
        // Get the rigidbody and add a launch force to it
        Rigidbody objectRB = firedObject.GetComponent<Rigidbody>();
        objectRB.velocity = transform.forward * m_launchForce;

        // Only fire once
        m_fire = false;
    }


    private void startCanonCounter() 
    {
        // Count time
        m_counter += Time.deltaTime;

        // If the counter reach the timer interval
        if (m_counter > m_fireInterval)
        {
            // Fire the canon and restart the counter
            m_fire = true;
            m_counter = 0.0f;
        }
    }


}
