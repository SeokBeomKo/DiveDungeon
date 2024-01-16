/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public float rollSpeed;

    private float rollTime;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Update() // 단발적인 키 입력
    {
        // Jump
        if (Input.GetButtonDown("Jump") && (anim.GetInteger("isJumping") == 0))
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            StartCoroutine(RollCoroutine());
           

            *//*anim.SetTrigger("isRolling");
            
            Vector2 rollDirection = spriteRenderer.flipX ? Vector2.left : Vector2.right;
            rigid.velocity = rollDirection * rollSpeed;
*//*
            //rigid.AddForce(rollDirection * rollSpeed);
        }

        // Attack
        if (Input.GetMouseButton(0))
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                anim.SetTrigger("isAttack");
            }
        }

        // Direction Sprite
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            anim.SetBool("isWalking", true);
        }

        // Stop Speed
        if(Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = Vector2.zero;
            anim.SetBool("isWalking", false);
        }

    }

    void FixedUpdate() // 지속적인 키 입력
    {
        // Move Speed
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        
        // velocity : 리지드바디의 현재 속도
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
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform")); // rayHit : 빔을 쏘고 거기에 맞은 오브젝트에 대한 정보

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetInteger("isJumping", 0);
                }
            }
        }
    }

    private IEnumerator RollCoroutine()
    {
        //anim.SetTrigger("isRolling");
        anim.SetBool("isRolling", true);
        //yield return new WaitForSeconds(0.2f);
        maxSpeed *= 2f;
        Vector2 rollDirection = spriteRenderer.flipX ? Vector2.left : Vector2.right;
        rigid.velocity = rollDirection * rollSpeed;

        yield return new WaitForSeconds(0.2f);
        anim.SetBool("isRolling", false);
        maxSpeed *= 0.5f;
    }
}
*/