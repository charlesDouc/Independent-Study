using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_Manager : MonoBehaviour {
	
	// public variables -------------------
	public bool timeStop;


	// private variables ------------------
	
	
	// ------------------------------------
	// Use this for initialization
	// ------------------------------------
	void Start () {
		// Default time is stop
		timeStop = false;
	
	}
	
	// ------------------------------------
	// Update is called once per frame
	// ------------------------------------
	void Update () {
		// If left btn mouse is  pressed
		if (Input.GetMouseButton(0)) {
			// Invert the state of time
			timeStop = !timeStop;
		}


		
	}
}
