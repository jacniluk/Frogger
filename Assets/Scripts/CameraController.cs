using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Offset camera to player
    private Vector3 offset;

    // References
    // Player the camera belongs to
    public GameObject player;

    // Smooth speed
    public float smooth = 6.0f;
    // Max up position of camera
    public float maxUpPosition = 4.0f;

    // Called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Called once per frame
    void Update()
    {
        
    }

    // Called after all Update functions have been called
    void LateUpdate()
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
}
