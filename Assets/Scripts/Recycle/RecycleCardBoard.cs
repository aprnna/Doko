using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
public class RecycleCardBoard : MonoBehaviour
{
    public RecycleMenu Menu;
    public int currentStep = 1;
    public GameObject CursorPaint;
    public GameObject CursorCut;
    public GameObject Cursor;
    private PlayerManager _pm;
    private Animator _mAnimatorCursorPaint;
    private Animator _mAnimatorCursorCut;
    private Animator _mAnimatorCursor;


    [Header("Cutting")]
    public TriggerTap[] TriggerCut;
    public GameObject[] Box;
    public GameObject[] Star;
    public Animator _mAnimatorStar;

    [Header("Strapping")]
    public Trigger TriggerStrapping;
    public GameObject Unstrapped;
    public GameObject Strapped;
    public AudioSource _sfxStrapped;

    [Header("Painting")]
    public Trigger TriggerPaint;
    public GameObject Colored;
    public GameObject CursorPainting;
    private Animator _mAnimatorCursorPainting;

    [Header("Finish")]
    public Canvas CanvasMain;
    public Canvas CanvasFinish;
    public Animator mShine;
    public Animator mRecycleCardboard;
    void Start()
    {
        _pm = PlayerManager.Instance;
        _mAnimatorCursorPaint = CursorPaint.GetComponent<Animator>();
        _mAnimatorCursorCut = CursorCut.GetComponent<Animator>();
        _mAnimatorCursor = Cursor.GetComponent<Animator>();
        StartCoroutine(Cutting());
    }
    IEnumerator Cutting()
    {
        yield return new WaitForSeconds(0.5f);
        string[] trigger = { "Potong1", "Potong2", "Potong3", "Potong4" };
        string[] trigger2 = { "C1", "C2", "C3", "C4" };
        for (int idx = 0; idx < TriggerCut.Length; idx++)
        {
            if(idx > 0)
            {
                Box[idx-1].SetActive(false);
                TriggerCut[idx-1].gameObject.SetActive(false);
            }
            TriggerCut[idx].gameObject.SetActive(true);
            _mAnimatorCursorCut.SetTrigger(trigger2[idx]);
            yield return new WaitUntil(() => TriggerCut[idx].Trigger == true);
            Box[idx+1].SetActive(true);
            Star[idx].SetActive(true);
            _mAnimatorStar.SetTrigger(trigger[idx]);
        }
        Box[Box.Length-2].SetActive(false);
        CursorCut.SetActive(false);
        Debug.Log("NEXT STEP");
        Menu.NextStep(currentStep);
        currentStep++;
        Animator mAnimatorLastBox = Box[Box.Length-1].GetComponent<Animator>();
        mAnimatorLastBox.SetTrigger("BuangKardus");
        StartCoroutine(Strapping());
    }
    IEnumerator Strapping()
    {
        _mAnimatorStar.SetTrigger("Menali");
        yield return new WaitForSeconds(1.5f);
        _mAnimatorStar.gameObject.SetActive(false);
        Unstrapped.SetActive(true);
        TriggerStrapping.gameObject.SetActive(true);
        Cursor.SetActive(true);
        _mAnimatorCursor.SetTrigger("MenaliCursor");
        yield return new WaitUntil(() => TriggerStrapping.trigger == true);
        _sfxStrapped.Play();
        Cursor.SetActive(false);
        Unstrapped.SetActive(false);
        Strapped.SetActive(true);
        Debug.Log("NEXT STEP");
        Menu.NextStep(currentStep);
        currentStep++;
        StartCoroutine(Painting());
    }
    IEnumerator Painting()
    {
        CursorPaint.SetActive(true);
        _mAnimatorCursorPaint.SetTrigger("Mewarnai");
        TriggerPaint.gameObject.SetActive(true);
        yield return new WaitUntil(() => TriggerPaint.trigger == true);
        Unstrapped.SetActive(false);
        Colored.SetActive(true);
        Debug.Log("NEXT STEP");
        Menu.NextStep(currentStep);
        currentStep++;
        CursorPaint.SetActive(false);
        StartCoroutine(Finish());
    }

    IEnumerator Finish()
    {
        CanvasMain.gameObject.SetActive(false);
        CanvasFinish.gameObject.SetActive(true);
        mShine.SetTrigger("Spin");
        mRecycleCardboard.SetTrigger("Scale");
        yield return new WaitForSeconds(1f);
        Debug.Log("Finish");
        _pm.Coin += 80;
    }
}
