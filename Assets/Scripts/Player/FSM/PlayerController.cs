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
    public float maxSpeed;
    public float moveSpeed;
    public float jumpForce;

    public int direction = 1;  // 1:R -1:L
    public bool isRight;
    private int maxJumpCount = 2;
    public int curJumpCount;

    private void Start()
    {
        InitializeJumpCount();
    }

    public void InitializeJumpCount()
    {
        curJumpCount = maxJumpCount;
    }

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

    public void SetInputDirection(int dir) // 1, 0, -1 방향 다 받아오는 함수
    {
        direction = dir;
        spriteRenderer.flipX = !isRight; // -1이면 true 반환 -> 뒤집어짐
    }

    public void SetFacingDirection() // -1과 1만 받아오는 함수
    {
        if (direction == 1)
            isRight = true;
        else if (direction == -1)
            isRight = false;
    }

    public void SetMoveSpeed()
    {
        if(Mathf.Abs(rigid.velocity.x) > maxSpeed)
        {
            float dir = isRight ? 1 : -1;
            rigid.velocity = new Vector2(maxSpeed * dir, rigid.velocity.y);
        }
    }

    public void Move()
    {
        rigid.AddForce(Vector2.right * direction * moveSpeed, ForceMode2D.Impulse);
        SetMoveSpeed();
    }

    public void Dodge()
    {
        Vector2 dir = isRight ? Vector2.right : Vector2.left * moveSpeed;
        rigid.AddForce(dir, ForceMode2D.Impulse);
        SetMoveSpeed();
    }

    public void Jump()
    {
        if (curJumpCount <= 0) return;

        curJumpCount--;
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public bool CheckGround()
    {
        Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 0.1f, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 0.1f, LayerMask.GetMask("Platform")); 
       
        if (rayHit.collider != null)
        {
             return true;
        }
        return false;
    }
}
