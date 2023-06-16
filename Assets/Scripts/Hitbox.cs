using UnityEngine;
using Lean.Touch;

public class Hitbox : MonoBehaviour
{
    [SerializeField]
    private Transform ObjectSliced;
    [SerializeField]
    private Transform Unsliced;
    private GameObject touchedObject;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

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
        if (finger.StartedOverGui) // Mencegah input jika sentuhan dimulai di atas UI
        {
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(finger.ScreenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            touchedObject = hit.collider.gameObject;
        }
    }

    private void HandleFingerUp(LeanFinger finger)
    {
        if (finger.StartedOverGui) // Mencegah input jika sentuhan dimulai di atas UI
        {
            return;
        }

        if (touchedObject != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(finger.ScreenPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject releasedObject = hit.collider.gameObject;

                if (releasedObject != touchedObject)
                {
                    Debug.Log("Finger released on a different object: " + releasedObject.name);
                    // Lakukan logika yang diinginkan ketika jari diangkat dari objek yang berbeda di sini
                    Unsliced.gameObject.SetActive(false);
                    ObjectSliced.gameObject.SetActive(true);

                    Transform childTransform = ObjectSliced.gameObject.transform.GetChild(0);

                    if (childTransform != null)
                    {
                        // Mengakses komponen di child object
                        Rigidbody childRigidbody = childTransform.GetComponent<Rigidbody>();

                        if (childRigidbody != null)
                        {
                            // Melakukan sesuatu dengan komponen childRigidbody
                             // childRigidbody.AddForce(Vector3.up * 0.5f, ForceMode.VelocityChange);
                        }
                    }
                }
            }

            touchedObject = null;
        }
    }
}
