using UnityEngine;
using Lean.Touch;

public class DestroyWhenTap: MonoBehaviour
{
    public GameObject ButtonHover;
    public GameObject Instruksi;
    public GameObject Check1;
    private Animator mAnimator;
    private LeanSelectable leanSelectable;

    private void Start()
    {
        // Mendapatkan referensi LeanSelectable pada objek
        leanSelectable = GetComponent<LeanSelectable>();

        // Menambahkan listener ke event OnSelect pada LeanSelectable
        leanSelectable.OnSelect.AddListener(OnTap);

        mAnimator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        // Menghapus listener dari event OnSelect pada LeanSelectable saat script dihancurkan
        leanSelectable.OnSelect.RemoveListener(OnTap);
    }

    private void OnTap(LeanFinger finger)
    {
        // Menghilangkan objek saat ditekan (tap)
        mAnimator.SetTrigger("Buang");
        ButtonHover.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        ButtonHover.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        Instruksi.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        Instruksi.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        Check1.gameObject.SetActive(true);
        Destroy(gameObject,2f);

    }
}