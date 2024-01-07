using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float rollSpeed;
    public float jumpPower;
    public float defaultTime;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Update() // �ܹ����� Ű �Է�
    {
        // Jump
        if(Input.GetButtonDown("Jump") && (anim.GetInteger("isJumping") == 0))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetInteger("isJumping", 1);
        }
        else if(Input.GetButtonDown("Jump") && (anim.GetInteger("isJumping") == 1))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetInteger("isJumping", 2);
        }

        // Roll
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetTrigger("isRolling");

            Vector2 rollDirection = spriteRenderer.flipX ? Vector2.left : Vector2.right;
            rigid.AddForce(rollDirection * rollSpeed);
        }

        // Stop Speed
        if(Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = Vector2.zero;
            anim.SetBool("isWalking", false);
        }

        // Direction Sprite
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            anim.SetBool("isWalking", true);
        }
    }

    void FixedUpdate() // �������� Ű �Է�
    {
        // Move Speed
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        
        // velocity : ������ٵ��� ���� �ӵ�
        // Max Speed
        if(rigid.velocity.x > maxSpeed) // Right Max Speed
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if(rigid.velocity.x < maxSpeed * (-1)) // Left Max Speed
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        // Landing Platform
        if(rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform")); // rayHit : ���� ��� �ű⿡ ���� ������Ʈ�� ���� ����

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetInteger("isJumping", 0);
                }
            }
        }
    }

    
}
