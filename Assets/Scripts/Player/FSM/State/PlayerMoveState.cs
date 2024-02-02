using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    public PlayerController player { get; set; }
    public PlayerMovementStateMachine stateMachine { get; set; }

    public PlayerMoveState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public HashSet<PlayerMovementEnums> inputHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.IDLE,
        PlayerMovementEnums.JUMP,
        PlayerMovementEnums.DODGE,
        PlayerMovementEnums.ATTACK,
        PlayerMovementEnums.DOWNJUMP
    };

    public HashSet<PlayerMovementEnums> logicHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.FALL
    };

    public void Update()
    {        
        if(!player.CheckGround())
        {
            stateMachine.ChangeStateLogic(PlayerMovementEnums.FALL);
        }
    }

    public void FixedUpdate()
    {
        player.Move();
        player.SetFacingDirection();
    }

    public void OnEnter()
    {
        player.PlayAnimation("Move");
    }

    public void OnExit()
    {
        player.rigid.velocity = Vector3.zero;
    }
}
