using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpObjects : MonoBehaviour {

	// public variables -------------------
	public float m_speed; 
	public bool m_canHold;
	[Space (10)]
	public GameObject m_dynamicObject;
	public GameObject m_pickUpObject;
	public Transform guide;


	// ------------------------------------
	// Update is called once per frame
	// ------------------------------------
	void Update () {
		// When the player presses E key 
		if (Input.GetKeyDown(KeyCode.E)) {
           if (!m_canHold)
               throw_drop();
           else 
               Grab();
			   PickUp();
       }
	   
	   // Change position of the dynamic object
       if (!m_canHold && m_dynamicObject)
           m_dynamicObject.transform.position = guide.position;
	   
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
     }


	// Grab dynamic object in front of camera ----------------------
	private void Grab() {

         if (!m_dynamicObject) 
             return;
		 
         // Set the object as a child, deactivate gravity
         m_dynamicObject.transform.SetParent(guide);
         m_dynamicObject.GetComponent<Rigidbody>().useGravity = false;
 
         // Change transform settings (position and rotation)
         m_dynamicObject.transform.localRotation = transform.rotation;
         m_dynamicObject.transform.position = guide.position;
 
		 // Can't hold more than one object 
         m_canHold = false;
     }


	 // Trow dynamic object in grab stance ------------------------
	 private void throw_drop() {

         if (!m_dynamicObject)
             return;
		 
		 // Activate gravity and clear the dynamic object variable
         m_dynamicObject.GetComponent<Rigidbody>().useGravity = true;
         m_dynamicObject = null; 

		 // Add some velocity to the object (trowing) and unparent the dynamic object
         guide.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity = transform.forward * m_speed;
         guide.GetChild(0).parent = null;
		 
		 // Set the player to be able to grab again
         m_canHold = true;
     }


	 // Make an object picked -------------------------------------
	 private void PickUp() {

		if (!m_pickUpObject) 
    		return;
		
		// Deactivate the renderer of the object 
		m_pickUpObject.gameObject.GetComponent<MeshRenderer>().enabled = false;
	 }
}
