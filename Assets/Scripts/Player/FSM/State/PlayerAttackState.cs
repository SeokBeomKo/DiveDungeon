using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IPlayerState
{
    public PlayerController player { get; set; }
    public PlayerMovementStateMachine stateMachine { get; set; }

    public PlayerAttackState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public HashSet<PlayerMovementEnums> inputHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.MOVE,
        PlayerMovementEnums.DODGE
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
    }

    public void OnEnter()
    {
        player.animator.Play("Attack1");
    }

    public void OnExit()
    {
    }
}
