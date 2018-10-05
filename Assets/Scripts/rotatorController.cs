using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatorController : MonoBehaviour {

	public float rotSpeed;
	[Header("Colliders (Optionnal")]
	public Collider m_inMovement;
	public Collider m_static;

	// ------------------------------------
	// Update is called once per frame
	// ------------------------------------
	void Update () {
		// Check the state of time
		if (!globalVariables.timeStopped) {
			rotate();

		} else if (m_inMovement != null && m_static != null) {
			m_inMovement.enabled = false;
			m_static.enabled = true;
		}

	}


	// ------------------------------------
	// Methods
	// ------------------------------------
	private void rotate() {
		// Rotate movement
		transform.Rotate(Vector3.right * rotSpeed);

		if(m_inMovement != null && m_static != null) {
			// Set the correct collider
			m_inMovement.enabled = true;
			m_static.enabled = false;
		}
	}
}
