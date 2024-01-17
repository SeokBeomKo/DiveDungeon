using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyController : MonoBehaviour
{
    [SerializeField]    public Animator             animator;
    [SerializeField]    public Rigidbody2D          rigid;
    [SerializeField]    public SpriteRenderer       spriteRenderer;
    [SerializeField]    public EnemyStateMachine    stateMachine;

    public float maxSpeed;
    public float moveSpeed;

    public float detectionRange;    // 감지 범위
    public float attackDistance;    // 공격 거리

    public int direction;

    [Header("임시 변수")]
    public float maxPatrolTime = 1f;
    public float curPatrolTime;

    public float maxPreparationTime = 1f;
    public float curPreparationTime;


    private void Update() 
    {
        Debug.Log(stateMachine.curState);
        if (stateMachine != null)
            stateMachine.curState.Update();
    }

    private void FixedUpdate() 
    {
        if (stateMachine != null)
            stateMachine.curState.FixedUpdate();
    }

    public void SetSpriteFlip()
    {
        spriteRenderer.flipX = direction == -1;
    }

    public void ControlSpped()
    {
        if (Mathf.Abs(rigid.velocity.x) > maxSpeed)
        {
            rigid.velocity = new Vector2(direction * maxSpeed, rigid.velocity.y);
        }
    }

    public abstract void Idle();

    public abstract void Patrol();
    public void EnterPatrol()
    {
        maxPatrolTime = Random.Range(2,4);
        curPatrolTime = 0;
        direction = Random.Range(0, 2) * 2 - 1;
        EnterMove();
    }
    public void ExitPatrol()
    {
        ExitMove();
    }

    public abstract void Trace();
    public void EnterTrace()
    {
        EnterMove();
    }
    public void ExitTrace()
    {
        ExitMove();
    }

    public abstract void Preparation();
    public void EnterPreparation()
    {
        animator.Play("Preparation");
    }
    public void ExitPreparation()
    {
        curPreparationTime = 0;
    }   

    public abstract void Attack();
    public void EnterAttack()
    {
        animator.Play("Attack");
    }
    public void ExitAttack()
    {

    }
    public abstract void OnAttack();

    public abstract void Move();
    public void EnterMove()
    {
        animator.Play("Move");
        SetSpriteFlip();
    }
    public void ExitMove()
    {
        rigid.velocity = Vector2.zero;
    }

    

    public GameObject FindPlayerInRadius()
    {
        int layerMask = LayerMask.GetMask("Player");

        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, detectionRange, layerMask);

        if (hitCollider != null)
        {
            return hitCollider.gameObject;
        }

        return null;
    }

    public bool CheckAttack()
    {
        if (maxPreparationTime <= curPreparationTime)
        {
            return true;
        }
        curPreparationTime += Time.deltaTime;
        return false;
    }

    // : IdleToPatrol 을 Invoke 나 Coroutine 을 쓰지 않고 시간을 재는 이유 : Idle 상태에서 플레이어 범위 안으로 들어왔을 시 바로 Trace 또는 Attack 상태로 전환
    public bool CheckPatrol()
    {
        if (maxPatrolTime <= curPatrolTime)
        {
            return true;
        }
        curPatrolTime += Time.deltaTime;
        return false;
    }
    
}
