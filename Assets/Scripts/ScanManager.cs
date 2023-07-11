using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScanManager : MonoBehaviour
{
    public string label;
    public Trash[] trash;
    public Text typeText;
    public Text nameText;
    public Text descText;
    public GameObject trashImage;
    public Animator mAnimator;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator.SetTrigger("Spin");
        
        for (int i = 0; i < trash.Length; i++)
        {
            if (label == trash[i].id)
            {
                trash[i].scanned = true;
                displayTrash(i);
                Trash scriptableObject = trash[i];
                SaveLoadManager.SaveScriptableObject(scriptableObject, trash[i].id + ".json");
            }
            else
            {
                Trash scriptableObject = trash[i];
                SaveLoadManager.SaveScriptableObject(scriptableObject, trash[i].id + ".json");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void displayTrash(int i)
    {
        nameText.text = trash[i].name;
        typeText.text = trash[i].trashType;
        descText.text = trash[i].description;
        trashImage.GetComponent<Image>().sprite = trash[i].image;
        RectTransform rt = trashImage.GetComponent (typeof (RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2(trash[i].image.pivot.x * 2, trash[i].image.pivot.y * 2);
    }
}
