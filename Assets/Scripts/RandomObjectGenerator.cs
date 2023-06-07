using System;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

public class RandomObjectGenerator : MonoBehaviour
{
    public ARRaycastManager raycastManager; // AR Raycast Manager component
    public GameObject[] objectsToSpawn; // Array of objects to spawn
    public float spawnInterval = 2f; // Time interval between spawns
    private int trashCount = 0;
    public int trashLimit;

    private float spawnTimer = 0f; // Timer for tracking spawn interval

    void Update()
    {
        // Check if enough time has passed for the next spawn
        if (Time.time >= spawnTimer)
        {
            // Perform raycast from the center of the screen
            Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);

            // Check if any planes are detected
            if (hits.Count > 0 && trashCount < trashLimit)
            {
                // Get a random plane hit result
                ARRaycastHit hit = hits[Random.Range(0, hits.Count)];

                // Spawn a random object on the detected plane
                SpawnRandomObject(hit.pose.position);
                trashCount += 1;

                // Reset the timer for the next spawn
                spawnTimer = Time.time + spawnInterval;
            }
        }
    }

    void SpawnRandomObject(Vector3 spawnPosition)
    {
        // Get a random object from the array
        GameObject randomObject = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

        // Instantiate the object at the spawn position with no rotation
        Instantiate(randomObject, spawnPosition, Quaternion.identity);
    }
}