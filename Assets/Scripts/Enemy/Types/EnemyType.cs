using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyType : MonoBehaviour
{
    EnemyController controller;

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
    public virtual void PatrolUpdate(){}
    public virtual void PatrolFixedUpdate(){}
    public virtual void PatrolEnter(){}
    public virtual void PatrolExit(){}
    // : << Patrol

    // : >> Trace
    public virtual void TraceUpdate(){}
    public virtual void TraceFixedUpdate(){}
    public virtual void TraceEnter(){}
    public virtual void TraceExit(){}
    // : << Trace

    // : >> Preparation
    public virtual void PreparationUpdate(){}
    public virtual void PreparationFixedUpdate(){}
    public virtual void PreparationEnter(){}
    public virtual void PreparationExit(){}
    // : << Preparation

    // : >> Attack
    public virtual void AttackUpdate(){}
    public virtual void AttackFixedUpdate(){}
    public virtual void AttackEnter(){}
    public virtual void AttackExit(){}
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
