using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFutureGirlType : PlayerType
{
    // ========== ������ �Լ� ==========
    public override void DodgeUpdate()
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            if (player.CheckGround())
            {
                player.stateMachine.ChangeStateLogic(PlayerMovementEnums.IDLE);
                return;
            }
            else
            {
                player.stateMachine.ChangeStateLogic(PlayerMovementEnums.FALL);
                return;
            }
        }
    }

    // ========== ���� ==========
    public override void AttackUpdate()
    {
        player.animator.Play("Attack");
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f) return;
        
        // �÷��̾ ���� �ִ��� üũ
        if (player.CheckGround())
        {
            player.stateMachine.ChangeStateLogic(PlayerMovementEnums.IDLE);
            player.InitializeJumpCount();
            return;
        }

        // �������� ����
        if (player.rigid.velocity.y < 0)
        {
            player.stateMachine.ChangeStateLogic(PlayerMovementEnums.FALL);
            return;
        }

        // �����ϴ� ����
        player.stateMachine.ChangeStateLogic(PlayerMovementEnums.RISE);
    }

    public override void AttackFixedUpdate()
    {
        player.SetFacingDirection();
    }

    public override void AttackOnEnter()
    {
        player.animator.Play("Attack");
    }

    public override void AttackOnExit()
    {

    }

    // ========== Ư�� ��ų ==========
    public override void SkillUpdate()
    {

    }
    public override void SkillFixedUpdate()
    {

    }
    public override void SkillOnEnter()
    {

    }
    public override void SkillOnExit()
    {

    }
}
