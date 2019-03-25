using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destroy appropriate objects which are coming into it
public class ObjectDestroyer : MonoBehaviour
{
    // Called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car") || other.gameObject.CompareTag("Floating Object"))
        {
            Destroy(other.gameObject);
        }
    }
}
