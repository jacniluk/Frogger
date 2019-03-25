using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controller for floating object, leafs and turtles on water which player can come on
public class FloatingObjectController : MonoBehaviour
{
    // References
    // Player
    GameObject player;

    // Speed of floating object
    float speed;
    // Min speed of floating object
    static float minSpeed;
    // Max speed of floating object
    static float maxSpeed;
    // Can floating object immerse
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

    // Set speed on current level, called by Level Manager
    public static void SetSpeed(float _minSpeed, float _maxSpeed)
    {
        minSpeed = _minSpeed;
        maxSpeed = _maxSpeed;
    }

    // Set speed for floating objects existing since map start, called by Level Manager
    public void SetSpeedForExisting()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }
}
