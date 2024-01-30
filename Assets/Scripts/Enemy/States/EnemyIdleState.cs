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
        controller.type.IdleUpdate();
    }
    public void FixedUpdate()
    {

    }
    public void OnEnter()
    {
        controller.type.IdleEnter();
    }
    public void OnExit()
    {

    }
}
