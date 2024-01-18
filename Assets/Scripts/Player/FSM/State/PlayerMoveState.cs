using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    public PlayerController player { get; set; }
    public PlayerMovementStateMachine stateMachine { get; set; }

    public PlayerMoveState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public HashSet<PlayerMovementEnums> inputHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.IDLE,
        PlayerMovementEnums.JUMPREADY,
        PlayerMovementEnums.DODGE
    };

    public HashSet<PlayerMovementEnums> logicHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.FALL
    };

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        player.Move();
        player.isRight = player.direction == 1;
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
