using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    // Called before the first frame update
    void Start()
    {
        
    }

    // Called once per frame
    void Update()
    {
        
    }

    // Called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car") || other.gameObject.CompareTag("Floating Object"))
        {
            Destroy(other.gameObject);
        }
    }
}
