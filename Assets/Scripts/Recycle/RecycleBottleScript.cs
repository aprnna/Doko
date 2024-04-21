using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class RecycleBottleScript : MonoBehaviour
{
    public RecycleMenu Menu;
    public int currentStep = 1;
    private PlayerManager _pm;

    [Header("Slicing")]
    public Trigger TriggerSlicing;
    public GameObject UnslicedBottle;
    public GameObject SlicedBottle;
    public GameObject SlicingCursor;
    private Animator _mAnimatorSlicing;

    [Header("Coloring")]
    public Trigger TriggerColoring;
    public GameObject ColoredBottle;
    public GameObject ColoringCursor;
    private Animator _mAnimatorColoring;
    private GameObject _unColoredBottle;

    [Header("Decor")]
    public AudioSource AttachDecorSfx;

    [Header("Finish")]
    public Canvas CanvasMain;
    public Canvas CanvasFinish;
    public Animator mRecycleBottleTop;
    public Animator mShine;
    public GameObject RecycleBottle;
    private Animator _mRecycleBottle;


    void Start()
    {
        _pm = PlayerManager.Instance;
        _mAnimatorSlicing = SlicingCursor.GetComponent<Animator>();
        _mAnimatorColoring = ColoringCursor.GetComponent<Animator>();
        _mRecycleBottle = RecycleBottle.GetComponent<Animator>();
        StartCoroutine(Slicing());
    }

    IEnumerator Slicing()
    {
        _mAnimatorSlicing.SetTrigger("CursorSlice");
        yield return new WaitUntil(() => TriggerSlicing.trigger == true);
        SlicingCursor.SetActive(false);
        UnslicedBottle.SetActive(false);
        SlicedBottle.SetActive(true);
        TriggerSlicing.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _unColoredBottle = SlicedBottle.transform.GetChild(0).gameObject;
        GameObject TopCans = SlicedBottle.transform.GetChild(1).gameObject;
        Animator mAnimatorTop = TopCans.GetComponent<Animator>();
        mAnimatorTop.SetTrigger("Buang");
        yield return new WaitForSeconds(0.4f);
        Destroy(TopCans);
        Debug.Log("NEXT STEP");
        Menu.NextStep(currentStep);
        currentStep++;
        StartCoroutine(Coloring());

    }
    IEnumerator Coloring()
    {
        ColoringCursor.SetActive(true);
        _mAnimatorColoring.SetTrigger("CursorColored");
        TriggerColoring.gameObject.SetActive(true);
        yield return new WaitUntil(() => TriggerColoring.trigger == true);
        ColoringCursor.SetActive(false);
        _unColoredBottle.SetActive(false);
        TriggerColoring.gameObject.SetActive(false);
        ColoredBottle.SetActive(true);
        Debug.Log("NEXT STEP");
        Menu.NextStep(currentStep);
        currentStep++;
        StartCoroutine(DecorCans());
    }
    IEnumerator DecorCans()
    {
        GameObject ColoredCansBottom = ColoredBottle.transform.GetChild(0).gameObject;
        yield return new WaitUntil(()=>ColoredCansBottom.transform.childCount != 0);
        GameObject DecorItem = ColoredCansBottom.transform.GetChild(0).gameObject;
        DecorItem.transform.localScale *= 1.2f;
        AttachDecorSfx.Play();
        yield return new WaitForSeconds(AttachDecorSfx.clip.length);
        Instantiate(ColoredCansBottom, RecycleBottle.transform.position, Quaternion.identity, RecycleBottle.transform);
        Debug.Log("NEXT STEP");
        Menu.NextStep(currentStep);
        currentStep++;
        StartCoroutine(Finish());

    }
    IEnumerator Finish()
    {
        yield return new WaitForSeconds(0.2f);
        CanvasMain.gameObject.SetActive(false);
        CanvasFinish.gameObject.SetActive(true);
        _mRecycleBottle.SetTrigger("FadeInScale");
        mShine.SetTrigger("Spin");
        mRecycleBottleTop.SetTrigger("FadeInUp");
        Debug.Log("Finish");
        _pm.Coin += 80;
    }
}
