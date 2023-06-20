using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berhasiil : MonoBehaviour
{
    public GameObject Check;
    public GameObject Background;
    public GameObject Finishing;
    public Canvas CanvasMain;
    public Canvas CanvasBerhasil;
    private bool Done = true;
    private Animator mAnimator;
    private Animator mAnimator2;
    private Animator mAnimator3;

    private void Start()
    {
        mAnimator = Background.transform.GetChild(0).GetComponent<Animator>();
        mAnimator2 = Background.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        mAnimator3 = Background.transform.GetChild(0).GetChild(1).GetComponent<Animator>();
    }
    void Update()
    {
        bool allChildrenActive = CheckAllChildrenActiveStatus();
        if (allChildrenActive && Done)
        {
            CanvasMain.gameObject.SetActive(false);
            CanvasBerhasil.gameObject.SetActive(true);
            Background.SetActive(true);
            GameObject newLine = Instantiate(Finishing, Background.transform.GetChild(0));
            Finishing.SetActive(false);
            mAnimator.SetTrigger("FadeInScale");
            mAnimator2.SetTrigger("FadeInUp");
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
