using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public PlayerMovementStateMachine movementStateMachine;

    [Header("수치 값")]
    public float moveSpeed;


    public int direction = 1;  // 1:R -1:L

    private void Update()
    {
        if (movementStateMachine.curState != null)
            movementStateMachine.curState.Update();
    }

    private void FixedUpdate()
    {
        if (movementStateMachine.curState != null)
            movementStateMachine.curState.FixedUpdate();
    }

    public void SetDirection(int dir)
    {
        direction = dir;
        spriteRenderer.flipX = direction == -1; // -1이면 true 반환 -> 뒤집어짐
    }

    public void SetMoveSpeed()
    {
        if(Mathf.Abs(rigid.velocity.x) > moveSpeed)
        {
            rigid.velocity = new Vector2(moveSpeed * direction, rigid.velocity.y);
        }
    }

    public void Move()
    {
        rigid.AddForce(Vector2.right * direction, ForceMode2D.Impulse);
        SetMoveSpeed();
    }

    public void Dodge()
    {
        rigid.AddForce(Vector2.right * direction, ForceMode2D.Impulse);
        SetMoveSpeed();
    }
}
