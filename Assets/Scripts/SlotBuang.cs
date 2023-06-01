using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotBuang : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            // Check if the dragged object is the sampah object
            PieceSampah draggedObject = eventData.pointerDrag.GetComponent<PieceSampah>();
            if (draggedObject != null)
            {
                // Move the dragged object to the slot position
                draggedObject.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                // Destroy the dragged object
                Destroy(draggedObject.gameObject);
            }
        }
    }
}

