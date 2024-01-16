/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public PlayerMovementStateMachine moveStateMachine;

    [Header("수치 값")]
    public float maxSpeed;
    public float moveSpeed;
    public float dodgeSpeed;
    public float jumpForce;

    public int direction = 1;

    private void Update()
    {
        if (moveStateMachine.curState != null)
            moveStateMachine.curState.Update();
        Debug.Log(moveStateMachine.curState);
    }

    private void FixedUpdate()
    {
        if (moveStateMachine.curState != null)
            moveStateMachine.curState.FixedUpdate();
    }

    public void SetDirection(int _direction)
    {
        direction = _direction;
        spriteRenderer.flipX = direction == -1;
    }

    public void MaxSpeed()
    {
        if (Mathf.Abs(rigid.velocity.x) > maxSpeed) // Right Max Speed
        {
            rigid.velocity = new Vector2(maxSpeed * direction, rigid.velocity.y);
        }
    }

    public void Move()
    {
        rigid.AddForce(Vector2.right * direction, ForceMode2D.Impulse);

        MaxSpeed();
    }

    public void Dodge()
    {
        rigid.AddForce(Vector2.right * direction, ForceMode2D.Impulse);

        MaxSpeed();
    }

    public void Jump()
    {
        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public bool CheckGround()
    {
        Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform")); // rayHit : ���� ��� �ű⿡ ���� ������Ʈ�� ���� ����

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
*/