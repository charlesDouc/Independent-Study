using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canonController : MonoBehaviour
{
    // public variables ---------------------
    public GameObject m_projectile;             // Prefab object that will serve as the projectile
    public float m_launchForce = 15f;           // Launch force from the canon 
    public bool m_fire;                         // Fire or not the canon

    // private variables ---------------------

    

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


}
