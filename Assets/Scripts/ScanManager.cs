using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanManager : MonoBehaviour
{
    public string label;
    public Trash[] trash;
    public Text typeText;
    public Text nameText;
    public Text descText;
    public Image trashImage;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < trash.Length; i++)
        {
            if (label == trash[i].id)
            {
                trash[i].scanned = true;
                displayTrash(i);
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
        trashImage.sprite = trash[i].image;
    }
}
