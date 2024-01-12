using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public PlayerController player {get; set;}
    public PlayerMovementStateMachine stateMachine {get; set;}

    public PlayerIdleState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            stateMachine.ChangeState(PlayerMovementStateEnums.MOVE);
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            stateMachine.ChangeState(PlayerMovementStateEnums.DODGE);
            return;
        }
        if(Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeState(PlayerMovementStateEnums.JUMP);
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
