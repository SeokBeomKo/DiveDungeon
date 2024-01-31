using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("조이스틱 배경 RectTransform")]
    public RectTransform rect;

    [Header("핸들 RectTransform")]
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
        if (touch.magnitude > 1) // magnitude : 벡터의 길이 반환
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
