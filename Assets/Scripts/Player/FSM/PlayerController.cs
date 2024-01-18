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
        spriteRenderer.flipX = !isRight; // -1이면 true 반환 -> 뒤집어짐
    }

    public void SetMoveSpeed()
    {
        if(Mathf.Abs(rigid.velocity.x) > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed * direction, rigid.velocity.y);
        }
    }

    public void Move()
    {
        rigid.AddForce(Vector2.right * direction * moveSpeed, ForceMode2D.Impulse);
        SetMoveSpeed();
    }

    public void Dodge()
    {
        rigid.AddForce(Vector2.right * direction, ForceMode2D.Impulse);
        SetMoveSpeed();
    }

    public void Jump()
    {
        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public bool CheckGround()
    {
        Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform")); 
        if (rayHit.collider != null)
        {
            if (rayHit.distance < 0.5f)
            {
                return true;
            }
        }
        return false;
    }
}
