using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instanciateController : MonoBehaviour {

	// public variables -------------------
	public float m_interval = 1f;
	public GameObject m_spawnTarget;

	// private variables ------------------
	private float m_timeMeasure;


	// ------------------------------------
	// Use this for initialization
	// ------------------------------------
	void Start () {
		
		
	}
	

	// ------------------------------------
	// Update is called once per frame
	// ------------------------------------
	void Update () {
		// Have a measure of time
		if (!globalVariables.timeStopped)
			m_timeMeasure += Time.deltaTime;

		// Spawn an object at each interval
		if (m_timeMeasure > m_interval)
			spawnObject();
		
	}


	// ------------------------------------
	// Methods
	// ------------------------------------
	private void spawnObject () {
		// Instanciate an object (prefab)
		GameObject newInstance = Instantiate (m_spawnTarget, transform.position, transform.rotation)as GameObject;
		// Make the object a child 
		newInstance.transform.parent = gameObject.transform;
		// Restart the time measure
		m_timeMeasure = 0f;
	}
}
