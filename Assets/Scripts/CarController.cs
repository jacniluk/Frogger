using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controller for car, update movement
public class CarController : MonoBehaviour
{
    // Speed of car
    float speed;
    // Min speed of car
    static float minSpeed;
    // Max speed of car
    static float maxSpeed;

    // Called before the first frame update
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    // Called every fixed frame-rate frame
    void FixedUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    // Set speed on current level, called by Level Manager
    public static void SetSpeed(float _minSpeed, float _maxSpeed)
    {
        minSpeed = _minSpeed;
        maxSpeed = _maxSpeed;
    }

    // Set speed for cars existing since map start, called by Level Manager
    public void SetSpeedForExisting()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }
}
