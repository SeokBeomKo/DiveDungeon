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
        // 공격 입력 값 delay
        if(player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
        {
            player.isAttack = false;
        }

        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f) return;

        // 공격 애니메이션 순서대로 나오게
        if (player.isAttack)
        {
            InitializeAttackAnimation();
            return;
        }
        
        // 플레이어가 땅에 있는지 체크
        if (player.CheckGround())
        {
            player.InitializeJumpCount();
            stateMachine.ChangeStateLogic(PlayerMovementEnums.IDLE);
            return;
        }

        // 떨어지는 상태
        if (player.rigid.velocity.y < 0)
        {
            stateMachine.ChangeStateLogic(PlayerMovementEnums.FALL);
            return;
        }

        // 점프하는 상태
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
