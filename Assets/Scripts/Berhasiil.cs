using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berhasiil : MonoBehaviour
{
    public GameObject Check;
    public GameObject Background;
    public GameObject Finishing;
    public Canvas CanvasMain;
    public Canvas CanvasBerhasil;
    private bool Done = true;
    private bool _isPlaying;

    private Animator mAnimator;
    private Animator mAnimator2;
    private Animator mAnimator3;

    private void Start()
    {
        mAnimator = Background.transform.GetChild(0).GetComponent<Animator>();
        mAnimator2 = Background.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        mAnimator3 = Background.transform.GetChild(0).GetChild(1).GetComponent<Animator>();
    }
    void Update()
    {
        bool allChildrenActive = CheckAllChildrenActiveStatus();
        if (allChildrenActive && Done)
        {
            
            ChangeSceneWithSound(Background);
            GameObject newLine = Instantiate(Finishing, Background.transform.GetChild(0));
            Done = false; 
        }
    }
    public void ChangeSceneWithSound(GameObject Background)
    {
        if (!_isPlaying)
        {
            StartCoroutine(PlayAudioAndChangeScene(Background));
        }
    }

    private IEnumerator PlayAudioAndChangeScene(GameObject Background)
    {
        _isPlaying = true;
        var audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        CanvasMain.gameObject.SetActive(false);
        CanvasBerhasil.gameObject.SetActive(true);
        Background.SetActive(true);
        Finishing.SetActive(false);
        GameObject.Find("Lubang").SetActive(false);
        mAnimator.SetTrigger("FadeInScale");
        mAnimator2.SetTrigger("FadeInUp");
        mAnimator3.SetTrigger("Spin");

    }
    private bool CheckAllChildrenActiveStatus()
    {
        int childCount = Check.gameObject.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = Check.gameObject.transform.GetChild(i);


            if (!child.gameObject.activeSelf)
            {
                return false;
            }
        }

        return true;
    }
}
