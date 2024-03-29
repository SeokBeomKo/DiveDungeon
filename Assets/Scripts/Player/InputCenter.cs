using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCenter : MonoBehaviour
{
    public PlayerController controller;
    public PlayerMovementStateMachine stateMachine;
    public InputHandler inputHandler;

    private void Start()
    {
        inputHandler.OnPlayerIdle += ChangeIdleState;
        inputHandler.OnPlayerMove += ChangeMoveState;
        inputHandler.OnPlayerDodge += ChangeDodgeState;
        inputHandler.OnPlayerJump += ChangeJumpState;
        inputHandler.OnPlayerDownJump += ChangeDownJumpState;
        inputHandler.OnPlayerAttack += ChangeAttackState;

        inputHandler.OnPlayerCheckDir += CheckDirection;
    }

    void ChangeIdleState()
    {
        stateMachine.ChangeStateInput(PlayerMovementEnums.IDLE);
    }
    void ChangeMoveState()
    {
        stateMachine.ChangeStateInput(PlayerMovementEnums.MOVE);
    }

    void ChangeDodgeState()
    {
        stateMachine.ChangeStateInput(PlayerMovementEnums.DODGE);
    }

    void ChangeJumpState()
    {
        if (controller.curJumpCount == 0) return;

        if(stateMachine.curState is PlayerAttackState)
        {
            controller.Jump();
            return;
        }
        if (stateMachine.curState is PlayerIdleState)
        {
            stateMachine.ChangeStateInput(PlayerMovementEnums.JUMPREADY);
            return;
        }
        stateMachine.ChangeStateInput(PlayerMovementEnums.JUMP);
    }

    void ChangeDownJumpState()
    {
        stateMachine.ChangeStateInput(PlayerMovementEnums.DOWNJUMP);
    }

    void ChangeAttackState(bool value)
    {
        if (value)
        {
            stateMachine.ChangeStateInput(PlayerMovementEnums.ATTACK);
            controller.isAttack = true;
            return;
        }
    }

    void CheckDirection(int dir)
    {
        controller.SetInputDirection(dir) ;
    }
}
