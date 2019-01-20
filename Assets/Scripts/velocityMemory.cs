using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocityMemory: MonoBehaviour
{

    // private variables --------------------
    private Rigidbody m_rb;
    private Vector3 m_currentVelocity;


    // --------------------------------------
    // Start is called before update
    // --------------------------------------
    void Start()
    {
        // Get the rigidbody
        m_rb = gameObject.GetComponent<Rigidbody>();
        
    }

    // ------------------------------------
    // Methods
    // ------------------------------------
    public void catchVelocity() 
    {
        // Catch current velocity of the object
        if (m_rb != null)
            m_currentVelocity = m_rb.velocity;
    }

    public void returnVelocity() 
    {
        // Return the correct velocity to the object
        if (m_rb != null)
            m_rb.velocity = m_currentVelocity;
    }

}
