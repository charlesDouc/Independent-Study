using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watchPickUpController : MonoBehaviour {

	
	// private variables ------------------
	private MeshRenderer m_renderer;
	

	// ------------------------------------
	// Use this for initialization
	// ------------------------------------
	void Start () {
		// Get the component
		m_renderer = gameObject.GetComponent<MeshRenderer>();	
	}
	

	// ------------------------------------
	// Update is called once per frame
	// ------------------------------------
	void Update () {

		// If the renderer ger disable (by player's interaction)
		if (!m_renderer.enabled)
			Picked();
	}


	// ------------------------------------
	// Methods
	// ------------------------------------
	private void Picked() {
		// Get the player game object and change the state of the watch
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<inputManager>().m_canStop = true;

		// Destroy the object, no longer needed
		Destroy(gameObject);

	}
}
