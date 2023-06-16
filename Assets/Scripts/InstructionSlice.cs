using UnityEngine;
using Lean.Touch;

public class InstructionSlice : MonoBehaviour
{
    private Animator mAnimator;

    private void Start()
    {
        mAnimator = GetComponent<Animator>();
        mAnimator.SetTrigger("Slice");
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
