using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    public PlayerController player {get; set;}
    public PlayerMovementStateMachine stateMachine {get; set;}

    public PlayerMoveState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Update()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            stateMachine.ChangeState(PlayerMovementStateEnums.IDLE);
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            stateMachine.ChangeState(PlayerMovementStateEnums.DODGE);
            return;
        }

        player.SetDirection(((int)Input.GetAxisRaw("Horizontal")));
    }

    public void FixedUpdate()
    {
        player.Move();
    }

    public void OnEnter()
    {
        player.animator.Play("Move");
    }
    public void OnExit()
    {
        player.rigid.velocity = Vector3.zero;
    }
}
