using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("���̽�ƽ ��� RectTransform")]
    public RectTransform rect;

    [Header("�ڵ� RectTransform")]
    public RectTransform handle;
    
    private Vector2 touch = Vector2.zero;
    
    public float radius;
    private float angle;

    private void Start()
    {
        radius = rect.sizeDelta.x * 0.5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        touch = (eventData.position - rect.anchoredPosition) / radius;
        if (touch.magnitude > 1) // magnitude : ������ ���� ��ȯ
        {
            touch = touch.normalized;
        }
        handle.anchoredPosition = touch * radius;

        angle = Mathf.Atan2(touch.y, touch.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        Debug.Log((int)angle);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handle.anchoredPosition = Vector2.zero;
    }

    public int GetAngle()
    {
        return (int)angle;
    }
}
