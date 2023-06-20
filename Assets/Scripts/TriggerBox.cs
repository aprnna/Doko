using UnityEngine;
using Lean.Touch;

public class TriggerBox : MonoBehaviour
{
    [SerializeField]
    private GameObject object1;
    [SerializeField]
    private GameObject object2;
    [SerializeField]
    private Transform After;
    [SerializeField]
    private Transform Before;
    public GameObject ButtonHover;
    public GameObject Instruksi;
    public GameObject Check;
    public int Child;
    public AudioSource Sfx;


    private bool fingerDownOnObject1 = false;
   
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
            if (hit.collider.gameObject == object1)
            {
                fingerDownOnObject1 = true;
                Sfx.Play();
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

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(finger.ScreenPosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == object2 && fingerDownOnObject1)
            {
                Debug.Log("Finger moved from Object 1 to Object 2");
                // Lakukan logika yang diinginkan ketika finger down dari Object 1 dipindahkan ke Object 2
                Before.gameObject.SetActive(false);
                After.gameObject.SetActive(true);
                ButtonHover.gameObject.transform.GetChild(Child-1).gameObject.SetActive(false);
                ButtonHover.gameObject.transform.GetChild(Child).gameObject.SetActive(true);
                Instruksi.gameObject.transform.GetChild(Child-1).gameObject.SetActive(false);
                Instruksi.gameObject.transform.GetChild(Child).gameObject.SetActive(true);
                Check.gameObject.SetActive(true);
                Sfx.Stop();
            }

            fingerDownOnObject1 = false;
        }
    }
}
