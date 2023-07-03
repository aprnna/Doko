using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int trashCount;
    public int trashLimit;

    public bool isPlaying = true;

    private void Start()
    {
        trashCount = 0;
    }

    private void Update()
    {
        if (trashCount == trashLimit)
        {
            isPlaying = false;
        }
    }
}
