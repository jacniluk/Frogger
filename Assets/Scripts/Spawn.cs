using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawn appropriate objects on map
public class Spawn : MonoBehaviour
{
    // Min time to next respawn
    float minInterval;
    // Max time to next respawn
    float maxInterval;

    // References
    // Spawn object
    public GameObject spawnObject;

    // Respawn new car
    void Respawn()
    {
        Instantiate(spawnObject, transform);

        Invoke("Respawn", Random.Range(minInterval, maxInterval));
    }

    // Set interval on current level, called by Level Manager
    public void SetInterval(float _minInterval, float _maxInterval)
    {
        minInterval = _minInterval;
        maxInterval = _maxInterval;
        Invoke("Respawn", Random.Range(minInterval, maxInterval));
    }
}
