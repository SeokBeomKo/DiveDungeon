using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityCameraMove : MonoBehaviour
{
    [Header("이동 방향")]
    [SerializeField]    public bool Horizontal; 
    [SerializeField]    public bool Vertical;

    [Header("수치 값")]
    [SerializeField]    public float speed;

    [SerializeField]    public Vector2 minPosition;
    [SerializeField]    public Vector2 maxPosition;

    void Update()
    {
        HorizontalMove();
        VerticalMove();
    }

    void HorizontalMove()
    {
        if (!Horizontal) return;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal"),0,0) * Time.deltaTime * speed;
        }
    }

    void VerticalMove()
    {
        if (!Vertical) return;

        if (Input.GetAxisRaw("Vertical") != 0)
        {
            transform.position += new Vector3(0,Input.GetAxisRaw("Vertical"),0) * Time.deltaTime * speed;
        }
    }
}
