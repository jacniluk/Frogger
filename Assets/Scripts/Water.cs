using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destroy player who come into
public class Water : MonoBehaviour
{
    // References
    // Player
    GameObject player;

    // Called when this collider/rigidbody has begun touching another rigidbody/collider
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Dead("You drowned!");
        }
    }
}
