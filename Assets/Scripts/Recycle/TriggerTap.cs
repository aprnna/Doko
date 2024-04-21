using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TriggerTap : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    private AudioSource _sfx;
    public bool Trigger;
    private bool _isPlaying;
    // Start is called before the first frame update
    void Start()
    {
        _sfx = GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isPlaying)
        {
            StartCoroutine(TriggerPlay(_sfx));
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isPlaying)
        {
            Trigger = false;
        }
    }
    IEnumerator TriggerPlay(AudioSource sfx)
    {
        sfx.Play();
        yield return new WaitForSeconds(sfx.clip.length);
        Trigger = true;
    }
}
