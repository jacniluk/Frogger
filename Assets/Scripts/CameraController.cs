using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controller for camera, follow player
public class CameraController : MonoBehaviour
{
    // Offset camera to player
    Vector3 offset;
    // Smooth speed
    float smooth = 6.0f;
    // Max up position of camera
    float maxUpPosition;

    // References
    // Player
    public GameObject player;

    // Called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Called every fixed frame-rate frame
    void FixedUpdate()
    {
        // Move camera with smooth
        Vector3 destination = player.transform.position + offset;
        float newZ = Vector3.Lerp(transform.position, destination, smooth * Time.deltaTime).z;
        if (newZ > maxUpPosition)
        {
            newZ = transform.position.z;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }

    // Set max up position, called by Level Manager
    public void SetMaxUpPosition(float _maxUpPosition)
    {
        maxUpPosition = _maxUpPosition;
    }
}
