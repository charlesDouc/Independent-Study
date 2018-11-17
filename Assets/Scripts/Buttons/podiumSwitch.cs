using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class podiumSwitch : MonoBehaviour {

// public variables -------------------
	[HideInInspector]public bool m_isActive;
	public Material m_unlit;
	public Material m_lit;
	public GameObject m_door;
	[Header ("Specific Objects")]
	public GameObject m_switchOn;
	public GameObject m_switchOff;
	public GameObject m_activationLight;
	[Header ("Delay")]
	public float m_counterLimit = 5f;

	// private variables -------------------
	private float m_counter = 0f;


	// ------------------------------------
	// Use this for initialization
	// ------------------------------------
	void Start () {
		// Set the button inactive
		m_isActive = false;
		// Set the off button
		m_switchOff.SetActive(true);
		m_switchOn.SetActive(false);

		// Set the material on the light
		m_activationLight.GetComponent<MeshRenderer>().material = m_unlit;
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
		// Set the material on the light to lit
		m_activationLight.GetComponent<MeshRenderer>().material = m_lit;
		// Unable the door 
		m_door.SetActive(false);
		// Get the switch on
		m_switchOn.SetActive(true);
		m_switchOff.SetActive(false);

		// Tell the btn manager
		gameObject.GetComponent<buttonManager>().m_gotTriggered = true;


		// Enable counter when time is normal
		if (!globalVariables.timeStopped) {
			m_counter += Time.deltaTime;
			Debug.Log(m_counter);
		}
	}


	private void returnNormal() {
		// No more active
		//m_isActive = false;

		// Reset the materials
		m_activationLight.GetComponent<MeshRenderer>().material = m_unlit;

		// Reable the door 
		m_door.SetActive(true);
		// Get the switch off
		m_switchOn.SetActive(false);
		m_switchOff.SetActive(true);

		// Tell the btn manager
		gameObject.GetComponent<buttonManager>().m_gotTriggered = false;

		// Reset timer
		m_counter = 0f;
	}
}
