using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : IPlayerState
{
    public PlayerController player { get; set; }
    public PlayerMovementStateMachine stateMachine { get; set; }

    public PlayerLandState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public HashSet<PlayerMovementEnums> inputHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.MOVE,
        PlayerMovementEnums.JUMP
    };

    public HashSet<PlayerMovementEnums> logicHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.IDLE
    };

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.70f)
        {
            stateMachine.ChangeStateLogic(PlayerMovementEnums.IDLE);
        }
    }

    public void OnEnter()
    {
        player.InitializeJumpCount();
        player.PlayAnimation("Land");
    }

    public void OnExit()
    {
    }
}
