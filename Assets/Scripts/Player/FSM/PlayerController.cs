using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public PlayerMovementStateMachine movementStateMachine;
    public BoxCollider2D boxCollider;

    [Header("이동 관련 값")]
    public float maxSpeed;
    public float moveSpeed;

    [Header("방향 관련 값")]
    public int direction = 1;  // 1:R -1:L
    public bool isRight;

    [Header("점프 관련 값")]
    private int maxJumpCount = 2;
    public float jumpForce;
    public int curJumpCount;
    public bool isDownJump;
    
    [Header("벽 슬라이드 관련 값")]
    public float wallSlidingSpeed;

    [Header("벽 점프 관련 값")]
    public Vector2 wallJumpPower;
    public bool isWallJump;
    public float wallJumpDirection;
    public float jumpingTime;
    public float wallJumpCounter;


    [Header("공격 관련 값")]
    public bool isAttack;

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
        Debug.Log(movementStateMachine.curState);
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

    public int CheckGroundLayer()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position + Vector2.down * 0.1f, Vector2.down, 0.2f);

        if (rayHit.collider == null) return 0;

        return rayHit.collider.gameObject.layer;
    }

    public bool CheckGround()
    {
        //Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 0.1f, 0));

        int layerMask = LayerMask.GetMask("Platform", "Ground");
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.down, 0.1f, layerMask); 
       
        if (rayHit.collider != null)
        {
             return true;
        }
        return false;
    }

    public bool CheckWall()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.right * direction, 0.3f, LayerMask.GetMask("Wall"));
        return rayHit.collider != null;
    }

    public void WallSlide()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, Mathf.Clamp(rigid.velocity.y, -wallSlidingSpeed, float.MaxValue));
    }

    public void SetWallJump()
    {
        wallJumpDirection = -direction;
        wallJumpCounter = jumpingTime;
    }

    public void WallJump()
    {
        curJumpCount--;
        rigid.velocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);

        if (wallJumpDirection != direction)
        {
            isRight = !isRight;
            direction *= -1;
        }
    }

    public void IgnoreLayerCoroutine()
    {
        StartCoroutine(IgnoreLayer());
    }

    IEnumerator IgnoreLayer()
    {
        isDownJump = false;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), true);
        yield return new WaitForSeconds(0.3f);
        isDownJump = true;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), false);
    }
}
