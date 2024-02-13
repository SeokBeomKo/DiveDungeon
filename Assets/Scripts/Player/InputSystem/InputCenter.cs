using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCenter : MonoBehaviour
{
    public Joystick joystick;
    public InputButton button;
    public PlayerMovementStateMachine stateMachine;
    public PlayerController controller;

    private void Start()
    {
        joystick.OnIdle += ChangeIdleState;
        joystick.OnMove += ChangeMoveState;
        joystick.OnDownJump += ChangeDownJumpState;
        joystick.OnCheckDirection += CheckDirection;
        joystick.OnCheckDashDirection += CheckDashDirection;

        button.OnDash += ChangeDodgeState;
        button.OnAttack += ChangeAttackState;
        button.OffAttack += StopAttackState;
        button.OnJump += ChangeJumpState;
        button.OnSkill += ChangeSkillState;
    }

    void ChangeIdleState()
    {
        stateMachine.ChangeStateInput(PlayerMovementEnums.IDLE);
    }

    void ChangeMoveState()
    {
        stateMachine.ChangeStateInput(PlayerMovementEnums.MOVE);
    }

    public void ChangeDodgeState()
    {
        stateMachine.ChangeStateInput(PlayerMovementEnums.DODGE);
    }

    public void ChangeJumpState()
    {
        if (controller.curJumpCount == 0) return;

        if (stateMachine.curState is PlayerAttackState)
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
        if (controller.CheckGroundLayer() == LayerMask.NameToLayer("Platform"))
        {
            stateMachine.ChangeStateInput(PlayerMovementEnums.DOWNJUMP);
            return;
        }
    }

    public void ChangeAttackState()
    {
        stateMachine.ChangeStateInput(PlayerMovementEnums.ATTACK);
        controller.isAttack = true;
    }

    public void StopAttackState()
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
        controller.SetInputDirection(dir);
    }

    void CheckDashDirection(Vector2 dir)
    {
        controller.SetDashDirection(dir);
    }
}
