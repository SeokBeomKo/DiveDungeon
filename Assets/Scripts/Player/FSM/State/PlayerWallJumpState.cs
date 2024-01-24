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
        PlayerMovementEnums.FALL,
        PlayerMovementEnums.LAND
    };

    // ���� Ű ������ ��� Fall 

    public void Update()
    {
        if(player.CheckGround())
        {
            stateMachine.ChangeStateLogic(PlayerMovementEnums.LAND);
            return;
        }

        if (!player.CheckWall() || player.direction != 0)
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
        player.animator.Play("WallSlide");
    }

    public void OnExit()
    {
    }
}
