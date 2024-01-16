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


    public void Update()
    {
        //float animationDuration = player.animator.GetCurrentAnimatorStateInfo(0).length;

        /*if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f / animationDuration)
        {
            player.rigid.velocity = Vector3.zero;
        }*/
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.35f)
        {
            stateMachine.ChangeState(PlayerMovementEnums.IDLE);
        }
    }

    public void FixedUpdate()
    {
        player.Dodge();
    }

    public void OnEnter()
    {
        player.animator.Play("Dodge");

        player.moveSpeed *= 1.5f;
    }

    public void OnExit()
    {
        player.rigid.velocity = Vector3.zero;
        player.moveSpeed /= 1.5f;
    }
}
