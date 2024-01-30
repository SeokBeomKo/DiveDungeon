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
        if (controller.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            stateMachine.ChangeState(EnemyStateEnums.IDLE);
            return;
        }
    }
    public void FixedUpdate()
    {
    }
    public void OnEnter()
    {
        controller.EnterAttack();
    }
    public void OnExit()
    {
        controller.ExitAttack();
    }
}
