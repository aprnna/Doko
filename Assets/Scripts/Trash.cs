using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

[CreateAssetMenu(fileName = "New Card")]
[Serializable] 
public class Trash : ScriptableObject
{
    public string id;
    public new string name;
    public string description;
    [JsonIgnore] public Sprite image;
    public string trashType;
    public bool scanned;
}
public static class SaveLoadManager
{
    public static void SaveScriptableObject<T>(T scriptableObject, string fileName) where T : ScriptableObject
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        string json = JsonConvert.SerializeObject(scriptableObject, Formatting.Indented);
        File.WriteAllText(filePath, json);

        Debug.Log("ScriptableObject saved at: " + filePath);
    }

    public static T LoadScriptableObject<T>(string fileName) where T : ScriptableObject
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            T scriptableObject = JsonConvert.DeserializeObject<T>(json);

            Debug.Log("ScriptableObject loaded from: " + filePath);
            return scriptableObject;
        }
        else
        {
            Debug.LogError("ScriptableObject file not found at: " + filePath);
            return null;
        }
    }
}