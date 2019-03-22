using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafController : MonoBehaviour
{
    // Speed of leaf
    float speed;
    // Min speed of leaf
    float minSpeed = 15.0f;
    // Max speed of leaf
    float maxSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called every fixed frame-rate frame
    void FixedUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
}
