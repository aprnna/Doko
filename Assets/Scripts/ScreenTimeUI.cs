using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ScreenTimeUI : MonoBehaviour
{
    public GameObject[] sleep;
    public GameObject[] wakeUp;

    public TMP_Text playTimeText;
    public TMP_Text waitTimeText;
    public Slider TimeSlider;
    public Slider TimeSliderInfo;

    private ScreenTimeController STController;

    void Start()
    {
        STController = ScreenTimeController.Instance;
        DontDestroyOnLoad(gameObject);
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        playTimeText.text = FormatTimeSpan(TimeSpan.FromMinutes(STController.maxPlayTimePerDay) - STController.currentPlayTime);
        waitTimeText.text = FormatTimeSpan(STController.currentWaitTime);

        if (STController.canPlay) TimeSlider.value = 1f - ((float)STController.currentPlayTime.TotalMinutes / STController.maxPlayTimePerDay);
        else TimeSlider.value = 1f - (float)STController.currentWaitTime.TotalMinutes / STController.waitTime;
        TimeSliderInfo.value = 1f - ((float)STController.currentPlayTime.TotalMinutes / STController.maxPlayTimePerDay);
    }

    string FormatTimeSpan(TimeSpan timeSpan)
    {
        return string.Format("{0:00}:{1:00}:{2:00}", Mathf.FloorToInt((float)timeSpan.TotalHours), timeSpan.Minutes,
            timeSpan.Seconds);
    }


    void SleepWakeupControl(GameObject[] itemsActive, GameObject[] itemsDisable)
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
