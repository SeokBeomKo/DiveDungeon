using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRedHoodType : PlayerType
{
    [Header("변신 후 애니메이터")]
    public AnimatorOverrideController whiteHood;
    [Header("기존 애니메이터")]
    public RuntimeAnimatorController redHood;

    float skillTime = 5.0f;
    bool isChange = false;


    // ========== 재정의 함수 ==========
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


    // ========== 공격 ==========
    int curIndex;

    public override void AttackUpdate()
    {
        // 공격 입력 값 delay
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
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
            player.stateMachine.ChangeStateLogic(PlayerMovementEnums.IDLE);
            player.InitializeJumpCount();
            return;
        }

        // 떨어지는 상태
        if (player.rigid.velocity.y < 0)
        {
            player.stateMachine.ChangeStateLogic(PlayerMovementEnums.FALL);
            return;
        }

        // 점프하는 상태
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

    // ========== 특수 스킬 ==========
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

    // ========== 필요한 함수 ==========
    IEnumerator CheckSkillTime()
    {
        yield return new WaitForSeconds(skillTime);

        player.stateMachine.ChangeStateAny(PlayerMovementEnums.SKILL);
    }
}
