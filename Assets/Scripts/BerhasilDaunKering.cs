using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
public class BerhasilDaunKering : MonoBehaviour
{
    public Canvas CanvasMain;
    public Canvas CanvasBerhasil;
    private bool Done = true;
    public GameObject Check;
    public GameObject Background;
    public GameObject[] objects;
    private Animator mAnimator;
    private Animator mAnimator3;
    private PlayerManager _pm;
    // Start is called before the first frame update
    void Start()
    {
        _pm = PlayerManager.Instance;
        mAnimator = Background.transform.GetChild(0).GetComponent<Animator>();
        mAnimator3 = Background.transform.GetChild(0).GetChild(1).GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        bool allChildrenActive = CheckAllChildrenActiveStatus();
        if (allChildrenActive && Done)
        {
            _pm.Coin += 80;
            CanvasMain.gameObject.SetActive(false);
            CanvasBerhasil.gameObject.SetActive(true);
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].gameObject.SetActive(false);
            }
            Background.SetActive(true);
            mAnimator.SetTrigger("FadeInScale");
            mAnimator3.SetTrigger("Spin");

            Done = false;

        }
    }
    private bool CheckAllChildrenActiveStatus()
    {
        int childCount = Check.gameObject.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = Check.gameObject.transform.GetChild(i);


            if (!child.gameObject.activeSelf)
            {
                return false;
            }
        }

        return true;
    }
}
