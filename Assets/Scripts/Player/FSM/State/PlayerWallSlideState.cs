using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : IPlayerState
{
    int grabDirection;

    public PlayerController player { get; set; }
    public PlayerMovementStateMachine stateMachine { get; set; }

    public PlayerWallSlideState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public HashSet<PlayerMovementEnums> inputHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.WALLJUMP
    };

    public HashSet<PlayerMovementEnums> logicHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.FALL,
        PlayerMovementEnums.LAND
    };

    public void Update()
    {
        if (player.CheckGround())
        {
            stateMachine.ChangeStateLogic(PlayerMovementEnums.LAND);
            return;
        }

        if (!player.CheckWall() || player.direction == 0 || grabDirection != player.direction)
        {
            stateMachine.ChangeStateLogic(PlayerMovementEnums.FALL);
            return;
        }
    }

    public void FixedUpdate()
    {
        player.WallSlide();
    }

    public void OnEnter()
    {
        player.curJumpCount = 1; // 일단 이렇게 하고 나중에 수정하기 !!!!!

        player.PlayAnimation("WallSlide");
        grabDirection = player.direction;
        player.SetWallJump();
    }

    public void OnExit()
    {
    }
}