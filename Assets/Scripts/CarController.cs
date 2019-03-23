using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Speed of car
    float speed;
    // Min speed of car
    float minSpeed = 5.0f; // 15
    // Max speed of car
    float maxSpeed = 6.0f; // 20

    // Called before the first frame update
    void Start()
    {
        // Initialization of data
        speed = Random.Range(minSpeed, maxSpeed);
    }

    // Called once per frame
    void Update()
    {
        
    }

    // Called every fixed frame-rate frame
    void FixedUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
}
