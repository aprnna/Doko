using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DraggableDestination : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            Debug.Log("Dropped");
            GameObject dropped = eventData.pointerDrag;
            DraggableObject draggableItem = dropped.GetComponent<DraggableObject>();
            draggableItem.parentAfterDrag = transform;
        }
    }
}
