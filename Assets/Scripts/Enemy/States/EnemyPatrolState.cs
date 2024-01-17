using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    public EnemyController controller {get; set;}
    public EnemyStateMachine stateMachine {get; set;}

    public EnemyPatrolState(EnemyStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        controller = _stateMachine.controller;
    }
    public void Update()
    {
        if (controller.CheckPatrol())
        {
            stateMachine.ChangeState(EnemyStateEnums.IDLE);
            return;
        }
    }
    public void FixedUpdate()
    {
        controller.Patrol();
    }
    public void OnEnter()
    {
        controller.EnterPatrol();
    }
    public void OnExit()
    {
        controller.ExitPatrol();
    }
}
