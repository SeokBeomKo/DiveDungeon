using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCenterTest : MonoBehaviour
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
        inputHandler.OffPlayerAttack += StopAttackState;
        inputHandler.OnPlayerSkill += ChangeSkillState;

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
        if (stateMachine.curState is PlayerWallSlideState)
        {
            stateMachine.ChangeStateInput(PlayerMovementEnums.WALLJUMP);
            return;
        }
        stateMachine.ChangeStateInput(PlayerMovementEnums.JUMP);
    }

    void ChangeDownJumpState()
    {
        if(controller.CheckGroundLayer() == LayerMask.NameToLayer("Platform"))
           stateMachine.ChangeStateInput(PlayerMovementEnums.DOWNJUMP);
    }

    void ChangeAttackState()
    {
        stateMachine.ChangeStateInput(PlayerMovementEnums.ATTACK);
        controller.isAttack = true;
    }

    void StopAttackState()
    {
        controller.isAttack = false;
    }

    void ChangeSkillState()
    {
        if (!controller.isSkillOn)
        {
            stateMachine.ChangeStateAny(PlayerMovementEnums.SKILL);
            return;
        }
    }

    void CheckDirection(int dir)
    {
        controller.SetInputDirection(dir) ;
    }
}
