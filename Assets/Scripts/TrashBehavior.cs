using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using UnityEngine.UI;

public class TrashBehavior : MonoBehaviour
{
    public string type;

    private GameManager gm;
    private Vector3 anorganik => GameObject.Find("Anorganik").transform.position;
    private Vector3 organik => GameObject.Find("Organik").transform.position;

    private Vector3 startPos;
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        startPos = gameObject.transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, organik) <= 100 && type == "Organik")
        {
            Destroy(gameObject);
            gm.trashCount++;
        }else if (Vector3.Distance(gameObject.transform.position, anorganik) <= 100 && type == "Anorganik")
        {
            Destroy(gameObject);
            gm.trashCount++;
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.name == type)
    //     {
    //         Destroy(gameObject);
    //         gm.trashCount++;
    //     }
    //     else
    //     {
    //         gameObject.transform.position = startPos;
    //     }
    // }
}