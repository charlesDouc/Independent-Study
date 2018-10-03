using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonManager : MonoBehaviour {

	// public variables -------------------
	[HideInInspector]public bool m_isActive;
	public Material m_default;
	public Material m_active;
	public Material m_red;
	public GameObject m_door;
	public GameObject m_colorBar;
	public float m_counterLimit = 5f;

	// private variables -------------------
	private Material m_material;
	private float m_counter;

	// ------------------------------------
	// Use this for initialization
	// ------------------------------------
	void Start () {
		m_isActive = false;
		GetComponent<MeshRenderer>().material = m_default;

		m_counter = 0f;
		
	}
	
	// ------------------------------------
	// Update is called once per frame
	// ------------------------------------
	void Update () {

		// Activate effect
		if (m_isActive) 
			effect();
		
		// Deactivate the effect when counter reach limit
		if(m_isActive && m_counter > m_counterLimit)  
			returnNormal();

		
	}


	// ------------------------------------
	// Methods
	// ------------------------------------
	private void effect() {
		// Change material of objects
		GetComponent<MeshRenderer>().material = m_active;
		m_colorBar.GetComponent<MeshRenderer>().material = m_active;

		// Unable the door 
		m_door.SetActive(false);

		// Enable counter when time is normal
		if (!globalVariables.timeStopped) {
			m_counter += Time.deltaTime;
			Debug.Log(m_counter);
		}
	}


	private void returnNormal() {
		// No more active
		m_isActive = false;
		
		// Reset the materials
		GetComponent<MeshRenderer>().material = m_default;
		m_colorBar.GetComponent<MeshRenderer>().material = m_red;

		// able the door 
		m_door.SetActive(true);

		// Reset timer
		m_counter = 0f;
	}
}
