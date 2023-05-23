using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSampah : MonoBehaviour
{
    private bool _dragging;

    private Vector2 _offset,_originalPosition;

    void Awake()
    {
        _originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_dragging) return;

        var mousePosition = GetMousePos();

        transform.position = mousePosition - _offset;
    }

    private void OnMouseDown()
    {
        _dragging = true;
        _offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnMouseUp()
    {
        transform.position = _originalPosition;
            _dragging = false;
    }

    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
