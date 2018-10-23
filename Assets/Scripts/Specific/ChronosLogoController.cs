using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronosLogoController : MonoBehaviour {

	// public variables -------------------
	public MeshRenderer[] m_changingLines;
	public Material m_neonWhite;
	public Material m_neonBlue;
	public GameObject m_normalHourglass;
	public float m_timeInterval = 1f;
	public GameObject[] m_rotationHourglass;

	// private variables ------------------
	private int m_topIndex;
	private int m_rotateIndex;
	private int m_bootomIndex;
	private float m_timeSpeed;
	private bool m_animateDown;
	private bool m_rotateAnimate;

	
	
	// ------------------------------------
	// Use this for initialization
	// ------------------------------------
	void Start () {
		// Neon Blue start
		m_changingLines[0].material = m_neonBlue;
		m_changingLines[1].material = m_neonBlue;
		m_changingLines[2].material = m_neonBlue;
		m_changingLines[3].material = m_neonBlue;
		m_changingLines[4].material = m_neonBlue;

		// Neon White
		m_changingLines[5].material = m_neonWhite;
		m_changingLines[6].material = m_neonWhite;
		m_changingLines[7].material = m_neonWhite;
		m_changingLines[8].material = m_neonWhite;

		// Initiate the indexes
		m_topIndex = 0;
		m_bootomIndex = 8;
		m_rotateIndex = 2;

		// Animation bool
		m_animateDown = true;
		m_rotateAnimate = false;

		// Time speed to 0
		m_timeSpeed = 0f;

		// Set lit and unlit rotation
		m_rotationHourglass[1].SetActive(false);
		m_rotationHourglass[2].SetActive(false);
		m_rotationHourglass[4].SetActive(false);
		m_rotationHourglass[6].SetActive(false);

		m_rotationHourglass[0].SetActive(true);
		m_rotationHourglass[3].SetActive(true);
		m_rotationHourglass[5].SetActive(true);
		m_rotationHourglass[7].SetActive(true);
	}
	
	// ------------------------------------
	// Update is called once per frame
	// ------------------------------------
	void Update () {
		// Down animation
		if (m_animateDown)
			timePassed();

		if (m_rotateAnimate)
			rotateAnimation();

		Debug.Log(m_rotateIndex);
		
	}

	// ------------------------------------
	// Methods
	// ------------------------------------
	// Function to animate the hourglass -------------------
	private void timePassed() {
		// Calculate time
		if (!globalVariables.timeStopped)
			m_timeSpeed += Time.deltaTime;

		// Animate each line with time
		if (m_timeSpeed > m_timeInterval && m_topIndex < 4) {
			// Change material in the hourglass
			m_changingLines[m_topIndex].material = m_neonWhite;
			m_changingLines[m_bootomIndex].material = m_neonBlue;

			// Increment index values
			m_topIndex ++;
			m_bootomIndex --;

			// Reset time
			m_timeSpeed = 0f;
		}

		// Check the boundaries of the animation
		if (m_timeSpeed > m_timeInterval * 2f && m_topIndex > 3) {
			// Reset indexes
			m_topIndex = 0;
			m_bootomIndex = 8;
			m_timeSpeed = m_timeInterval;

			// Stop Animation down
			m_animateDown = false;
			m_rotateAnimate = true;

			// Reset Neon and deactivate lit
			m_rotationHourglass[0].SetActive(false);
			m_rotationHourglass[1].SetActive(true);
			resetHourGlass();
		}
	}
	

	// Function to rotate the hourglass -----------------
	private void rotateAnimation() {
		// Calculate time
		if (!globalVariables.timeStopped)
			m_timeSpeed += Time.deltaTime;

		// Animate a rotation
		if (m_timeSpeed > m_timeInterval && m_rotateIndex < 7) {
			// Check the state of the index
			if (m_rotateIndex > 2) {
				// Return to normal neon state
				m_rotationHourglass[m_rotateIndex - 2].SetActive(false);
				m_rotationHourglass[m_rotateIndex - 1].SetActive(true);
			}

			// Change neon state
			m_rotationHourglass[m_rotateIndex].SetActive(true);
			m_rotationHourglass[m_rotateIndex + 1].SetActive(false);

			// Increment rotation index
			m_rotateIndex += 2;

			// Reset time
			m_timeSpeed = 0f;
		}

		// Check the boundaries of the animation
		if (m_timeSpeed > m_timeInterval && m_rotateIndex > 7) {

			// Reactivate the normal hour glass
			m_rotationHourglass[0].SetActive(true);
			m_rotationHourglass[1].SetActive(false);

			// Stop rotate animation after an interval
			if (m_timeSpeed > m_timeInterval * 2f) {
				m_rotateAnimate = false;
				m_animateDown = true;

				// Reset index
				m_rotateIndex = 2;
				m_timeSpeed = 0f;
			}

			// Supress last rotate state
			m_rotationHourglass[6].SetActive(false);		
			m_rotationHourglass[7].SetActive(true);
		}

	}


	// Function to restore the normal hourglass -----------
	private void resetHourGlass() {
		// Neon Blue start
		m_changingLines[0].material = m_neonBlue;
		m_changingLines[1].material = m_neonBlue;
		m_changingLines[2].material = m_neonBlue;
		m_changingLines[3].material = m_neonBlue;
		m_changingLines[4].material = m_neonBlue;

		// Neon White
		m_changingLines[5].material = m_neonWhite;
		m_changingLines[6].material = m_neonWhite;
		m_changingLines[7].material = m_neonWhite;
		m_changingLines[8].material = m_neonWhite;
	}
}
