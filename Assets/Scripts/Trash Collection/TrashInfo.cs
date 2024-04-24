using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrashInfo : MonoBehaviour
{
    public Trash[] trashes;
    public Sprite[] trashSprite;
    public GameObject[] ContentInfo;
    public string[] SceneRecycleName;

    public Image trashImage;
    public Text titletxt;
    public Text categorytxt;

    private int _selected;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void ShowTrashInfo(int index)
    {
        _selected = index;
        gameObject.SetActive(true);
        ContentInfo[index].SetActive(true);
        trashImage.sprite = trashSprite[index];
        titletxt.text = trashes[index].name;
        categorytxt.text = trashes[index].trashType;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneRecycleName[_selected]);
    }

    private void OnDisable()
    {
        ContentInfo[_selected].SetActive(false);
    }

}
