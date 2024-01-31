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
        player.wallJumpCounter -= Time.deltaTime;
        
        if(player.wallJumpCounter < 0f)
        {
            //if (player.rigid.velocity.y < 0f)
                stateMachine.ChangeStateLogic(PlayerMovementEnums.FALL);
        }

            // 점프 (실제 애드포스) > 상승 > 낙하 > 착지 단일 책임 원칙 지금은 점프 스테이트가 2가지 역할을 하고있어 상승중일때 포함되고 점프 애드포스 기능또한 포함되어있어 그니까지금
    }

    public void FixedUpdate()
    {
    }

    public void OnEnter()
    {
        player.isWallJump = true;
        player.PlayAnimation("Jump");
        player.WallJump();
    }

    public void OnExit()
    {
        player.isWallJump = false;
    }
}
