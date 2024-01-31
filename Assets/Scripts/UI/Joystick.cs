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
    private float widthHalf;

    private void Start()
    {
        widthHalf = rect.sizeDelta.x * 0.5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        touch = (eventData.position - rect.anchoredPosition) / widthHalf;
        if (touch.magnitude > 1) // magnitude : ������ ���� ��ȯ
        {
            touch = touch.normalized;
        }
        handle.anchoredPosition = touch * widthHalf;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handle.anchoredPosition = Vector2.zero;
    }
}
