using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerController : MonoBehaviour {

	// public variables -------------------
	[Header("Trigger Type")]
	public bool m_respaw;


	// private variables ------------------


	// ------------------------------------
	// Use this for initialization
	// ------------------------------------
	void Start () {
		
	}
	
	// ------------------------------------
	// Update is called once per frame
	// ------------------------------------
	void Update () {
		
	}


	// ------------------------------------
	// Methods
	// ------------------------------------
	// Check what object enters the trigger ---------------------
	void OnTriggerEnter(Collider col) {
		
		// If it's for respawn objects
		if(m_respaw && col.gameObject.tag == "DynamicObject" || col.gameObject.tag == "Player")
			respawnObject(col.gameObject);
			
		
	}

	// For respawning objects -----------------------------------
	private void respawnObject(GameObject target) {
		// check if it's valid
		if (!target) 
			return;
		
		// Initiate respawn process
		target.GetComponent<respawnController>().m_relocate = true;
	}
}
