using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpManager : MonoBehaviour
{

    // public variables ---------------------
    public bool m_canInteract = true;
    public GameObject m_proximityObject;

    // private variables --------------------
    private Vector3 m_defaultPos;
    private Vector3 m_defaultRot;

    // --------------------------------------
    // Start is called before update
    // --------------------------------------
    void Start()
    {
        // Get the initial pos and rot of the object
        // m_defaultPos = gameObject.transform.localPosition;
        // m_defaultRot = gameObject.transform.localEulerAngles;
    }

    // --------------------------------------
    // Update is called once per frame
    // --------------------------------------
    void Update()
    {
        //if (gameObject.transform.localPosition != m_defaultPos)
        //    gameObject.transform.localPosition = m_defaultPos;

        // Interact with objects (action key)
		if (Input.GetButtonDown("Action") && m_canInteract 
			&& m_proximityObject != null && !globalVariables.timeStopped)
			makeInteraction();

    }

    // --------------------------------------
    // Methods
    // --------------------------------------
    private void makeInteraction()
    {
        // Disable interaction
		m_canInteract = false;

		// Check if its a button
		if (m_proximityObject.tag == "Button")
        {
            m_proximityObject.GetComponent<btnManager>().m_on = true;
            m_canInteract = true;
        }
    }


    private void OnCollisionEnter (Collision col)
    {
        // Catch the object the player is colliding with
        if (col.gameObject.tag == "Button")
            m_proximityObject = col.gameObject;
    }

    private void OnCollisionExit (Collision col)
    {
        // Return null when object is leaving
        if (col.gameObject.tag == "Button")
            m_proximityObject = null;
    }


}
