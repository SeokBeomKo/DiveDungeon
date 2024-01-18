using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpReadyState : IPlayerState
{
    public PlayerController player { get; set; }
    public PlayerMovementStateMachine stateMachine { get; set; }

    public PlayerJumpReadyState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public HashSet<PlayerMovementEnums> inputHash { get; } = new HashSet<PlayerMovementEnums>()
    {
    };

    public HashSet<PlayerMovementEnums> logicHash { get; } = new HashSet<PlayerMovementEnums>()
    {   
        PlayerMovementEnums.JUMP
    };

    public void Update()
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.70f)
        {
            stateMachine.ChangeStateAny(PlayerMovementEnums.JUMP);
            return;
        }
    }

    public void FixedUpdate()
    {
        
    }

    public void OnEnter()
    {
        player.animator.Play("JumpReady");
    }

    public void OnExit()
    {
    }
}
