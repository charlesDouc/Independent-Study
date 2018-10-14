using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnController : MonoBehaviour {

	// public variables -------------------
	public bool m_fromStartPosition = true;
	public Transform[] m_respawnPosition;
	[HideInInspector]public int m_postionIndex;
	[HideInInspector]public bool m_relocate;


	// private variables ------------------
	private Vector3 m_startPos;
	private Vector3 m_startRot;

	// ------------------------------------
	// Use this for initialization
	// ------------------------------------
	void Start () {
		// Get the start position of the object
		m_startPos = gameObject.transform.position;
		m_startRot = gameObject.transform.localEulerAngles;

		m_relocate = false;
		
		//Use for checkpoints
		m_postionIndex = 0;
	}
	

	// ------------------------------------
	// Update is called once per frame
	// ------------------------------------
	void Update () {
		// If the object needs to be relocated
		if (m_relocate)
			respawn();
		
	}


	// ------------------------------------
	// Methods
	// ------------------------------------
	private void respawn() {
		// Check the method for respawn
		if(m_fromStartPosition) {
			// Relocate object to start position
			gameObject.transform.position = m_startPos;
			gameObject.transform.localEulerAngles = m_startRot;

		} else if (m_respawnPosition[m_postionIndex] != null) {
			// Relocate object to checkpoint locations
			gameObject.transform.position = m_respawnPosition[m_postionIndex].position;
			gameObject.transform.localEulerAngles = m_respawnPosition[m_postionIndex].localEulerAngles;
		}

		// Clear physics and rotation
		gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

		Debug.Log("Respawned an object");
		// Disable respawn
		m_relocate = false;
	}
}
