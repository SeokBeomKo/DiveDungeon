using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public PlayerController player { get; set; }
    public PlayerMovementStateMachine stateMachine { get; set; }


    // 생성자의 주요 목적은 객체를 초기화하고 초기 상태로 설정하는 것
    public PlayerIdleState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            stateMachine.ChangeState(PlayerMovementEnums.MOVE);
            return;
        }
        if (Input.GetButtonDown("Fire3"))
        {
            stateMachine.ChangeState(PlayerMovementEnums.DODGE);
            return;
        }
        if (Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeState(PlayerMovementEnums.JUMPREADY);
            return;
        }
        if (Input.GetMouseButton(0))
        {
            stateMachine.ChangeState(PlayerMovementEnums.ATTACK);
            return;
        }
    }

    public void FixedUpdate()
    {

    }

    public void OnEnter()
    {
        player.animator.Play("Idle");
    }

    public void OnExit()
    {

    }
}