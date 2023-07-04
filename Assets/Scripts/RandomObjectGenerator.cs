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
    public Text uiText;
    public GameManager gm;

    private GameObject organik = null;
    private GameObject anorganik = null;
    private GameObject pisang = null;
    private GameObject botol = null;
    private GameObject cup = null;
    private GameObject kaleng = null;
    private GameObject kardus = null;
    
    float pisangPos;
    float botolPos;
    float cupPos;
    float kalengPos;
    float kardusPos;

    private bool pisangDestroyed = false;
    private bool botolDestroyed = false;
    private bool cupDestroyed = false;
    private bool kalengDestroyed = false;
    private bool kardusDestroyed = false;
    

    private float spawnTimer = 0f; // Timer for tracking spawn interval

    private void Start()
    {
        uiText.text = "Loading...";
    }

    void Update()
    {

        // Perform raycast from the center of the screen
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);

        // Check if any planes are detected
        if (hits.Count >= 1 && trashCanCount < trashCanLimit)
        {
            // Get a random plane hit result
            ARRaycastHit hit = hits[0];

            SpawnTrashCan(hit);
            trashCanCount++;
        }

        if (trashCanCount == 1 && trashCount < trashLimit)
        {
            ARRaycastHit hit = hits[0];
            // Spawn a random object on a random position within the detected plane
            SpawnRandomObjectWithinPlane(hit);
            uiText.text = "";
            trashCount += trashLimit + 1;
        }

        pisangPos = Vector3.Distance(organik.transform.position, pisang.transform.position);
        botolPos = Vector3.Distance(anorganik.transform.position, botol.transform.position);
        cupPos = Vector3.Distance(anorganik.transform.position, cup.transform.position);
        kalengPos = Vector3.Distance(anorganik.transform.position, kaleng.transform.position);
        kardusPos = Vector3.Distance(organik.transform.position, kardus.transform.position);

        if (pisangPos <= 0.3f && pisangDestroyed == false)
        {
            pisang.SetActive(false);
            pisangDestroyed = true;
            pisangPos = 100;
            gm.trashCount++;
        }

        if (botolPos <= 0.3f && botolDestroyed == false)
        {
            botol.SetActive(false);
            botolDestroyed = true;
            botolPos = 100;
            gm.trashCount++;
        }

        if (cupPos <= 0.3f && cupDestroyed == false)
        {
            cup.SetActive(false);
            cupDestroyed = true;
            cupPos = 100;
            gm.trashCount++;
        }

        if (kalengPos <= 0.3f && kalengDestroyed == false)
        {
            kaleng.SetActive(false);
            kalengDestroyed = true;
            kalengPos = 100;
            gm.trashCount++;
        }

        if (kardusPos <= 0.3f && kardusDestroyed == false)
        {
            kardus.SetActive(false);
            kardusDestroyed = true;
            kardusPos = 100;
            gm.trashCount++;
        }
    }

    void SpawnRandomObjectWithinPlane(ARRaycastHit hit)
    {
        // Instantiate the object at the spawn position with no rotation
        pisang = Instantiate(objectsToSpawn[0], hit.pose.position + Random.insideUnitSphere * 0.5f, Quaternion.identity);
        botol = Instantiate(objectsToSpawn[1], hit.pose.position + Random.insideUnitSphere * 0.5f, Quaternion.identity);
        cup = Instantiate(objectsToSpawn[2], hit.pose.position + Random.insideUnitSphere * 0.5f, Quaternion.identity);
        kaleng = Instantiate(objectsToSpawn[3], hit.pose.position + Random.insideUnitSphere * 0.5f, Quaternion.identity);
        kardus = Instantiate(objectsToSpawn[4], hit.pose.position + Random.insideUnitSphere * 0.5f, Quaternion.identity);
    }

    void SpawnTrashCan(ARRaycastHit hit)
    {
        Vector3 randomPosition = hit.pose.position + Random.insideUnitSphere * 0.5f;
        organik = Instantiate(trashCans[0], randomPosition, Quaternion.identity);
        anorganik = Instantiate(trashCans[1], randomPosition + new Vector3(0.5f, 0f, 0f), Quaternion.identity);
    }
}