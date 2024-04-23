using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
public class RecycleDriedLeavesScript : MonoBehaviour
{
    public RecycleMenu Menu;
    public int currentStep = 1;
    private PlayerManager _pm;

    [Header("Gluing")]
    public Trigger[] TriggerGlue;
    public GameObject[] Glue;
    public GameObject Cursor;
    private Animator _mAnimatorCursor;

    [Header("Pasting")]
    public GameObject DriedLeaves;
    public AudioSource PastingSfx;

    [Header("Painting")]
    public Trigger TriggerPaint;
    public GameObject CursorPainting;
    private Animator _mAnimatorCursorPainting;
    public GameObject ColoredDriedLeaves;

    [Header("Finish")]
    public Canvas CanvasMain;
    public Canvas CanvasFinish;
    public Animator mShine;
    public Animator mRecycleDriedLeaves;
    void Start()
    {
        _pm = PlayerManager.Instance;
        _mAnimatorCursor = Cursor.GetComponent<Animator>();
        _mAnimatorCursorPainting = CursorPainting.GetComponent<Animator>();
        StartCoroutine(Gluing());
    }

    IEnumerator Gluing()
    {
        string[] triggerAnim = { "LemKanan", "LemBawah","LemKiri","LemAtas" };
        for(int idx = 0; idx < Glue.Length; idx++)
        {
            _mAnimatorCursor.SetTrigger(triggerAnim[idx]);
            TriggerGlue[idx].gameObject.SetActive(true);
            yield return new WaitUntil(()=>TriggerGlue[idx].trigger==true);
            Glue[idx].SetActive(true);
            TriggerGlue[idx].gameObject.SetActive(false);
        }
        Cursor.SetActive(false);
        Debug.Log("NEXT STEP");
        Menu.NextStep(currentStep);
        currentStep++;
        Cursor.SetActive(true);
        _mAnimatorCursor.SetTrigger("ClickMe");

    }
    public void ButtonPasting()
    {
        StartCoroutine(Pasting());
    }
    IEnumerator Pasting()
    {
        if (currentStep == 2)
        {
            Cursor.SetActive(false);
            PastingSfx.Play();
            DriedLeaves.SetActive(true);
            Debug.Log("NEXT STEP");
            Menu.NextStep(currentStep);
            currentStep++;
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Painting());
        }
    }
    IEnumerator Painting()
    {
        CursorPainting.SetActive(true);
        _mAnimatorCursorPainting.SetTrigger("Colored");
        TriggerPaint.gameObject.SetActive(true);
        yield return new WaitUntil(() => TriggerPaint.trigger == true);
        DriedLeaves.SetActive(false);
        ColoredDriedLeaves.SetActive(true);
        Menu.NextStep(currentStep);
        currentStep++;
        StartCoroutine(Finish());
    }
    IEnumerator Finish()
    {
        CanvasMain.gameObject.SetActive(false);
        CanvasFinish.gameObject.SetActive(true);
        mRecycleDriedLeaves.SetTrigger("FadeInScale");
        mShine.SetTrigger("Spin");
        yield return new WaitForSeconds(0.5f);
        _pm.Coin += 80;
    }

}
