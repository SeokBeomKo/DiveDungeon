using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public PlayerController player { get; set; }
    public PlayerMovementStateMachine stateMachine { get; set; }


    // �������� �ֿ� ������ ��ü�� �ʱ�ȭ�ϰ� �ʱ� ���·� �����ϴ� ��
    public PlayerIdleState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            stateMachine.ChangeState(PlayerMovementEnums.MOVE);
            return;
        }
        if (Input.GetButtonDown("Fire3"))
        {
            stateMachine.ChangeState(PlayerMovementEnums.DODGE);
            return;
        }
        if (Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeState(PlayerMovementEnums.JUMPREADY);
            return;
        }
        if (Input.GetMouseButton(0))
        {
            stateMachine.ChangeState(PlayerMovementEnums.ATTACK);
            return;
        }
    }

    public void FixedUpdate()
    {

    }

    public void OnEnter()
    {
        player.animator.Play("Idle");
    }

    public void OnExit()
    {

    }
}