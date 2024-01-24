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

    [Header("�̵� ���� ��")]
    public float maxSpeed;
    public float moveSpeed;

    [Header("���� ���� ��")]
    public int direction = 1;  // 1:R -1:L
    public bool isRight;

    [Header("���� ���� ��")]
    private int maxJumpCount = 2;
    public float jumpForce;
    public int curJumpCount;
    public bool isDownJump;
    
    [Header("�� ���� ���� ��")]
    public bool isWallSliding;
    public float wallSlidingSpeed;

    [Header("���� ���� ��")]
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
        if (movementStateMachine.curState != null)
            movementStateMachine.curState.Update();
    }

    private void FixedUpdate()
    {
        if (movementStateMachine.curState != null)
            movementStateMachine.curState.FixedUpdate();
    }

    public void SetInputDirection(int dir) // 1, 0, -1 ���� �� �޾ƿ��� �Լ�
    {
        direction = dir;
        spriteRenderer.flipX = !isRight; // -1�̸� true ��ȯ -> ��������
    }

    public void SetFacingDirection() // -1�� 1�� �޾ƿ��� �Լ�
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

    public bool CheckWall()
    {
        // ������ ����, ������ ���� ũ��, ����, ���ڰ� �����Ǵ� ����, �Ÿ�
        RaycastHit2D rayHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right * direction, 0.1f, LayerMask.GetMask("Wall"));
        return rayHit.collider != null;
    }

    public void WallSlide()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, Mathf.Clamp(rigid.velocity.y, -wallSlidingSpeed, float.MaxValue));
    }

    public void IgnoreLayerCoroutine()
    {
        StartCoroutine(IgnoreLayer());
    }

    IEnumerator IgnoreLayer()
    {
        Debug.Log("A");
        isDownJump = false;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), true);
        yield return new WaitForSeconds(0.2f);
        isDownJump = true;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), false);
    }
}
