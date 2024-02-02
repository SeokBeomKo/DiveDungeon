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
    public float angle;

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
            // touchPosition 을 이미지 크기로 나눔
            touchPosition.x = touchPosition.x / background.sizeDelta.x ;
            touchPosition.y = touchPosition.y / background.sizeDelta.y;

            touchPosition = new Vector2(touchPosition.x * 2 - 1, touchPosition.y * 2 - 1);

            // 각도 계산
            angle = -Mathf.Atan2(touchPosition.x, touchPosition.y) * Mathf.Rad2Deg; // - 붙여주면 시계방향에서 반시계 방향으로 
            if (angle < 0) angle += 360;

            // magnitude : 벡터의 길이 반환
            if (touchPosition.magnitude > 1)  
            {
                touchPosition = touchPosition.normalized;
            }
        }

        // 가상 조이스틱 컨트롤러 
        joystick.anchoredPosition =
            new Vector2(touchPosition.x * background.sizeDelta.x / 2, touchPosition.y * background.sizeDelta.y / 2);

        //Debug.Log(touchPosition);
    }

    // 터치 종료 시 1회 호출
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