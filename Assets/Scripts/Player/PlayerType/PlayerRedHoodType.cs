using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRedHoodType : PlayerType
{
    [Header("Before transformation")]
    public AnimatorOverrideController whiteHood;
    [Header("After transformation")]
    public RuntimeAnimatorController redHood;

    float skillTime = 5.0f;
    bool isChange = false;


    // ========== redefinition function ==========
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

    // ========== Dodge State ==========
    public override void DodgeUpdate()
    {
        if (!player.animator.GetCurrentAnimatorStateInfo(0).IsName("Dodge")) return;

        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.35f)
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

    public override void DodgeFixedUpdate()
    {
        player.Dodge();
    }

    public override void DodgeOnEnter()
    {
        player.PlayAnimation("Dodge");

        player.moveSpeed *= 5f;
        player.maxSpeed *= 2f;
    }

    public override void DodgeOnExit()
    {
        player.rigid.velocity = new Vector2(0, player.rigid.velocity.y);

        player.moveSpeed /= 5f;
        player.maxSpeed /= 2f;
    }


    // ========== Attack State ==========
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

        if (player.CheckGround())
        {
            player.stateMachine.ChangeStateLogic(PlayerMovementEnums.IDLE);
            player.InitializeJumpCount();
            return;
        }

        if (player.rigid.velocity.y < 0)
        {
            player.stateMachine.ChangeStateLogic(PlayerMovementEnums.FALL);
            return;
        }

        player.stateMachine.ChangeStateLogic(PlayerMovementEnums.RISE);
    }

    public override void AttackFixedUpdate()
    {
        player.SetFacingDirection();
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


    // ========== Skill State ==========
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

    // ========== New Function ==========
    IEnumerator CheckSkillTime()
    {
        yield return new WaitForSeconds(skillTime);

        player.stateMachine.ChangeStateAny(PlayerMovementEnums.SKILL);
    }
}
