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
        stateMachine.ChangeStateInput(PlayerMovementEnums.JUMPREADY);
    }

    void ChangeAttackState()
    {
        stateMachine.ChangeStateInput(PlayerMovementEnums.ATTACK);
    }


    void CheckDirection(int dir)
    {
        controller.SetInputDirection(dir) ;
    }
}
