using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaunKering : MonoBehaviour
{
    public GameObject ButtonHover;
    public GameObject Instruksi;
    public GameObject Check;
    public GameObject Cursor;
    private Animator mAnimator;
    public int Child;

    public static bool LemDone;
    public void PopDaunKering()
    {
        if (LemDone)
        {
            mAnimator = Cursor.GetComponent<Animator>();
            gameObject.SetActive(true);
            ButtonHover.gameObject.transform.GetChild(Child - 1).gameObject.SetActive(false);
            ButtonHover.gameObject.transform.GetChild(Child).gameObject.SetActive(true);
            Instruksi.gameObject.transform.GetChild(Child - 1).gameObject.SetActive(false);
            Instruksi.gameObject.transform.GetChild(Child).gameObject.SetActive(true);
            Check.gameObject.SetActive(true);

            Cursor.SetActive(true);
            mAnimator.SetTrigger("Colored");
        }
    }
}
