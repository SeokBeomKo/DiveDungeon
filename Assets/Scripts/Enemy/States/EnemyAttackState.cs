using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    public EnemyController controller {get; set;}
    public EnemyStateMachine stateMachine {get; set;}

    public EnemyAttackState(EnemyStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        controller = _stateMachine.controller;
    }
    public void Update()
    {
        controller.type.AttackUpdate();
    }
    public void FixedUpdate()
    {
    }
    public void OnEnter()
    {
        controller.type.AttackEnter();
    }
    public void OnExit()
    {
        controller.type.AttackExit();
    }
}
