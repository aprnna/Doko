using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Localiztion")]
public class Localization : ScriptableObject
{
    [SerializeField] private Page[] pages;
    
    public Page[] Pages
    {
        get => pages;
        set => pages = value;
    }

    [Serializable]
    public struct Dictionaries
    {
        public string[] text;
    }
    
    [Serializable]
    public struct Page
    {
        public string pageName;
        public Dictionaries[] dictionaries;
    }
}
