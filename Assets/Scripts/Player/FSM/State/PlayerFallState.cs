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
        PlayerMovementEnums.LAND
    };
    public void Update()
    {

    }

    public void FixedUpdate()
    {
        player.Move();
        player.SetFacingDirection();

        if (player.CheckGround())
        {
            stateMachine.ChangeStateAny(PlayerMovementEnums.LAND);
            return;
        }
    }

    public void OnEnter()
    {
        player.animator.Play("Fall");
    }

    public void OnExit()
    {
    }
}
