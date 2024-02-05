using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  // 오브젝트의 터치와 관련

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("배경 RectTransform")]
    public RectTransform background;

    [Header("조이스틱 RectTransform")]
    public RectTransform joystick;
    
    public Vector2 touchPosition;
    public Vector2 dashDirection;
    public float angle;

    private float dragThreshold = 0.7f;


    // >>>>
    public delegate void InputHandler();
    public event InputHandler OnIdle;
    public event InputHandler OnMove;
    public event InputHandler OnDownJump;

    public delegate void InputIntHandler(int value);
    public event InputIntHandler OnCheckDirection;

    public delegate void InputVectorHandler(Vector2 vector);
    public event InputVectorHandler OnCheckDashDirection;
    // <<<<


    // 터치 시작 시 1회 호출
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    // 터치 상태일 때 매 프레임 호출
    public void OnDrag(PointerEventData eventData)
    {
        touchPosition = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (background, eventData.position, eventData.pressEventCamera, out touchPosition)) 
        {
            // touchPosition 을 이미지 크기로 나눔 ( 0 ~ 1 )
            touchPosition = touchPosition / background.sizeDelta ;

            // -1 ~ 1 
            touchPosition = new Vector2(touchPosition.x * 2 - 1, touchPosition.y * 2 - 1);

            // magnitude : 벡터의 길이 반환
            if (touchPosition.magnitude > 1)  
            {
                touchPosition = touchPosition.normalized;
            }

            // 각도 계산
            angle = -Mathf.Atan2(touchPosition.x, touchPosition.y) * Mathf.Rad2Deg; // - 붙여주면 시계방향에서 반시계 방향으로 
            if (angle < 0) angle += 360;
        }

        // 가상 조이스틱 컨트롤러 
        joystick.anchoredPosition =
            new Vector2(touchPosition.x * background.sizeDelta.x / 2 * dragThreshold, touchPosition.y * background.sizeDelta.y / 2 * dragThreshold);

        // 대시 좌표
        float x = Mathf.Cos(Mathf.Atan2(touchPosition.y, touchPosition.x));
        float y = Mathf.Sin(Mathf.Atan2(touchPosition.y, touchPosition.x));
        dashDirection = new Vector2(x, y);
        OnCheckDashDirection?.Invoke(dashDirection);

        // 이동 방향
        int inputDirection = MovementDirection(touchPosition);
        OnCheckDirection?.Invoke(inputDirection);

        if (inputDirection == 0)
        {
            if (touchPosition.y < -0.3)
            {
                OnDownJump?.Invoke();
            }

            OnIdle?.Invoke();
            return;
        }

        OnMove?.Invoke();
    }

    // 터치 종료 시 1회 호출
    public void OnPointerUp(PointerEventData eventData)
    {
        joystick.anchoredPosition = Vector2.zero;
        touchPosition = Vector2.zero;

        OnCheckDirection?.Invoke(0);
        OnIdle?.Invoke();
    }

    public int MovementDirection(Vector2 touchPos)
    {
        if (touchPos.x > 0.1f)
            return 1;
        else if (touchPos.x < -0.1f)
            return -1;

        return 0;
    }

    public float GetDashAngle()
    {
        return angle;
    }
}