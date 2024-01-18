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
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.35f)
        {
            if (player.CheckGround())
                stateMachine.ChangeStateLogic(PlayerMovementEnums.IDLE);
            else
                stateMachine.ChangeStateLogic(PlayerMovementEnums.FALL);

            return;
        }
    }

    public void FixedUpdate()
    {
        player.Dodge();
    }

    public void OnEnter()
    {
        player.animator.Play("Dodge");

        player.moveSpeed *= 5f;
        player.maxSpeed *= 1.8f;
    }

    public void OnExit()
    {
        player.rigid.velocity = new Vector2(0, player.rigid.velocity.y);

        player.moveSpeed /= 5f;
        player.maxSpeed /= 1.8f;
    }
}
