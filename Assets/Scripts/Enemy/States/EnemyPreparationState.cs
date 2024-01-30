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
        controller.type.PreparationUpdate();
    }
    public void FixedUpdate()
    {
        controller.type.PreparationFixedUpdate();
    }
    public void OnEnter()
    {
        controller.type.PreparationEnter();
    }
    public void OnExit()
    {
        controller.type.PreparationExit();
    }
}
