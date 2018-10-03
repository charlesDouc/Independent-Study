using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalTime : MonoBehaviour {

	// public variables -------------------
	[Header("Global time of the scene (12h model)")]
	public float hour;
	public float minutes;
	public float seconds;
	public bool PM;
	[HideInInspector]public float hourAngle;
	[HideInInspector]public float minuteAngle;
	[HideInInspector]public float secondAngle;

	// private variables ------------------
	private bool m_restartMotion;
	
	
	// ------------------------------------
	// Use this for initialization
	// ------------------------------------
	void Start () {
	

	}
	

	// ------------------------------------
	// Update is called once per frame
	// ------------------------------------
	void Update () {
		// If the time is not stopped, calculate time
		// Otherwise, stop motion in the scene
		if (!globalVariables.timeStopped) 
			currentTime();
		else if (!m_restartMotion)
			stopMotion();

		// If time just got back working
		if (m_restartMotion && !globalVariables.timeStopped)
			restartMotion();
	}


	// ------------------------------------
	// Methods
	// ------------------------------------
	// Calculate Time current time in the scene ---------------------
	public void currentTime() {
		// Count time as it pass.
		seconds += Time.deltaTime;

		// Add a minute, restart seconds each 60 secs
		if (seconds > 60f) {
			minutes ++;
			seconds = 0f;
		}
		// Add an hour, restat minutes
		if (minutes > 59f) {
			hour ++;
			minutes = 0f;
		}
		// Restart clock at 24
		if (hour > 11f) {
			hour = 0f;
			// Invert AM PM
			PM = !PM;
		}

		// For Clocks (angles on a 360 degree)
		hourAngle = ((seconds/60f + minutes)/60f + hour) * 30f;
		minuteAngle = (seconds/60f + minutes) * 6f;
		secondAngle = seconds * 6f;
	}


	// Stop motion of all objects in the scene ----------------------
	public void stopMotion() {
		// Get all dynamic (can be grabbed) objects in the scene
		GameObject[] gravityObjects;
		gravityObjects = GameObject.FindGameObjectsWithTag("DynamicObject");

		// For each object, disable gravity (so they stop motion)
		foreach (GameObject gravityObject in gravityObjects) {
			// Remember current Velocity of the moving object
			gravityObject.GetComponent<velocityMemory>().catchVelocity();
			gravityObject.GetComponent<Rigidbody>().isKinematic = true;
		}

		// Motion will need to restart 
		m_restartMotion = true;
	}


	// Restart motion of all objects in the scene -------------------
	public void restartMotion() {
		// Get all dynamic (can be grabbed) objects in the scene
		GameObject[] gravityObjects;
		gravityObjects = GameObject.FindGameObjectsWithTag("DynamicObject");

		// For each object, disable gravity (so they stop motion)
		foreach (GameObject gravityObject in gravityObjects) {
			gravityObject.GetComponent<Rigidbody>().isKinematic = false;
			// Return the correct velocity to the object
			gravityObject.GetComponent<velocityMemory>().returnVelocity();
		}

		// Once everything is done
		m_restartMotion = false;
	}

}
