using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // References
    // Car
    public GameObject spawnObject;

    // Time to next respawn
    public float respawnIntervalMin = 1.0f;
    public float respawnIntervalMax = 2.0f;

    // Called before the first frame update
    void Start()
    {
        Invoke("Respawn", Random.Range(respawnIntervalMin, respawnIntervalMax));
    }

    // Called once per frame
    void Update()
    {
        
    }

    // Respawn new car
    void Respawn()
    {
        Instantiate(spawnObject, transform);

        Invoke("Respawn", Random.Range(respawnIntervalMin, respawnIntervalMax));
    }
}
