using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    // public variables -------------------------
    public bool m_open = false;                       // If the door is open or close
    public bool m_close = true;
    public float m_speed = 0.5f;                      // Speed of the transition

    // private variables ------------------------
    private Vector3 m_currentPos;                     // Current pos of the object
    private float m_target;                           // Y target of the door (only Y since this door opens verticaly)
    private float m_height = 2.85f;                   // The height of the standard vertical door used in this demo

    // ------------------------------------------
    // Start is called before update
    // ------------------------------------------
    void Start()
    {
        // Set the target depending where the object stands
        // Target is basicaly the height of the door itself
        m_target = transform.position.y + m_height;
    }

    // ------------------------------------------
    // Update is called once per frame
    // ------------------------------------------
    void Update()
    {
        // If the door open's animation is trigger
        if (m_open && !globalVariables.timeStopped)
            Open();
        else if (m_close && !globalVariables.timeStopped)
            Close();
    }

    // ------------------------------------------
    // Methods
    // ------------------------------------------

    // Open animation of the door -----------------------------------------
    private void Open()
    {
        // Catch the current location
        m_currentPos = gameObject.transform.position;

        // Check where the door stands atm
        if (m_currentPos.y < m_target)
        {
            // Lerp to object toward its destination
            m_currentPos = Vector3.Lerp(m_currentPos, new Vector3(m_currentPos.x, m_target, m_currentPos.z), m_speed * Time.deltaTime);
        }
        
        // Stop the animation after acertain point
        if (m_currentPos.y > m_target - 0.1f)
            m_open = false;

        // Update the object position
        transform.position = m_currentPos;        
    }


    // Close animation of the door ----------------------------------------
    private void Close()
    {
        // Catch the current location
        m_currentPos = gameObject.transform.position;

        // Check where the door stands atm
        if (m_currentPos.y > m_target - m_height)
        {
            // Lerp to object toward its destination
            m_currentPos = Vector3.Lerp(m_currentPos, new Vector3(m_currentPos.x, m_target - m_height, m_currentPos.z), m_speed * Time.deltaTime);
        }


        // Stop the animation after acertain point
        if (m_currentPos.y < m_target - m_height + 0.1f)
            m_close = false;


        // Update the object position
        transform.position = m_currentPos;        
    }


    // Receive info on the door state ------------------------------------
    public void DoorState(bool active)
    {
        // Select the correct state based from another object
        if (active)
        {
            m_open = true;
            m_close = false;
        }
        else
        {
            m_close = true;
            m_open = false;
        }
    }
}
