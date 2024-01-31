using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownJumpState : IPlayerState
{
    public PlayerController player { get; set; }
    public PlayerMovementStateMachine stateMachine { get; set; }

    public PlayerDownJumpState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public HashSet<PlayerMovementEnums> inputHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.DODGE,
        PlayerMovementEnums.ATTACK
    };

    public HashSet<PlayerMovementEnums> logicHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.LAND
    };

    public void Update()
    {
        if (player.CheckGround() && player.isDownJump)
        {
            stateMachine.ChangeStateLogic(PlayerMovementEnums.LAND);
            return;
        }
    }

    public void FixedUpdate()
    {
        player.Move();
        player.SetFacingDirection();
    }

    public void OnEnter()
    {
        player.PlayAnimation("Fall");
        player.IgnoreLayerCoroutine();
    }

    public void OnExit()
    {
        
    }
}
