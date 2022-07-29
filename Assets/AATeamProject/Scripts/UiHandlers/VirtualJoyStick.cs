using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoyStick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Image point;
    public float radius = 70f;

    private Vector2 originalPoint = Vector2.zero;
    public RectTransform rectTf;

    private Vector2 direction;

    public static VirtualJoyStick instance;

    private void Start()
    {
        originalPoint = point.rectTransform.position;
    }

    public float GetAxis(string axis)
    {
        var dir = direction / radius;
        switch (axis)
        {
            case "Horizontal":
                return dir.x;
            case "Vertical":
                return dir.y;
        }
        return 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var newPos = eventData.position;
        direction = newPos - originalPoint;
        if (direction.magnitude > radius)
        {
            newPos = originalPoint + direction.normalized * radius;
        }

        point.rectTransform.position = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        direction = Vector2.zero;
        point.rectTransform.position = originalPoint;
    }
}
