using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trigger : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform object1RectTransform; 
    public RectTransform object2RectTransform;
    public AudioSource Sfx;
    public bool trigger = false;

    private Canvas canvas;
    private Vector2 initialPositionObject1;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        initialPositionObject1 = object1RectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Sfx)
        {
            Sfx.Play();
        }
        if (eventData.pointerPress == object1RectTransform.gameObject)
        {
            object1RectTransform.SetAsLastSibling();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        object1RectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Sfx)
        {
            Sfx.Stop();
        }
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject == object2RectTransform.gameObject)
        {
            Debug.Log("Object 1 dropped on Object 2");
            trigger = true;
        }
        object1RectTransform.anchoredPosition = initialPositionObject1;
    }
}
