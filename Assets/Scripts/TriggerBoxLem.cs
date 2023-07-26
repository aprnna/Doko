using UnityEngine;
using Lean.Touch;
using UnityEngine.UI;

public class TriggerBoxLem : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objects; 
    [SerializeField]
    private Transform After;
    public GameObject ButtonHover;
    public GameObject Instruksi;
    public GameObject Check;
    public GameObject cursorDaunKering;
    public int Child;
    public AudioSource Sfx;

    public GameObject cursorLem;
    private Animator mAnimator;

    private bool fingerDownOnObject = false;

    private GameObject startPoint;
    private string trigger;
    private bool passedObject2 = false;
    private bool passedObject3 = false;
    private bool passedObject4 = false;

    private void Start()
    {
        mAnimator = cursorLem.GetComponent<Animator>();
        trigger = "LemKanan";
        mAnimator.SetTrigger(trigger);
        startPoint = objects[0];
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
                Sfx.Play();
                cursorLem.SetActive(false);
            }
        }
    }

    private void HandleFingerUp(LeanFinger finger)
    {
        Sfx.Stop();
        if (finger.StartedOverGui)
        {
            return;
        }
        cursorLem.SetActive(true);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(finger.ScreenPosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == objects[1] && fingerDownOnObject)
            {
                if (!passedObject2)
                {
                    passedObject2 = true;
                    startPoint = objects[1];
                    trigger = "LemBawah";
                }
            }
            else if (hit.collider.gameObject == objects[2] && fingerDownOnObject && passedObject2)
            {
                if (!passedObject3)
                {
                    passedObject3 = true;
                    startPoint = objects[2];
                    cursorLem.SetActive(true);
                    trigger = "LemKiri";
                }
            }
            else if (hit.collider.gameObject == objects[3] && fingerDownOnObject && passedObject2 && passedObject3)
            {
                if (!passedObject4)
                {
                    passedObject4 = true;
                    startPoint = objects[3];
                    cursorLem.SetActive(true);
                    trigger = "LemAtas";
                }
            }
            else if (hit.collider.gameObject == objects[0] && fingerDownOnObject && passedObject2 && passedObject3 && passedObject4)
            {
                Destroy(cursorLem);
                After.gameObject.SetActive(true);
                ButtonHover.gameObject.transform.GetChild(Child - 1).gameObject.SetActive(false);
                ButtonHover.gameObject.transform.GetChild(Child).gameObject.SetActive(true);
                Instruksi.gameObject.transform.GetChild(Child - 1).gameObject.SetActive(false);
                Instruksi.gameObject.transform.GetChild(Child).gameObject.SetActive(true);
                Check.gameObject.SetActive(true);
                Sfx.Stop();
                DaunKering.LemDone = true;
                cursorDaunKering.SetActive(true);
                this.enabled = false;
            }
            fingerDownOnObject = false;
        }
        mAnimator.SetTrigger(trigger);
    }
}
