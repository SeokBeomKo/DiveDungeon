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
        PlayerMovementEnums.DOWNJUMP
    };

    public HashSet<PlayerMovementEnums> logicHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.IDLE,
        PlayerMovementEnums.RISE,
        PlayerMovementEnums.FALL
    };

    int curIndex;

    public void Update()
    {
        player.type.AttackUpdate();
    }

    public void FixedUpdate()
    {
        player.type.AttackFixedUpdate();
    }

    public void OnEnter()
    {
        player.type.AttackOnEnter();
    }

    public void OnExit()
    {
    }
   
}
