using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clockManager : MonoBehaviour {

	// public variables -------------------
	public Transform m_hourNeedle;
	public Transform m_minuteNeedle;
	public Transform m_secondNeedle;

	// private variables ------------------
	private GameObject m_gameMaster;
	private float m_angleHour;
	private float m_angleMinute;
	private float m_angleSecond;


	
	
	// ------------------------------------
	// Use this for initialization
	// ------------------------------------
	void Start () {
		// Get the GM Object
		m_gameMaster = GameObject.FindGameObjectWithTag("GameController");		
	}


	// ------------------------------------
	// Update is called once per frame
	// ------------------------------------
	void Update () {
		// Get time data
		m_angleHour = m_gameMaster.GetComponent<globalTime>().hourAngle;
		m_angleMinute = m_gameMaster.GetComponent<globalTime>().minuteAngle;
		m_angleSecond = m_gameMaster.GetComponent<globalTime>().secondAngle;
		
		// Change angle of needles
		m_hourNeedle.localRotation = Quaternion.Euler(0, 0f, m_angleHour);
		m_minuteNeedle.localRotation = Quaternion.Euler(0, 0f, m_angleMinute);
		m_secondNeedle.localRotation = Quaternion.Euler(0, 0f, m_angleSecond);
	}
}
