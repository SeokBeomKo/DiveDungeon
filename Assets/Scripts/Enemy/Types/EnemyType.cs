using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyType : MonoBehaviour
{
    public EnemyController controller;

    // : >> Idle
    public virtual void IdleUpdate()
    {
        if (controller.CheckPatrol())
        {
            controller.stateMachine.ChangeState(EnemyStateEnums.PATROL);
            return;
        }
        if (controller.FindPlayerInRadius() != null)
        {
            controller.stateMachine.ChangeState(EnemyStateEnums.TRACE);
            return;
        }
    }
    public virtual void IdleFixedUpdate(){}
    public virtual void IdleEnter()
    {
        controller.maxPatrolTime = Random.Range(3,5);
        controller.animator.Play("Idle");
    }
    public virtual void IdleExit(){}
    // : << Idle

    // : >> Patrol
    public virtual void PatrolUpdate()
    {
        if (controller.CheckPatrol())
        {
            controller.stateMachine.ChangeState(EnemyStateEnums.IDLE);
            return;
        }
        if (controller.FindPlayerInRadius() != null)
        {
            controller.stateMachine.ChangeState(EnemyStateEnums.TRACE);
            return;
        }
    }
    public virtual void PatrolFixedUpdate()
    {
        controller.Move();
        controller.ControlSpped();
    }
    public virtual void PatrolEnter()
    {
        controller.maxPatrolTime = Random.Range(2,4);
        controller.curPatrolTime = 0;
        controller.direction = Random.Range(0, 2) * 2 - 1;
        controller.EnterMove();
    }
    public virtual void PatrolExit(){controller.ExitMove();}
    // : << Patrol

    // : >> Trace
    public virtual void TraceUpdate()
    {
        if (controller.FindPlayerInRadius() == null)
        {
            controller.stateMachine.ChangeState(EnemyStateEnums.IDLE);
            return;
        }
        if (Vector2.Distance(controller.FindPlayerInRadius().transform.position, controller.transform.position) <= controller.attackDistance)
        {
            controller.stateMachine.ChangeState(EnemyStateEnums.PREPARATION);
            return;
        }
    }
    public virtual void TraceFixedUpdate()
    {
        controller.direction = controller.FindPlayerInRadius().transform.position.x < transform.position.x ? -1 : 1;

        controller.SetSpriteFlip();
        controller.Move();
        controller.ControlSpped();
    }
    public virtual void TraceEnter(){controller.EnterMove();}
    public virtual void TraceExit(){controller.ExitMove();}
    // : << Trace

    // : >> Preparation
    public virtual void PreparationUpdate()
    {
        if (controller.CheckAttack())
        {
            controller.stateMachine.ChangeState(EnemyStateEnums.ATTACK);
            return;
        }
    }
    public virtual void PreparationFixedUpdate(){}
    public virtual void PreparationEnter(){controller.EnterPreparation();}
    public virtual void PreparationExit(){controller.ExitPreparation();}
    // : << Preparation

    // : >> Attack
    public virtual void AttackUpdate()
    {
        if (controller.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            controller.stateMachine.ChangeState(EnemyStateEnums.IDLE);
            return;
        }
    }
    public virtual void AttackFixedUpdate(){}
    public virtual void AttackEnter(){controller.EnterAttack();}
    public virtual void AttackExit(){}

    public virtual void OnAttack(){}
    // : << Attack

    // : >> Hit
    public virtual void HitUpdate(){}
    public virtual void HitFixedUpdate(){}
    public virtual void HitEnter(){}
    public virtual void HitExit(){}
    // : << Hit

    // : >> Die
    public virtual void DieUpdate(){}
    public virtual void DieFixedUpdate(){}
    public virtual void DieEnter(){}
    public virtual void DieExit(){}
    // : << Die
}
