using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonManager : MonoBehaviour {

	// public variables -------------------
	[HideInInspector]public bool m_gotTriggered = false;
	[Header("List of all buttons available")]
	public bool m_podiumSwitch;

	// private variables -------------------
	

	// ------------------------------------
	// Use this for initialization
	// ------------------------------------
	void Start () {

		
	}
	
	// ------------------------------------
	// Update is called once per frame
	// ------------------------------------
	void Update () {
		if (m_podiumSwitch)
			gameObject.GetComponent<podiumSwitch>().m_isActive = m_gotTriggered;
			


		
	}


	// ------------------------------------
	// Methods
	// ------------------------------------


}
