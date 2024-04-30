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
    
    public TimeSpan CurrentPlayTime;
    public TimeSpan CurrentWaitTime;
    public bool canPlay;

    private const string PlayTimeKey = "PlayTime";
    private const string WaitTimeKey = "WaitTime";
    private const string CanPlayKey = "CanPlay";

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
            CurrentPlayTime += TimeSpan.FromSeconds(Time.deltaTime);
            if (CurrentPlayTime.TotalMinutes >= maxPlayTimePerDay)
            {
                canPlay = false;
                CurrentWaitTime = TimeSpan.FromMinutes(waitTime);
                CurrentPlayTime = TimeSpan.Zero;
            }
        }
        else
        {
            if (CurrentWaitTime.TotalSeconds > 0)
            {
                CurrentWaitTime -= TimeSpan.FromSeconds(Time.deltaTime);
                if (CurrentWaitTime.TotalSeconds <= 0)
                {
                    canPlay = true;
                    CurrentPlayTime = TimeSpan.Zero;
                }
            }
        }

    }

    void SaveData()
    {
        PlayerPrefs.SetString(PlayTimeKey, CurrentPlayTime.ToString());
        PlayerPrefs.SetString(WaitTimeKey, CurrentWaitTime.ToString());
        PlayerPrefs.SetString(CanPlayKey, canPlay.ToString());
    }

    void LoadData()
    {
        string savedPlayTime = PlayerPrefs.GetString(PlayTimeKey, TimeSpan.Zero.ToString());
        string savedWaitTime = PlayerPrefs.GetString(WaitTimeKey, TimeSpan.Zero.ToString());
        string savedCanPlay = PlayerPrefs.GetString(CanPlayKey, true.ToString());
        CurrentPlayTime = TimeSpan.Parse(savedPlayTime);
        CurrentWaitTime = TimeSpan.Parse(savedWaitTime);
        canPlay = bool.Parse(savedCanPlay);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
