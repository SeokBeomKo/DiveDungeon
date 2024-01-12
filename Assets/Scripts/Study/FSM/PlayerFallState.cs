using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : IPlayerState
{
    public PlayerController player {get; set;}
    public PlayerMovementStateMachine stateMachine {get; set;}

    public PlayerFallState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        if(player.CheckGround())
        {
            stateMachine.ChangeState(PlayerMovementStateEnums.IDLE);
            return;
        }
    }

    public void OnEnter()
    {
        player.animator.Play("Fall");
    }

    public void OnExit()
    {

    }
}
