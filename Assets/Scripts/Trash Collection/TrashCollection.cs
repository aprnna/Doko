using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Player;
public class TrashCollection : MonoBehaviour
{
    public Trash[] trashes;

    public Image[] trashImage;

    public Image[] checkImage;

    private PlayerManager _pm;

    // Start is called before the first frame update
    void Start()
    {
        _pm = PlayerManager.Instance;
        for (int i = 0; i < trashes.Length; i++)
        {
            Trash scriptableObject = SaveLoadManager.LoadScriptableObject<Trash>(trashes[i].id + ".json");
            trashes[i] = scriptableObject;
            if (scriptableObject != null)
            {
                if (trashes[i].scanned == true)
                {
                    trashImage[i].color = new Color(255, 255, 255, 255);
                    checkImage[i].color = new Color(255, 255, 255, 255);
                }
                else
                {
                    checkImage[i].color = new Color(0, 0, 0, 0);
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
