using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : IEnemyState
{
    public EnemyController controller {get; set;}
    public EnemyStateMachine stateMachine {get; set;}

    public EnemyHitState(EnemyStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        controller = _stateMachine.controller;
    }
    public void Update()
    {

    }
    public void FixedUpdate()
    {

    }
    public void OnEnter()
    {

    }
    public void OnExit()
    {

    }
}
