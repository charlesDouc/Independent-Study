using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class pickUpManager : MonoBehaviour
{
    // public variables ---------------------
    public bool m_canInteract = true;           // Allow interaction with objects
    public GameObject m_proximityObject;        // Target catched by a trigger
    public GameObject m_actionIcon;             // Action Icon displaying on the ui
    public float m_trowForce = 2.5f;            // Force when an object is trown after holding

    // private variables --------------------
    private BoxCollider m_boxCol;               // Collider with the trigger
    private bool m_holding = false;             // State of if holding an object
    private GameObject m_player;                // Player object
    private float m_originalDrag;               // Original drag before holding of an object
    private float m_originalAngDrag;            // Original angularDrag before holding of an object


    // --------------------------------------
    // Start is called before update
    // --------------------------------------
    void Start()
    {
        // Get the box collider of gameObject
        m_boxCol = gameObject.GetComponent<BoxCollider>();

        // Get the player g.o.
        m_player = GameObject.FindGameObjectWithTag("Player");
    }


    // --------------------------------------
    // Update is called once per frame
    // --------------------------------------
    void Update()
    {
        // Interact with objects (action key)
		if (Input.GetButtonDown("Action") && m_canInteract && m_proximityObject && !globalVariables.timeStopped)
        {
            makeInteraction();
        }
        else if (Input.GetButtonDown("Action") && m_holding)
        {
            // If holding an object
            trow();
        }
        

        // Make a little animation when hovering interactible objects
        if (m_proximityObject && m_canInteract && !globalVariables.timeStopped) 
            m_actionIcon.GetComponent<actionIconController>().m_hover = true;
        else 
            m_actionIcon.GetComponent<actionIconController>().m_hover = false;



        // Enable 3D view if right click is down
	   if (Input.GetButton("Fire2") && m_holding)  {
		 	moveObject();
			lockCamera(true);
	   } else
	   		lockCamera(false);
    }


    void FixedUpdate() 
    {
        // While holding
        if (m_holding)
        {
            // Keep the object stick on original point and follow other colliders
            float step = 5f * Time.deltaTime;
            m_proximityObject.transform.localPosition = Vector3.MoveTowards(m_proximityObject.transform.localPosition, transform.localPosition, step);
        }

    }


    // --------------------------------------
    // Methods
    // --------------------------------------
    // Fire an interaction with a specific  object ----------------------------------
    private void makeInteraction()
    {
        // Disable interaction
		m_canInteract = !m_canInteract;


		// Check if its a button
		if (m_proximityObject.tag == "Button")
        {
            // Activate the button and return true for interaction
            m_proximityObject.GetComponent<btnManager>().m_on = true;
            m_canInteract = true;
        }


        // If its a holdable object
        if (m_proximityObject.tag == "DynamicObject")
            hold();
    }



    // Sccan an object when colliding with it --------------------------------------
    private void OnTriggerEnter (Collider col)
    {
        // Catch the object the player is colliding with
        if (col.gameObject.tag == "Button") 
            m_proximityObject = col.gameObject;


        if (col.gameObject.tag == "DynamicObject")
        {
            // Make sure the dynamic object can be held
            if (col.GetComponent<velocityMemory>().m_canBeHold)
                m_proximityObject = col.gameObject;
        }

        
    }

    private void OnTriggerExit (Collider col)
    {
        // Return null when object is leaving
        if (col.gameObject.tag == "Button") 
            m_proximityObject = null;
        

        if (col.gameObject.tag == "DynamicObject")
        {
            // Make sure the dynamic object can be held
            if (col.GetComponent<velocityMemory>().m_canBeHold)
                m_proximityObject = null;
        }
    }



    // Holding an object in front of the player --------------------------------------
    private void hold()
    {
        // Make sure the object can be taken
        if (!m_proximityObject.GetComponent<velocityMemory>().m_canBeHold)
        {
            m_proximityObject = null;
            m_canInteract = true;
            return;
        }

        // Set the obbject as a child
        m_proximityObject.transform.SetParent(transform);

        // Get the rigidbody and capture its settings
        Rigidbody m_objectRB = m_proximityObject.GetComponent<Rigidbody>();
        m_originalDrag = m_objectRB.drag;
        m_originalAngDrag = m_objectRB.angularDrag;

        // Set its gravity to false while holding and change drag values
        m_objectRB.useGravity = false;
        m_objectRB.drag = 65f;
        m_objectRB.angularDrag = 65f;

        // Set the same position as our object area
        m_proximityObject.transform.position = transform.position;

        // Set off the box collider
        m_boxCol.enabled = false;

        //Currently holding an object
        m_holding = true;

        // Can't stop time while grabing
		 m_player.GetComponent<intervalInputs>().m_canStop = false;
    }



    // Trowing an object in fron of the player --------------------------------------
    private void trow() 
    {
        // Reset object rigidbody values
        Rigidbody m_objectRB = m_proximityObject.GetComponent<Rigidbody>();
        m_objectRB.drag = m_originalDrag;
        m_objectRB.angularDrag = m_originalAngDrag;
        m_objectRB.useGravity = true;

        // Apply velocity to the object
        m_objectRB.velocity = transform.forward * m_trowForce;
        
        // Get rid of the object
        m_proximityObject.transform.parent = null;
        m_proximityObject = null;

        // Reable interaction
        m_canInteract = true;
        m_holding = false;

        // Set on the box collider
        m_boxCol.enabled = true;

        // Can stop time while grabing
		 m_player.GetComponent<intervalInputs>().m_canStop = true;
    }



    // Move an object in 3D when an object is grabed ------------------------------
	 private void moveObject() {
		 float rotSpeed = 0.05f;
		 float mouseDirectionX = Input.GetAxis("Mouse X") * rotSpeed;
		 float mouseDirectionY = Input.GetAxis("Mouse Y") * rotSpeed;

		 m_proximityObject.transform.RotateAround(Vector3.up, -mouseDirectionX);
         m_proximityObject.transform.RotateAround(Vector3.right, -mouseDirectionY);
	 }


    // Lock camera while in 3D move mode  -----------------------------------------
	 private void lockCamera(bool state) {
		 m_player.GetComponent<FirstPersonController>().m_camRot = !state;
	 }
}
