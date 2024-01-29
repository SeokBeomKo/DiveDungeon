using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRedHoodType : PlayerType
{
    [Header("���� �� �ִϸ�����")]
    public AnimatorOverrideController whiteHood;
    [Header("���� �ִϸ�����")]
    public RuntimeAnimatorController redHood;

    float skillTime = 5.0f;
    bool isChange = false;


    // ========== ������ �Լ� ==========
    public override void RiseUpdate()
    {
        if (player.rigid.velocity.y < 0)
        {
            player.stateMachine.ChangeStateLogic(PlayerMovementEnums.FALL);
            return;
        }
        if (!player.CheckGround() && player.CheckWall() && player.direction != 0 && !player.isWallJump)
        {
            player.stateMachine.ChangeStateLogic(PlayerMovementEnums.WALLSLIDE);
            return;
        }
    }

    public override void FallUpdate()
    {
        if (player.CheckGround())
        {
            player.stateMachine.ChangeStateAny(PlayerMovementEnums.LAND);
            return;
        }

        if (!player.CheckGround() && player.CheckWall() && player.direction != 0 && !player.isWallJump)
        {
            player.stateMachine.ChangeStateLogic(PlayerMovementEnums.WALLSLIDE);
            return;
        }
    }


    // ========== ���� ==========
    int curIndex;

    public override void AttackUpdate()
    {
        // ���� �Է� �� delay
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
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
    }

    public override void AttackOnEnter()
    {
        curIndex = 1;
        InitializeAttackAnimation();
    }

    public override void AttackOnExit()
    {

    }

    void InitializeAttackAnimation()
    {
        player.animator.Play("Attack" + curIndex);
        curIndex++;
        if (curIndex > 3) curIndex = 1;
    }

    // ========== Ư�� ��ų ==========
    public override void SkillUpdate()
    {
        if(player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            player.stateMachine.ChangeStateLogic(PlayerMovementEnums.IDLE);
        }
    }

    public override void SkillFixedUpdate()
    {

    }
    public override void SkillOnEnter()
    {
        player.isSkillOn = true;
        player.animator.Play("Change");
    }

    public override void SkillOnExit()
    {
        if (!isChange)
        {
            player.animator.runtimeAnimatorController = whiteHood;
            isChange = true;
            StartCoroutine(CheckSkillTime());
        }
        else
        {
            isChange = false;
            player.isSkillOn = false;
            player.animator.runtimeAnimatorController = redHood;
        }
    }

    // ========== �ʿ��� �Լ� ==========
    IEnumerator CheckSkillTime()
    {
        yield return new WaitForSeconds(skillTime);

        player.stateMachine.ChangeStateAny(PlayerMovementEnums.SKILL);
    }
}
