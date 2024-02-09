using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public PlayerMovementStateMachine stateMachine;
    public PlayerType type;
    public GhostEffect ghost;

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
    
    [Header("대시 관련 값")]
    public bool isDash;
    public float dashTime;
    public Vector2 dashDirection;

    [Header("벽 슬라이드 관련 값")]
    public float wallSlidingSpeed;

    [Header("벽 점프 관련 값")]
    public Vector2 wallJumpPower;
    public bool isWallJump;
    public float wallJumpDirection;
    public float jumpingTime;
    public float wallJumpCounter;

    [Header("스킬 관련 값")]
    public bool isSkillOn = false;

    [Header("공격 관련 값")]
    public bool isAttack;

    private void Start()
    {
        InitializeJumpCount();
        dashDirection = new Vector2(0, 1);
    }

    private void Update()
    {
        // Debug.Log(stateMachine.curState);
        if (stateMachine.curState != null)
            stateMachine.curState.Update();
    }

    private void FixedUpdate()
    {
        if (stateMachine.curState != null)
            stateMachine.curState.FixedUpdate();
    }


    // >>>
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

    public void SetDashSpeed()
    {
        if(Mathf.Abs(rigid.velocity.x) > maxSpeed || Mathf.Abs(rigid.velocity.y) > maxSpeed)
        {
            rigid.velocity = new Vector2(dashDirection.x * maxSpeed, dashDirection.y * maxSpeed);
        }
    }

    public void SetDashDirection(Vector2 direction)
    {
        dashDirection = direction;
        dashDirection.Normalize();
    }

    public void Move()
    {
        rigid.AddForce(Vector2.right * direction * moveSpeed, ForceMode2D.Impulse);
        SetMoveSpeed();
    }

    public void Dodge()
    {
        Vector2 dir = isRight ? Vector2.right : Vector2.left;
        rigid.AddForce(dir * moveSpeed, ForceMode2D.Impulse);
        SetMoveSpeed();
    }

    public void Dash()
    {
        Vector2 dir = isRight ? Vector2.right : Vector2.left;
        rigid.velocity = new Vector2(dir.x * moveSpeed, 0f);
        SetMoveSpeed();
    }

    public void eightWayDash()
    {
        rigid.velocity = dashDirection * moveSpeed;
        SetDashSpeed();
    }

    public void Jump()
    {
        if (curJumpCount <= 0) return;

        curJumpCount--;
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void InitializeJumpCount()
    {
        curJumpCount = maxJumpCount;
    }

    public int CheckGroundLayer()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position + Vector2.down * 0.1f, Vector2.down, 0.2f);

        if (rayHit.collider == null) return 0;

        return rayHit.collider.gameObject.layer;
    }

    public bool CheckGround()
    {
        int layerMask = LayerMask.GetMask("Platform", "Ground");
        RaycastHit2D rayHit = Physics2D.BoxCast(rigid.position, new Vector2(0.5f, 0.1f), 0f, Vector2.down, 0.1f, layerMask);

        return rayHit.collider != null;
    }

    public bool CheckWall()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.right * direction, 0.3f, LayerMask.GetMask("Wall"));
        
        if (rayHit.collider != null)
        {
             return true;
        }
        return false;
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

    public void PlayAnimation(string name)
    {
        animator.Play(name);
    }
}
