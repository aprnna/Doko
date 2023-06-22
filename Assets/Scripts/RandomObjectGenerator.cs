using System;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class RandomObjectGenerator : MonoBehaviour
{
    public ARRaycastManager raycastManager; // AR Raycast Manager component
    public GameObject[] objectsToSpawn; // Array of objects to spawn
    public GameObject[] trashCans;
    public float spawnInterval = 2f; // Time interval between spawns
    private int trashCount = 0;
    public int trashLimit;
    private int trashCanCount = 0;
    private int trashCanLimit = 1;
    public GameObject uiText;

    private float spawnTimer = 0f; // Timer for tracking spawn interval

    private void Start()
    {
        uiText.SetActive(true);
    }

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
            if (hits.Count > 1 && trashCanCount < trashCanLimit)
            {
                uiText.SetActive(false);
                // Get a random plane hit result
                ARRaycastHit hit = hits[Random.Range(0, hits.Count)];

                SpawnTrashCan(hit);
                trashCanCount++;
            }

            if (trashCanCount == 1 && trashCount < trashLimit)
            {
                ARRaycastHit hit = hits[Random.Range(0, hits.Count)];
                // Spawn a random object on a random position within the detected plane
                SpawnRandomObjectWithinPlane(hit);
                trashCount += 1;
            }
        }
    }

    void SpawnRandomObjectWithinPlane(ARRaycastHit hit)
    {
        // Get a random position within the detected plane
        Vector3 randomPosition = hit.pose.position + Random.insideUnitSphere * 0.5f;

        // Spawn a random object at the random position
        SpawnRandomObject(randomPosition);
    }

    void SpawnRandomObject(Vector3 spawnPosition)
    {
        // Get a random object from the array
        GameObject randomObject = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

        // Instantiate the object at the spawn position with no rotation
        Instantiate(randomObject, spawnPosition, Quaternion.identity);
    }

    void SpawnTrashCan(ARRaycastHit hit)
    {
        Vector3 randomPosition = hit.pose.position + Random.insideUnitSphere * 0.5f;
        Instantiate(trashCans[0], randomPosition, Quaternion.identity);
    }
}