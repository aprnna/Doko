using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Player;
public class ScanManager : MonoBehaviour
{
    public string label;
    public Trash[] trash;
    public Text typeText;
    public Text nameText;
    public Text descText;
    public GameObject trashImage;
    public Animator mAnimator;
    private PlayerManager _pm;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator.SetTrigger("Spin");
        _pm = PlayerManager.Instance;
        for (int i = 0; i < trash.Length; i++)
        {
            if (label == trash[i].id)
            {
                trash[i].scanned = true;
                _pm.SetTrash(label, true);
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
        _pm.Coin += 50;
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
