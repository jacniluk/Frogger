using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car") || other.gameObject.CompareTag("Leaf"))
        {
            Destroy(other.gameObject);
        }
    }
}
