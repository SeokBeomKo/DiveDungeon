using System;
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
    public Vector2 dashDirection;
    public float angle;

    private float dragThreshold = 0.7f;
    private bool isDrag = false;

    // >>>>
    public delegate void InputHandler();
    public event InputHandler OnPlayerIdle;
    public event InputHandler OnPlayerMove;
    public event InputHandler OnPlayerDownJump;

    public delegate void InputIntHandler(int value);
    public event InputIntHandler OnPlayerCheckDir;
    // <<<<


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
            // touchPosition �� �̹��� ũ��� ���� ( 0 ~ 1 )
            touchPosition = touchPosition / background.sizeDelta ;

            // -1 ~ 1 
            touchPosition = new Vector2(touchPosition.x * 2 - 1, touchPosition.y * 2 - 1);

            // magnitude : ������ ���� ��ȯ
            if (touchPosition.magnitude > 1)  
            {
                touchPosition = touchPosition.normalized;
            }

            // ���� ���
            angle = -Mathf.Atan2(touchPosition.x, touchPosition.y) * Mathf.Rad2Deg; // - �ٿ��ָ� �ð���⿡�� �ݽð� �������� 
            if (angle < 0) angle += 360;
        }

        // ���� ���̽�ƽ ��Ʈ�ѷ� 
        joystick.anchoredPosition =
            new Vector2(touchPosition.x * background.sizeDelta.x / 2 * dragThreshold, touchPosition.y * background.sizeDelta.y / 2 * dragThreshold);

        dashDirection = touchPosition;

        int inputDirection = MovementDirection(touchPosition);
        OnPlayerCheckDir?.Invoke(inputDirection);
        
        if (inputDirection == 0)
        {
            OnPlayerIdle?.Invoke();
            return;
        }

        OnPlayerMove?.Invoke();
    }

    // ��ġ ���� �� 1ȸ ȣ��
    public void OnPointerUp(PointerEventData eventData)
    {
        joystick.anchoredPosition = Vector2.zero;
        touchPosition = Vector2.zero;

        OnPlayerCheckDir?.Invoke(0);
        OnPlayerIdle?.Invoke();
    }

    public int MovementDirection(Vector2 touchPos)
    {
        if (touchPos.x > 0.1)
            return 1;
        else if (touchPos.x < -0.1)
            return -1;


        return 0;
    }

    public Vector2 GetDashCoordinates()
    {
        return dashDirection;
    }

    public float GetDashAngle()
    {
        return angle;
    }
}