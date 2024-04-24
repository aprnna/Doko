using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalizationHandler : MonoBehaviour
{
    [SerializeField] private int _pageIndex;
    [SerializeField] private Localization _localizationAsset;
    [SerializeField] private TMP_Text[] _texts;

    void Awake()
    {
        for (int i = 0; i < _texts.Length; i++)
        {
            _texts[i].text = _localizationAsset.Pages[_pageIndex].dictionaries[i].text[1];
        }
    }
}
