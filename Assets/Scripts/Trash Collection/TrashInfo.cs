using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashInfo : MonoBehaviour
{
    public Trash[] trashes;
    public Sprite[] trashSprite;
    public GameObject[] ContentInfo;

    public Image trashImage;
    public Text titletxt;
    public Text categorytxt;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void ShowTrashInfo(int index)
    {
        gameObject.SetActive(true);
        trashImage.sprite = trashSprite[index];
        titletxt.text = trashes[index].name;
        categorytxt.text = trashes[index].trashType;
    }

}
