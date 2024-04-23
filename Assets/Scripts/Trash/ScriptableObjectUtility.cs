using System.IO;
using UnityEngine;

public static class ScriptableObjectUtility
{
    // Simpan Scriptable Object ke file
    public static void SaveScriptableObject<T>(T scriptableObject, string filePath) where T : ScriptableObject
    {
        string json = JsonUtility.ToJson(scriptableObject);
        File.WriteAllText(filePath, json);
        Debug.Log("Scriptable Object saved at: " + filePath);
    }

    // Muat Scriptable Object dari file
    public static T LoadScriptableObject<T>(string filePath) where T : ScriptableObject
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            T scriptableObject = ScriptableObject.CreateInstance<T>();
            JsonUtility.FromJsonOverwrite(json, scriptableObject);
            Debug.Log("Scriptable Object loaded from: " + filePath);
            return scriptableObject;
        }
        else
        {
            Debug.LogError("Scriptable Object file not found at: " + filePath);
            return null;
        }
    }
}