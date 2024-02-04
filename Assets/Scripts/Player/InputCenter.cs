using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCenter : MonoBehaviour
{
    public Joystick joystick;
    public PlayerMovementStateMachine stateMachine;
    public PlayerController controller;

    private void Start()
    {
        joystick.OnPlayerIdle += ChangeIdleState;
        joystick.OnPlayerMove += ChangeMoveState;
        joystick.OnPlayerDownJump += ChangeDownJumpState;
        joystick.OnPlayerCheckDir += CheckDirection;
        joystick.OnPlayerCheckDashDir += CheckDashDirection;
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
            stateMachine.ChangeStateInput(PlayerMovementEnums.DOWNJUMP);
    }

    public void ChangeAttackState()
    {
        stateMachine.ChangeStateInput(PlayerMovementEnums.ATTACK);
        controller.isAttack = true;
        return;
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
