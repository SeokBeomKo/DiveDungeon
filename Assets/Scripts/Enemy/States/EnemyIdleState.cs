using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    public EnemyController controller {get; set;}
    public EnemyStateMachine stateMachine {get; set;}

    public EnemyIdleState(EnemyStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        controller = _stateMachine.controller;
    }
    public void Update()
    {
        if (controller.CheckPatrol())
        {
            stateMachine.ChangeState(EnemyStateEnums.PATROL);
            return;
        }
        if (controller.FindPlayerInRadius() != null)
        {
            stateMachine.ChangeState(EnemyStateEnums.TRACE);
            return;
        }
    }
    public void FixedUpdate()
    {

    }
    public void OnEnter()
    {
        controller.maxPatrolTime = Random.Range(3,5);
        controller.animator.Play("Idle");
    }
    public void OnExit()
    {

    }
}
