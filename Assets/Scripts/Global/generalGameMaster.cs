using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class generalGameMaster : MonoBehaviour {

	// public variables -------------------


	// private variables ------------------
	
	
	// ------------------------------------
	// Use this for initialization
	// ------------------------------------
	void Start () {
		
	}
	
	// ------------------------------------
	// Update is called once per frame
	// ------------------------------------
	void Update () {
		// Lock cursor at middle of the screen
		Cursor.lockState = CursorLockMode.Locked;
		
		// Quit the game
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();

		// Restart the game
		if(Input.GetKeyDown(KeyCode.LeftArrow))
			SceneManager.LoadScene(0);
		
		if(Input.GetKeyDown(KeyCode.RightArrow))
			SceneManager.LoadScene(1);

	}
}
