using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashBehavior : MonoBehaviour
{
    public string type;
    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash Can") && other.name == type)
        {
            Destroy(gameObject);
            gm.trashCount++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        gm.trashCount++;
    }
}