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
    };

    public HashSet<PlayerMovementEnums> logicHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.IDLE,
        PlayerMovementEnums.JUMP,
        PlayerMovementEnums.FALL
    };

    int curIndex;

    public void Update()
    {
        player.type.AttackUpdate();
    }

    public void FixedUpdate()
    {
    }

    public void OnEnter()
    {
        player.type.AttackOnEnter();
    }

    public void OnExit()
    {
    }
   
}
