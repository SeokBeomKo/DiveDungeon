using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : IPlayerState
{
    public PlayerController player { get; set; }
    public PlayerMovementStateMachine stateMachine { get; set; }

    public PlayerDodgeState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public HashSet<PlayerMovementEnums> inputHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        // PlayerMovementEnums.MOVE <- 나중에 받아오기
    };

    public HashSet<PlayerMovementEnums> logicHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.IDLE,
        PlayerMovementEnums.FALL
    };
    public void Update()
    {
        player.type.DodgeUpdate();
    }

    public void FixedUpdate()
    {
        player.type.DodgeFixedUpdate();
    }

    public void OnEnter()
    {
        player.type.DodgeOnEnter();
    }

    public void OnExit()
    {
        player.type.DodgeOnExit();
    }
}
