using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    public PlayerController player { get; set; }
    public PlayerMovementStateMachine stateMachine { get; set; }

    public PlayerJumpState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public HashSet<PlayerMovementEnums> inputHash { get; } = new HashSet<PlayerMovementEnums>()
    {
    };

    public HashSet<PlayerMovementEnums> logicHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.RISE
    };
    public void Update()
    {
        if(player.rigid.velocity.y > 0)
        {
            stateMachine.ChangeStateLogic(PlayerMovementEnums.RISE);
        }
    }

    public void FixedUpdate()
    {
    }
    
    public void OnEnter()
    {
        player.Jump();
        player.PlayAnimation("Jump");
    }

    public void OnExit()
    {
    }
}
