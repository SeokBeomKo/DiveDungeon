using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : IPlayerState
{
    public PlayerController player { get; set; }
    public PlayerMovementStateMachine stateMachine { get; set; }

    public PlayerFallState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public HashSet<PlayerMovementEnums> inputHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.JUMP,
        PlayerMovementEnums.DODGE,
        PlayerMovementEnums.ATTACK
    };

    public HashSet<PlayerMovementEnums> logicHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.LAND,
        PlayerMovementEnums.WALLSLIDE
    };
    public void Update()
    {
        player.type.FallUpdate();
    }

    public void FixedUpdate()
    {
        player.type.FallFixedUpdate();
    }

    public void OnEnter()
    {
        player.type.FallOnEnter();
    }

    public void OnExit()
    {
    }
}
