using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class ScreenTimeController : MonoBehaviour
{
    public static ScreenTimeController Instance;

    public float maxPlayTimePerDay; // Menit
    public float waitTime; // Menit
    
    public TimeSpan currentPlayTime;
    public TimeSpan currentWaitTime;
    public bool canPlay = true;

    private const string playTimeKey = "PlayTime";
    private const string waitTimeKey = "WaitTime";

    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    void Update()
    {
        if (canPlay)
        {
            currentPlayTime += TimeSpan.FromSeconds(Time.deltaTime);
            if (currentPlayTime.TotalMinutes >= maxPlayTimePerDay)
            {
                canPlay = false;
                currentWaitTime = TimeSpan.FromMinutes(waitTime);
            }
        }
        else
        {
            if (currentWaitTime.TotalSeconds > 0)
            {
                currentWaitTime -= TimeSpan.FromSeconds(Time.deltaTime);
                if (currentWaitTime.TotalSeconds <= 0)
                {
                    canPlay = true;
                    currentPlayTime = TimeSpan.Zero;
                    currentWaitTime = TimeSpan.Zero;

                }
            }
        }

    }

    void SaveData()
    {
        PlayerPrefs.SetString(playTimeKey, currentPlayTime.ToString());
        PlayerPrefs.SetString(waitTimeKey, currentWaitTime.ToString());
    }

    void LoadData()
    {
        string savedPlayTime = PlayerPrefs.GetString(playTimeKey, TimeSpan.Zero.ToString());
        string savedWaitTime = PlayerPrefs.GetString(waitTimeKey, TimeSpan.Zero.ToString());

        currentPlayTime = TimeSpan.Parse(savedPlayTime);
        currentWaitTime = TimeSpan.Parse(savedWaitTime);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
