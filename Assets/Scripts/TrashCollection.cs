using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashCollection : MonoBehaviour
{
    public Trash[] trashes;

    public Image[] trashImage;

    public Image[] checkImage;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < trashes.Length; i++)
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
