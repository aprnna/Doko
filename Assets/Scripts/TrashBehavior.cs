using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashBehavior : MonoBehaviour
{
    public string type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash Can") && other.name == type)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}