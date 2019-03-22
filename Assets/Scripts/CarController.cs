using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Speed of car
    float speed;
    // Min speed of car
    float minSpeed = 15.0f;
    // Max speed of car
    float maxSpeed = 20.0f;

    // Called before the first frame update
    void Start()
    {
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
