using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : IPlayerState
{
    public PlayerController player { get; set; }
    public PlayerMovementStateMachine stateMachine { get; set; }

    public PlayerSkillState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public HashSet<PlayerMovementEnums> inputHash { get; } = new HashSet<PlayerMovementEnums>()
    {
    };

    public HashSet<PlayerMovementEnums> logicHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.IDLE
    };

    public void Update()
    {
        player.type.SkillUpdate();
    }

    public void FixedUpdate()
    {
    }

    public void OnEnter()
    {
        player.type.SkillOnEnter();
    }

    public void OnExit()
    {
        player.type.SkillOnExit();
    }
}
