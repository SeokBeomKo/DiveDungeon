using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTraceState : IEnemyState
{
    public EnemyController controller {get; set;}
    public EnemyStateMachine stateMachine {get; set;}

    public EnemyTraceState(EnemyStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        controller = _stateMachine.controller;
    }
    public void Update()
    {

    }
    public void FixedUpdate()
    {
        if (controller.FindPlayerInRadius() == null)
        {
            stateMachine.ChangeState(EnemyStateEnums.IDLE);
            return;
        }
        if (Vector2.Distance(controller.FindPlayerInRadius().transform.position, controller.transform.position) <= controller.attackDistance)
        {
            stateMachine.ChangeState(EnemyStateEnums.PREPARATION);
            return;
        }

        controller.Trace();
    }
    public void OnEnter()
    {
        controller.EnterTrace();
    }
    public void OnExit()
    {
        controller.ExitTrace();
    }
}
