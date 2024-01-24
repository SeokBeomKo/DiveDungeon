using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : IPlayerState
{
    public PlayerController player { get; set; }
    public PlayerMovementStateMachine stateMachine { get; set; }

    public PlayerWallJumpState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public HashSet<PlayerMovementEnums> inputHash { get; } = new HashSet<PlayerMovementEnums>()
    {
    };

    public HashSet<PlayerMovementEnums> logicHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.WALLSLIDE,
        PlayerMovementEnums.FALL
    };

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
    }

    public void OnEnter()
    {
        player.animator.Play("Jump");
    }

    public void OnExit()
    {
    }
}
