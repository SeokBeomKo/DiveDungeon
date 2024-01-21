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
        PlayerMovementEnums.IDLE,
        PlayerMovementEnums.JUMP,
        PlayerMovementEnums.FALL
    };

    int curIndex;

    public void Update()
    {
        // ���� �Է� �� delay
        if(player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
        {
            player.isAttack = false;
        }

        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f) return;

        // ���� �ִϸ��̼� ������� ������
        if (player.isAttack)
        {
            InitializeAttackAnimation();
            return;
        }
        
        // �÷��̾ ���� �ִ��� üũ
        if (player.CheckGround())
        {
            player.InitializeJumpCount();
            stateMachine.ChangeStateLogic(PlayerMovementEnums.IDLE);
            return;
        }

        // �������� ����
        if (player.rigid.velocity.y < 0)
        {
            stateMachine.ChangeStateLogic(PlayerMovementEnums.FALL);
            return;
        }

        // �����ϴ� ����
        stateMachine.ChangeStateLogic(PlayerMovementEnums.JUMP);
    }

    public void FixedUpdate()
    {
    }

    public void OnEnter()
    {
        curIndex = 1;
        InitializeAttackAnimation();
    }

    public void OnExit()
    {
    }

    void InitializeAttackAnimation()
    {
        player.animator.Play("Attack" + curIndex);
        curIndex++;
        if (curIndex > 3) curIndex = 1;
    }
   
}
