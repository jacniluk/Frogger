using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafController : MonoBehaviour
{
    // References
    // Player
    GameObject player;

    // Speed of leaf
    float speed;
    // Min speed of leaf
    float minSpeed = 5.0f;
    // Max speed of leaf
    float maxSpeed = 6.0f;
    // Can leaf immerse
    bool canImmerse;
    // Is leaf currently immersing
    bool isImmersing;

    // Start is called before the first frame update
    void Start()
    {
        // Initialization of data
        player = null;
        speed = Random.Range(minSpeed, maxSpeed);
        canImmerse = Random.Range(0, 2) == 0 ? true : false;
        isImmersing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canImmerse && !isImmersing)
        {
            if (Random.Range(0, 150) == 0)
            {
                isImmersing = true;
                transform.position += new Vector3(0.0f, -0.21f, 0.0f);
                Invoke("Resurface", 1.0f);
            }
        }
    }

    // Called every fixed frame-rate frame
    void FixedUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        if (player)
        {
            if (player.GetComponent<PlayerController>().isSwimming == true)
            {
                player.transform.position = transform.position;
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
            player.GetComponent<PlayerController>().isSwimming = true;
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

    // Resurface out of water
    void Resurface()
    {
        isImmersing = false;
        transform.position += new Vector3(0.0f, 0.21f, 0.0f);
    }
}
