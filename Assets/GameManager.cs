using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int trashCount;
    public int trashLimit;

    private void Start()
    {
        trashCount = 0;
    }

    private void Update()
    {
        if (trashCount == trashLimit)
        {
            gameObject.GetComponent<ScenaManager>().ChangeSceneWithSound("SandboxRazak");
        }
    }
}
