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
        player.type.RiseUpdate();
    }

    public void FixedUpdate()
    {
        player.type.RiseFixedUpdate();
    }

    public void OnEnter()
    {
        player.type.RiseOnEnter();
    }

    public void OnExit()
    {
    }
}
