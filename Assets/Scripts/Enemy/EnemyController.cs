using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyController : MonoBehaviour
{
    public delegate void EnemyChangeState();
    
    [SerializeField]    public Animator             animator;
    [SerializeField]    public Rigidbody2D          rigid;
    [SerializeField]    public SpriteRenderer       spriteRenderer;
    [SerializeField]    public EnemyStateMachine    stateMachine;

    public float maxSpeed;
    public float moveSpeed;

    public int direction;

    [Header("임시 변수")]
    public float maxPatrolTime = 1f;
    public float curPatrolTime;


    private void Update() 
    {
        if (stateMachine != null)
            stateMachine.curState.Update();
    }

    private void FixedUpdate() 
    {
        if (stateMachine != null)
            stateMachine.curState.FixedUpdate();
    }

    public void PlayAnimation(string name)
    {
        animator.Play(name);
    }

    public void ControlSpped()
    {
        if (Mathf.Abs(rigid.velocity.x) > maxSpeed)
        {
            rigid.velocity = new Vector2(direction * maxSpeed, rigid.velocity.y);
        }
    }

    public abstract void Idle();

    public void EnterPatrol()
    {
        maxPatrolTime = Random.Range(2,4);
        direction = Random.Range(0, 2) * 2 - 1;
        EnterMove();
    }

    public void ExitPatrol()
    {
        rigid.velocity = Vector2.zero;
    }

    public abstract void Patrol();
    public abstract void Trace();
    public abstract void Attack();
    public abstract void OnAttack();

    public void EnterMove()
    {
        animator.Play("Move");
        spriteRenderer.flipX = direction == -1;
    }

    // : IdleToPatrol 을 Invoke 나 Coroutine 을 쓰지 않고 시간을 재는 이유 : Idle 상태에서 플레이어 범위 안으로 들어왔을 시 바로 Trace 또는 Attack 상태로 전환
    public bool CheckPatrol()
    {
        if (maxPatrolTime <= curPatrolTime)
        {
            curPatrolTime = 0f;
            return true;
        }
        curPatrolTime += Time.deltaTime;
        return false;
    }
    
}
