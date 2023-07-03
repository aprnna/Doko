using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using UnityEngine.UI;

public class TrashBehavior : MonoBehaviour
{
    public string type;

    private LeanSelectable _leanSelectable;
    private GameManager gm;

    private Vector3 startPos;
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        _leanSelectable = gameObject.GetComponent<LeanSelectable>();
        startPos = gameObject.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == type)
        {
            Destroy(gameObject);
            gm.trashCount++;
        }
        else
        {
            gameObject.transform.position = startPos;
        }
    }
}