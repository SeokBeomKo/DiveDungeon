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
        PlayerMovementEnums.IDLE
    };

    int curIndex;

    public void Update()
    {
        if(player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
        {
            player.isAttack = false;
        }

        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            if(player.isAttack)
            {
                player.animator.Play("Attack" + curIndex);
                curIndex++;
                if (curIndex > 3) curIndex = 1;
            }
            else
            {
                stateMachine.ChangeStateLogic(PlayerMovementEnums.IDLE);
            }
        }
    }

    public void FixedUpdate()
    {
    }

    public void OnEnter()
    {
        curIndex = 1;
        player.animator.Play("Attack1");
        curIndex++;
    }

    public void OnExit()
    {
    }

   
}
