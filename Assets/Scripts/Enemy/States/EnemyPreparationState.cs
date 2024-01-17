using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPreparationState : IEnemyState
{
    public EnemyController controller {get; set;}
    public EnemyStateMachine stateMachine {get; set;}

    public EnemyPreparationState(EnemyStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        controller = _stateMachine.controller;
    }
    public void Update()
    {
        if (controller.CheckAttack())
        {
            stateMachine.ChangeState(EnemyStateEnums.ATTACK);
            return;
        }
    }
    public void FixedUpdate()
    {
    }
    public void OnEnter()
    {
        controller.EnterPreparation();
    }
    public void OnExit()
    {
        controller.ExitPreparation();
    }
}
