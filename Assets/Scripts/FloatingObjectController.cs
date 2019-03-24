using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObjectController : MonoBehaviour
{
    // References
    // Player
    GameObject player;

    // Speed of leaf
    float speed;
    // Min speed of leaf
    float minSpeed = 2.6f;
    // Max speed of leaf
    float maxSpeed = 2.7f;
    // Can leaf immerse
    bool canImmerse;

    // Start is called before the first frame update
    void Start()
    {
        // Initialization of data
        player = null;
        speed = Random.Range(minSpeed, maxSpeed);
        canImmerse = Random.Range(0, 6) == 0 ? true : false;
        if (canImmerse)
        {
            Invoke("Immerce", Random.Range(3, 5));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called every fixed frame-rate frame
    void FixedUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        if (player)
        {
            if (player.GetComponent<PlayerController>().isFloating == true)
            {
                player.gameObject.GetComponent<PlayerController>().MoveSwimming(transform.position);
            }
            else
            {
                player = null;
            }
        }
    }

    // Called when this collider/rigidbody has begun touching another rigidbody/collider
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            player.GetComponent<PlayerController>().isFloating = true;
        }
    }

    // Called when this collider/rigidbody has stopped touching another rigidbody/collider
    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject == player)
        {
            player = null;
        }
    }

    // Immerce into water
    void Immerce()
    {
        transform.position += new Vector3(0.0f, -0.21f, 0.0f);
        Invoke("Resurface", 1.0f);
    }

    // Resurface out of water
    void Resurface()
    {
        transform.position += new Vector3(0.0f, 0.21f, 0.0f);
        Invoke("Immerce", Random.Range(3, 5));
    }
}
