using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class ScreenTimeController : MonoBehaviour
{
    public float maxPlayTimePerDay; // Menit
    public float waitTime; // Menit
    
    public GameObject[] sleep;
    public GameObject[] wakeUp;
    
    private TimeSpan currentPlayTime;
    private TimeSpan currentWaitTime;
    private bool canPlay = true;

    public TMP_Text playTimeText;
    public TMP_Text waitTimeText;
    public Slider TimeSlider;
    public Slider TimeSliderInfo;

    private const string playTimeKey = "PlayTime";
    private const string waitTimeKey = "WaitTime";

    void Start()
    {
        LoadData();
        UpdateUI();
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
                SleepWakeupControl(sleep,wakeUp);
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
                    SleepWakeupControl(wakeUp,sleep);

                }
            }
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        playTimeText.text = FormatTimeSpan(TimeSpan.FromMinutes(maxPlayTimePerDay) - currentPlayTime);
        waitTimeText.text = FormatTimeSpan(currentWaitTime);

        if(canPlay) TimeSlider.value = 1f - ((float)currentPlayTime.TotalMinutes / maxPlayTimePerDay);
        else TimeSlider.value = 1f - (float)currentWaitTime.TotalMinutes / waitTime;
        TimeSliderInfo.value = 1f - ((float)currentPlayTime.TotalMinutes / maxPlayTimePerDay);
    }

    string FormatTimeSpan(TimeSpan timeSpan)
    {
        return string.Format("{0:00}:{1:00}:{2:00}", Mathf.FloorToInt((float)timeSpan.TotalHours), timeSpan.Minutes, timeSpan.Seconds);
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

    void SleepWakeupControl(GameObject[] itemsActive,GameObject[] itemsDisable)
    {
        foreach (var t in itemsActive)
        {
            t.SetActive(true);
        }
        foreach (var t in itemsDisable)
        {
            t.SetActive(false);
        }
    }

}
