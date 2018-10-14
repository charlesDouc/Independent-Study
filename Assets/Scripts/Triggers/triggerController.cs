using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class triggerController : MonoBehaviour {

	// public variables -------------------
	[Header("Trigger Type")]
	public bool m_respaw;
	public bool m_checkpoint;
	public bool m_killObject;
	public bool m_restart;


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
		if(m_respaw && col.gameObject.tag == "DynamicObject" || col.gameObject.tag == "Player" && m_respaw)
			respawnObject(col.gameObject);

		// If it's for respawn objects
		if(m_checkpoint && col.gameObject.tag == "Player")
			changeCheckpoint(col.gameObject);

		// If it's destroying object
		if(m_killObject && col.gameObject.tag == "DynamicObject")
			killObject(col.gameObject);

		// If it's restarting the game
		if(m_restart && col.gameObject.tag == "Player")
			restartScene(col.gameObject);
			
		
	}

	// For respawning objects -----------------------------------
	private void respawnObject(GameObject target) {
		// check if it's valid
		if (!target) 
			return;
		
		// Initiate respawn process
		target.GetComponent<respawnController>().m_relocate = true;
	}

	// For checkpoint index -----------------------------------
	private void changeCheckpoint(GameObject target) {
		// check if it's valid
		if (!target) 
			return;
		
		// Change index
		target.GetComponent<respawnController>().m_postionIndex ++;
		// Destroy trigger
		Destroy(gameObject);
	}


	// For killing specific object -----------------------------------
	private void killObject(GameObject target) {
		// check if it's valid
		if (!target) 
			return;
		
		// Destroy the target object
		Destroy(target);
	}


	/* --------------------TO BE DESTROYED AFTER PROTOTYPE SCENE COMPLETE ---------------------------  */
	// For Restarting the game -----------------------------------
	private void restartScene(GameObject target) {
		// check if it's valid
		if (!target) 
			return;

		// Reload the scene
		SceneManager.LoadScene(0);
	}
	/* ---------------------------------------------------------------------------------------------  */
}

