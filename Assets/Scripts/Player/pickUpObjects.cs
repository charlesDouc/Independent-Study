using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;


public class pickUpObjects : MonoBehaviour {

	// public variables -------------------
	public float m_speed; 
	public bool m_canHold;
	[Space (10)]
	public GameObject m_dynamicObject;
	public GameObject m_pickUpObject;
	public GameObject m_buttonObject;
	public Transform guide;

	// private variables ------------------
	private GameObject m_player;

	
	// ------------------------------------
	// Use this for initialization
	// ------------------------------------
	void Start () {
		m_player = GameObject.FindGameObjectWithTag("Player");
	}

	// ------------------------------------
	// Update is called once per frame
	// ------------------------------------
	void Update () {
		// When the player presses E key 
		if (Input.GetKeyDown(KeyCode.E)) {
           if (!m_canHold) {
               throw_drop();
		   } else {
               Grab();
			   PickUp();
			   pushButton();
		   }
       }

	   
	   // Change position of the dynamic object and keep velocity 0
       if (!m_canHold && m_dynamicObject) {
           m_dynamicObject.transform.position = guide.position;
	   }

	   // Enable 3D view if right click is down
	   if (Input.GetMouseButton(1) && m_dynamicObject && !m_canHold)  {
		 	moveObject();
			lockCamera(true);
	   } else
	   		lockCamera(false);
	}


	// ------------------------------------
	// Methods
	// ------------------------------------
	 // When objects collide with trigger ----------------------------
     void OnTriggerEnter(Collider col) {
         if (col.gameObject.tag == "DynamicObject") {
			 // Make sure player has nothing
             if (!m_dynamicObject) 
                 m_dynamicObject = col.gameObject;
		 }

		 if (col.gameObject.tag == "PickUpObject") {
			 // Make sure player has nothing
             if (!m_pickUpObject) 
                 m_pickUpObject = col.gameObject;
		 }

		 if (col.gameObject.tag == "Button") {
			 // Make sure player has nothing
             if (!m_pickUpObject) 
                 m_buttonObject = col.gameObject;
		 }

     }
 

     // When objects exit the trigger -------------------------------
     void OnTriggerExit(Collider col) {
         if (col.gameObject.tag == "DynamicObject") {
			 // Make sure player has nothing
             if (m_canHold)
                 m_dynamicObject = null;
         }

		 if (col.gameObject.tag == "PickUpObject") {
			 // Make sure player has nothing
             if (m_canHold)
                 m_pickUpObject = null;
         }

		 if (col.gameObject.tag == "Button") {
			 // Make sure player has nothing
             if (m_canHold)
                 m_buttonObject = null;
         }
     }


	// Grab dynamic object in front of camera ----------------------
	private void Grab() {

         if (!m_dynamicObject) 
             return;

		 // Make sure the player is in normal time
		 if(globalVariables.timeStopped) 
			 return;
		 
		 // Set the object kinematic
	 	 m_dynamicObject.GetComponent<Rigidbody>().isKinematic = true;

         // Set the object as a child, deactivate gravity
         m_dynamicObject.transform.SetParent(guide);
         m_dynamicObject.GetComponent<Rigidbody>().useGravity = false;
 
         // Change transform settings (position and rotation)
         m_dynamicObject.transform.localRotation = transform.rotation;
         m_dynamicObject.transform.position = guide.position;
	
		 
		 // Can't hold more than one object 
         m_canHold = false;
		 // Can't stop time while grabing
		 m_player.GetComponent<inputManager>().m_canStop = false;
     }


	 // Trow dynamic object in grab stance ------------------------
	 private void throw_drop() {

         if (!m_dynamicObject)
             return;
		 
		 // Activate gravity and clear the dynamic object variable
         m_dynamicObject.GetComponent<Rigidbody>().useGravity = true;
		 m_dynamicObject.GetComponent<Rigidbody>().isKinematic = false;
         m_dynamicObject = null; 

		 // Add some velocity to the object (trowing) and unparent the dynamic object
         guide.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity = transform.forward * m_speed;
         guide.GetChild(0).parent = null;
		 
		 // Set the player to be able to grab again
         m_canHold = true;
		 // Can stop time if no object in hand
		 m_player.GetComponent<inputManager>().m_canStop = true;
     }


	 // Make an object picked -------------------------------------
	 private void PickUp() {

		if (!m_pickUpObject) 
    		return;
		
		// Make sure the player is in normal time
		if(globalVariables.timeStopped) 
			 return;
		
		// Deactivate the renderer of the object 
		m_pickUpObject.gameObject.GetComponent<MeshRenderer>().enabled = false;
	 }


	 // Make an object picked -------------------------------------
	 private void pushButton() {

		if (!m_buttonObject) 
    		return;
		
		// Make sure the player is in normal time
		if(globalVariables.timeStopped) 
			 return;

		// Get the value of the state of the button
		bool buttonState = m_buttonObject.gameObject.GetComponent<buttonManager>().m_isActive;

		// Turn the button state on (one time)
		if (!buttonState) {
			buttonState = true;
			// Update the state
			m_buttonObject.gameObject.GetComponent<buttonManager>().m_isActive = buttonState;
		} else
			return; 

	 }


	 // Move the object in 3D when an object is grabed ------------
	 private void moveObject() {
		 float rotSpeed = 5f;

		 m_dynamicObject.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * rotSpeed, Space.World);
		 m_dynamicObject.transform.Rotate(Vector3.right, Input.GetAxis("Mouse Y") * rotSpeed, Space.World);

	 }


	 // Make an object picked -------------------------------------
	 private void lockCamera(bool state) {
		 m_player.GetComponent<FirstPersonController>().m_canRotate = !state;
	 }
}
