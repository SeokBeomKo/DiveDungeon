using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : IPlayerState
{
    public PlayerController player {get; set;}
    public PlayerMovementStateMachine stateMachine {get; set;}

    public PlayerDodgeState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Update()
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.35f)
        {
            stateMachine.ChangeState(PlayerMovementStateEnums.IDLE);
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

        player.maxSpeed *= 2f;
    }
    public void OnExit()
    {
        player.rigid.velocity = Vector3.zero;
        player.maxSpeed *= 0.5f;
    }
}
