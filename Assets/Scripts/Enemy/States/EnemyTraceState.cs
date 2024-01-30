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
        controller.type.TraceUpdate();
    }
    public void FixedUpdate()
    {
        controller.type.TraceFixedUpdate();
    }
    public void OnEnter()
    {
        controller.type.TraceEnter();
    }
    public void OnExit()
    {
        controller.type.TraceExit();
    }
}
