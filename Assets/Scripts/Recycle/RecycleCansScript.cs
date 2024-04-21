using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class RecycleCansScript : MonoBehaviour
{
    public RecycleMenu Menu;
    public int currentStep = 1;
    private PlayerManager _pm;

    [Header("Slicing")]
    public Trigger TriggerSlicing;
    public GameObject UnslicedCans;
    public GameObject SlicedCans;
    public GameObject SlicingCursor;
    private Animator _mAnimatorSlicing;

    [Header("Coloring")]
    public Trigger TriggerColoring;
    public GameObject ColoredCans;
    public GameObject ColoringCursor;
    private Animator _mAnimatorColoring;
    private GameObject _unColoredCans;

    [Header("Decor")]
    public AudioSource AttachDecorSfx;

    [Header("Finish")]
    public Canvas CanvasMain;
    public Canvas CanvasFinish;
    public Animator mRecycleCansTop;
    public Animator mShine;
    public GameObject RecycleCans;
    private Animator _mRecycleCans;


    void Start()
    {
        _pm = PlayerManager.Instance;
        _mAnimatorSlicing = SlicingCursor.GetComponent<Animator>();
        _mAnimatorColoring = ColoringCursor.GetComponent<Animator>();
        _mRecycleCans = RecycleCans.GetComponent<Animator>();
        StartCoroutine(SlicingCans());
    }

    IEnumerator SlicingCans()
    {
        _mAnimatorSlicing.SetTrigger("CursorSlice");
        yield return new WaitUntil(() => TriggerSlicing.trigger == true);
        SlicingCursor.SetActive(false);
        UnslicedCans.SetActive(false);
        SlicedCans.SetActive(true);
        TriggerSlicing.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _unColoredCans = SlicedCans.transform.GetChild(0).gameObject;
        GameObject TopCans = SlicedCans.transform.GetChild(1).gameObject;
        Animator mAnimatorTop = TopCans.GetComponent<Animator>();
        mAnimatorTop.SetTrigger("Buang");
        yield return new WaitForSeconds(0.4f);
        Destroy(TopCans);
        Debug.Log("NEXT STEP");
        Menu.NextStep(currentStep);
        currentStep++;
        StartCoroutine(ColoringCans());

    }
    IEnumerator ColoringCans()
    {
        ColoringCursor.SetActive(true);
        _mAnimatorColoring.SetTrigger("CursorColored");
        TriggerColoring.gameObject.SetActive(true);
        yield return new WaitUntil(() => TriggerColoring.trigger == true);
        ColoringCursor.SetActive(false);
        _unColoredCans.SetActive(false);
        TriggerColoring.gameObject.SetActive(false);
        ColoredCans.SetActive(true);
        Debug.Log("NEXT STEP");
        Menu.NextStep(currentStep);
        currentStep++;
        StartCoroutine(DecorCans());
    }
    IEnumerator DecorCans()
    {
        GameObject ColoredCansBottom = ColoredCans.transform.GetChild(0).gameObject;
        yield return new WaitUntil(()=>ColoredCansBottom.transform.childCount != 0);
        GameObject DecorItem = ColoredCansBottom.transform.GetChild(0).gameObject;
        DecorItem.transform.localScale *= 2f;
        AttachDecorSfx.Play();
        yield return new WaitForSeconds(AttachDecorSfx.clip.length);
        Instantiate(ColoredCansBottom, RecycleCans.transform.position, Quaternion.identity, RecycleCans.transform);
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
        _mRecycleCans.SetTrigger("FadeInScale");
        mShine.SetTrigger("Spin");
        mRecycleCansTop.SetTrigger("FadeInUp");
        Debug.Log("Finish");
        _pm.Coin += 80;
    }
}
