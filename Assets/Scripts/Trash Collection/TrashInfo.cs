using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TrashInfo : MonoBehaviour
{
    public Trash[] trashes;
    public Sprite[] trashSprite;
    public GameObject[] ContentInfo;
    public string[] SceneRecycleName;

    public Image trashImage;
    public TMP_Text titletxt;
    public TMP_Text categorytxt;

    private int _selected;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void ShowTrashInfo(int index)
    {
        _selected = index;
        gameObject.SetActive(true);
        if (ContentInfo[index]) ContentInfo[index].SetActive(true);
        trashImage.sprite = trashSprite[index];
        titletxt.text = trashes[index].name;
        categorytxt.text = trashes[index].trashType;
    }

    public void ChangeScene()
    {
        if(SceneRecycleName[_selected]!=null) SceneManager.LoadScene(SceneRecycleName[_selected]);
    }

    private void OnDisable()
    {
        if (ContentInfo[_selected]) ContentInfo[_selected].SetActive(false);
    }

}
