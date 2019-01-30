using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class actionIconController : MonoBehaviour
{
    // public variables ---------------------
    public float m_speed = 5f;
    public bool m_hover;

    // private variables --------------------
    private float m_value = 0f;

    // --------------------------------------
    // Start is called before update
    // --------------------------------------
    void Start()
    {
           
    }

    // --------------------------------------
    // Update is called once per frame
    // --------------------------------------
    void Update()
    {
        if (m_hover)
            activate();

        if (!m_hover)
            deactivate();
    }

    // --------------------------------------
    // Methods
    // --------------------------------------
    public void activate() {
        // Get the animation activated
        if (m_value < 100f) {
            m_value += m_speed;
            gameObject.GetComponent<Slider>().value = m_value;
        }
    }

    public void deactivate() {
        // Get the animation deactivated
        if (m_value > 0f) {
            m_value -= m_speed;
            gameObject.GetComponent<Slider>().value = m_value;
        }
    }
}
