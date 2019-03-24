using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    // References
    // Player
    GameObject player;

    // Called before the first frame update
    void Start()
    {
        
    }

    // Called once per frame
    void Update()
    {
        
    }

    // Called when this collider/rigidbody has begun touching another rigidbody/collider
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Dead("You drowned!");
        }
    }
}
