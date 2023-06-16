using UnityEngine;
using Lean.Touch;

public class Coloring : MonoBehaviour
{
    [SerializeField]
    private GameObject object1;
    [SerializeField]
    private GameObject object2;
    [SerializeField]
    private Transform colored;
    [SerializeField]
    private Transform unColored;
    public GameObject ButtonHover;
    public GameObject Instruksi;
    public GameObject Check2;

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
            if (hit.collider.gameObject == object2 && fingerDownOnObject1)
            {
                Debug.Log("Finger moved from Object 1 to Object 2");
                // Lakukan logika yang diinginkan ketika finger down dari Object 1 dipindahkan ke Object 2
                unColored.gameObject.SetActive(false);
                colored.gameObject.SetActive(true);
                ButtonHover.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                ButtonHover.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                Instruksi.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                Instruksi.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                Check2.gameObject.SetActive(true);
            }

            fingerDownOnObject1 = false;
        }
    }
}
