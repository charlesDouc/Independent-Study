using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorsController : MonoBehaviour {

	// public variables -------------------
	public Transform m_leftDoor;
	public Transform m_rightDoor;
	[Space(10)]
	public float speed = 10f;
	public Vector3 m_leftDestination;
	public Vector3 m_rightDestination;
	public bool m_opening;
	[HideInInspector]public bool m_closing;

	// private variables ------------------
	private Vector3 m_startLeftPos;
	private Vector3 m_startRightPos;

	// ------------------------------------
	// Use this for initialization
	// ------------------------------------
	void Start () {
		m_opening = false;
		m_closing = false;

		// Catch current position of door
		m_startLeftPos = m_leftDoor.position;
		m_startRightPos = m_rightDoor.position;
	}
	

	// ------------------------------------
	// Update is called once per frame
	// ------------------------------------
	void Update () {

		// If the doors need to be opened
		if (m_opening)
			Open();

		// If the doors need to be closed
		if (m_closing)
			Close();
	}


	// ------------------------------------
	// Methods
	// ------------------------------------
	private void Open() {
		// Check time state
		if (!globalVariables.timeStopped) {
			// Make movement
			m_leftDoor.position = Vector3.Lerp(m_startRightPos, m_leftDestination, speed);
			m_rightDoor.position = Vector3.Lerp(m_startRightPos, m_rightDestination, speed);
		}
	}

	private void Close() {

	}
	
}
