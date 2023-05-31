using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card")]
public class Trash : ScriptableObject
{
    public string id;
    public new string name;
    public string description;
    public Sprite image;
    public string trashType;
    public bool scanned;
}
