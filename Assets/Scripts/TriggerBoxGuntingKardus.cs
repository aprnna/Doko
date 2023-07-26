using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class TriggerBoxGuntingKardus : MonoBehaviour
{
    private GameObject startPoint;

    public GameObject objectsKardus;
    public GameObject[] trigger;
    public GameObject Star;
    public GameObject lastKardus;
    public GameObject Menali;
    public GameObject MenaliCursor;
    private Animator mAnimator;
    private Animator mAnimator2;

    private bool fingerDownOnObject = false;
    private bool _isPlayingAnim;

    public GameObject ButtonHover;
    public GameObject Instruksi;
    public GameObject Check;
    public int Child;

    public AudioSource sfx;

    private bool passedObject2 = false;
    private bool passedObject3 = false;
    private bool passedObject4 = false;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = Star.GetComponent<Animator>();
        mAnimator2 = lastKardus.GetComponent<Animator>();
        startPoint = trigger[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        LeanTouch.OnFingerDown += HandleFingerDown;
        LeanTouch.OnFingerUp += HandleFingerUp;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= HandleFingerDown;
        LeanTouch.OnFingerUp -= HandleFingerUp;
    }
    private void HandleFingerDown(LeanFinger finger)
    {
        if (finger.StartedOverGui)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(finger.ScreenPosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == startPoint)
            {
                fingerDownOnObject = true;
            }
        }
    }

    private void HandleFingerUp(LeanFinger finger)
    {
        if (finger.StartedOverGui)
        {
            return;
        }
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(finger.ScreenPosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == trigger[0] && fingerDownOnObject)
            {
                if (!passedObject2)
                {
                    passedObject2 = true;
                    startPoint = trigger[1];
                    NextStepWithSound(1, "Potong1");
                    
                }
            }
            else if (hit.collider.gameObject == trigger[1] && fingerDownOnObject && passedObject2)
            {
                if (!passedObject3)
                {
                    passedObject3 = true;
                    startPoint = trigger[2];
                    NextStepWithSound(2, "Potong2");


                }
            }
            else if (hit.collider.gameObject == trigger[2] && fingerDownOnObject && passedObject2 && passedObject3)
            {
                if (!passedObject4)
                {
                    passedObject4 = true;
                    startPoint = trigger[3];
                    NextStepWithSound(3, "Potong3");


                }
            }
            else if (hit.collider.gameObject == trigger[3] && fingerDownOnObject && passedObject2 && passedObject3 && passedObject4)
            {
                // this.enabled = false;
                NextStepWithSound(4, "Potong4");
                

            }
            fingerDownOnObject = false;
        }
    }
    public void DestroyStar()
    {
        if (!_isPlayingAnim)
        {
            StartCoroutine(PlayAnimasiAndChangeObject());
        }
    }

    private IEnumerator PlayAnimasiAndChangeObject()
    {
        _isPlayingAnim = true;
        mAnimator.SetTrigger("Menali");
        yield return new WaitForSeconds(2);
        Star.SetActive(false);
        Menali.SetActive(true);
        MenaliCursor.SetActive(true);
    }
    public void NextStepWithSound(int kardus,string triggers)
    {
        StartCoroutine(PlayAudioAndNextStep(kardus, triggers));
    }

    private IEnumerator PlayAudioAndNextStep(int kardus, string triggers)
    {
        sfx.Play();
        yield return new WaitForSeconds(sfx.clip.length);
        objectsKardus.transform.GetChild(kardus-1).gameObject.SetActive(false);
        objectsKardus.transform.GetChild(kardus).gameObject.SetActive(true);
        mAnimator.SetTrigger(triggers);
        if (kardus == 4 )
        {
            ButtonHover.gameObject.transform.GetChild(Child - 1).gameObject.SetActive(false);
            ButtonHover.gameObject.transform.GetChild(Child).gameObject.SetActive(true);
            Instruksi.gameObject.transform.GetChild(Child - 1).gameObject.SetActive(false);
            Instruksi.gameObject.transform.GetChild(Child).gameObject.SetActive(true);
            Check.gameObject.SetActive(true);
            mAnimator2.SetTrigger("BuangKardus");
            DestroyStar();
        }
        
    }
}
