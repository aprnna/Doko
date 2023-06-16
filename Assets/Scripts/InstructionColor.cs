using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;


public class InstructionColor : MonoBehaviour
{
    private bool hasRun = false;
    private Animator mAnimator;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!hasRun && GameObject.Find("Check").gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            mAnimator.SetTrigger("Colored");
            hasRun = true; // Setel boolean menjadi true agar fungsi hanya dijalankan sekali
        }
    }
    private void OnEnable()
    {
        // Daftarkan fungsi-fungsi yang akan dipanggil saat terjadi input sentuhan
        LeanTouch.OnFingerDown += HandleFingerDown;
    }

    private void OnDisable()
    {
        // Batalkan pendaftaran fungsi-fungsi input sentuhan saat objek dinonaktifkan
        LeanTouch.OnFingerDown -= HandleFingerDown;
    }

    private void HandleFingerDown(LeanFinger finger)
    {
        Destroy(gameObject);
    }
}
