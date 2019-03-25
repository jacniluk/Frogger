using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Set properties for objects on current level
public class LevelManager : MonoBehaviour
{
    // Max time
    public float maxTime;
    // Min speed of car
    public float minCarSpeed;
    // Max speed of car
    public float maxCarSpeed;
    // Min speed of floating object
    public float minFloatingObjectSpeed;
    // Max speed of floating object
    public float maxFloatingObjectSpeed;
    // Min interval for car
    public float minCarInterval;
    // Max interval for car
    public float maxCarInterval;
    // Min interval for floating object
    public float minFloatingObjectInterval;
    // Max interval for floating object
    public float maxFloatingObjectInterval;

    // Start is called before the first frame update
    void Start()
    {
        // Set properties for objects on level depending on its difficulty
        FindObjectOfType<PlayerController>().SetMaxTime(maxTime);
        CarController.SetSpeed(minCarSpeed, maxCarSpeed);
        CarController[] cars = FindObjectsOfType<CarController>();
        foreach (CarController c in cars)
        {
            c.SetSpeedForExisting();
        }
        FloatingObjectController.SetSpeed(minFloatingObjectSpeed, maxFloatingObjectSpeed);
        FloatingObjectController[] floatingObjects = FindObjectsOfType<FloatingObjectController>();
        foreach (FloatingObjectController fo in floatingObjects)
        {
            fo.SetSpeedForExisting();
        }
        Spawn[] spawns = FindObjectsOfType<Spawn>();
        foreach (Spawn s in spawns)
        {
            if (s.spawnObject.tag.Equals("Car"))
            {
                s.SetInterval(minCarInterval, maxCarInterval);
            }
            else if (s.spawnObject.tag.Equals("Floating Object"))
            {
                s.SetInterval(minFloatingObjectInterval, maxFloatingObjectInterval);
            }
        }
        FindObjectOfType<CameraController>().SetMaxUpPosition(GameObject.Find("Target Spots").transform.position.z - 7.375f);
    }
}
