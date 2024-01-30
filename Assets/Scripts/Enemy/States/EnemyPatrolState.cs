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
        controller.type.PatrolUpdate();
    }
    public void FixedUpdate()
    {
        controller.type.PatrolFixedUpdate();
    }
    public void OnEnter()
    {
        controller.type.PatrolEnter();
    }
    public void OnExit()
    {
        controller.type.PatrolExit();
    }
}
