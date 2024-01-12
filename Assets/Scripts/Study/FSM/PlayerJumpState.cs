using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    public PlayerController player {get; set;}
    public PlayerMovementStateMachine stateMachine {get; set;}

    public PlayerJumpState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        if(player.rigid.velocity.y < 0)
        {
            stateMachine.ChangeState(PlayerMovementStateEnums.FALL);
        }
    }

    public void OnEnter()
    {
        player.Jump();
        player.animator.Play("Jump");
    }

    public void OnExit()
    {
        
    }


}
