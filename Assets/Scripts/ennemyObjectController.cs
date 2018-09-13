using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ennemyObjectController : MonoBehaviour {

	// public variables -------------------


	// private variables ------------------
	private GameObject m_player;
	private Transform m_target;
	private NavMeshAgent m_agent;
	private bool m_timeState;

	// ------------------------------------
	// Use this for initialization
	// ------------------------------------
	void Start () {
		// Get the player's position
		m_player = GameObject.FindGameObjectWithTag("Player");
		m_target = m_player.transform;

		// Set the agent component
		m_agent = GetComponent<NavMeshAgent>();
		m_agent.speed = 0f;
		
	}
	
	// ------------------------------------
	// Update is called once per frame
	// ------------------------------------
	void Update () {
		// Get the time state of the game from the player
		m_timeState = m_player.GetComponent<timeManager>().m_timeStop;

		// If time is stopped, proceed with the chase
		if (m_timeState) {
			m_agent.speed = 2f;
		} else {
			// Don't move
			m_agent.speed = 0f;
		}

		// Set the destination of the agent to the player current position
		m_agent.destination = m_target.position;
		
	}
}
