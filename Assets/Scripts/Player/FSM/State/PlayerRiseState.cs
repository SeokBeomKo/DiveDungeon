using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRiseState : IPlayerState
{
    public PlayerController player { get; set; }
    public PlayerMovementStateMachine stateMachine { get; set; }

    public PlayerRiseState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public HashSet<PlayerMovementEnums> inputHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.DODGE,
        PlayerMovementEnums.ATTACK,
        PlayerMovementEnums.JUMP
    };

    public HashSet<PlayerMovementEnums> logicHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.FALL,
        PlayerMovementEnums.WALLSLIDE
    };

    public void Update()
    {
        if (player.rigid.velocity.y < 0)
        {
            stateMachine.ChangeStateLogic(PlayerMovementEnums.FALL);
            return;
        }

        if (!player.CheckGround() && player.CheckWall() && player.direction != 0 && !player.isWallJump)
        {
            stateMachine.ChangeStateLogic(PlayerMovementEnums.WALLSLIDE);
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
        player.animator.Play("Jump");
    }

    public void OnExit()
    {
    }
}
