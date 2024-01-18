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

    public HashSet<PlayerMovementEnums> inputHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.MOVE,
        PlayerMovementEnums.JUMPREADY,
        PlayerMovementEnums.DODGE,
        PlayerMovementEnums.ATTACK
    };

    public HashSet<PlayerMovementEnums> logicHash { get; } = new HashSet<PlayerMovementEnums>()
    {
    };

    public void Update()
    {
        /*if (Input.GetAxisRaw("Horizontal") != 0)
        {
            stateMachine.ChangeStateAny(PlayerMovementEnums.MOVE);
            return;
        }
        if (Input.GetButtonDown("Fire3"))
        {
            stateMachine.ChangeStateAny(PlayerMovementEnums.DODGE);
            return;
        }
        if (Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeStateAny(PlayerMovementEnums.JUMPREADY);
            return;
        }
        if (Input.GetMouseButton(0))
        {
            stateMachine.ChangeStateAny(PlayerMovementEnums.ATTACK);
            return;
        }*/
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