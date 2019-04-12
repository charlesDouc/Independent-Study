using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generalTriggers : MonoBehaviour
{

    // public variables ---------------------
    public bool m_destroyDynaObj;

    // private variables --------------------

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
        
    }

    // --------------------------------------
    // Methods
    // --------------------------------------
 private void OnTriggerEnter(Collider col)
 {
     if (m_destroyDynaObj && col.gameObject.tag == "DynamicObject")
        Destroy(col.gameObject);

 }
}
