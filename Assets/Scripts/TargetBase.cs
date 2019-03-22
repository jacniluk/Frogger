using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBase : MonoBehaviour
{
    // Components
    // Collider
    BoxCollider coll;

    // Called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider>();
    }

    // Called once per frame
    void Update()
    {
        
    }

    // Called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Base is not available anymore
        coll.enabled = false;
    }
}
