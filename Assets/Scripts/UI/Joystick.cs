using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  // ������Ʈ�� ��ġ�� ����

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("��� RectTransform")]
    public RectTransform background;

    [Header("���̽�ƽ RectTransform")]
    public RectTransform joystick;
    
    public Vector2 touchPosition;
    public float angle;

    // ��ġ ���� �� 1ȸ ȣ��
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    // ��ġ ������ �� �� ������ ȣ��
    public void OnDrag(PointerEventData eventData)
    {
        touchPosition = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (background, eventData.position, eventData.pressEventCamera, out touchPosition)) 
        {
            // touchPosition �� �̹��� ũ��� ����
            touchPosition.x = touchPosition.x / background.sizeDelta.x ;
            touchPosition.y = touchPosition.y / background.sizeDelta.y;

            touchPosition = new Vector2(touchPosition.x * 2 - 1, touchPosition.y * 2 - 1);

            // ���� ���
            angle = -Mathf.Atan2(touchPosition.x, touchPosition.y) * Mathf.Rad2Deg; // - �ٿ��ָ� �ð���⿡�� �ݽð� �������� 
            if (angle < 0) angle += 360;

            // magnitude : ������ ���� ��ȯ
            if (touchPosition.magnitude > 1)  
            {
                touchPosition = touchPosition.normalized;
            }
        }

        // ���� ���̽�ƽ ��Ʈ�ѷ� 
        joystick.anchoredPosition =
            new Vector2(touchPosition.x * background.sizeDelta.x / 2, touchPosition.y * background.sizeDelta.y / 2);

        //Debug.Log(touchPosition);
    }

    // ��ġ ���� �� 1ȸ ȣ��
    public void OnPointerUp(PointerEventData eventData)
    {
        joystick.anchoredPosition = Vector2.zero;
        //touchPosition = Vector2.zero;
    }

    public float GetHorizontal()
    {
        return touchPosition.x;
    }

    public float GetVertical()
    {
        return touchPosition.y;
    }

    public float GetAngle()
    {
        return angle;
    }
}