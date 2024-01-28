using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFutureGirlType : PlayerType
{
    // ========== 재정의 함수 ==========
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

    // ========== 공격 ==========
    public override void AttackUpdate()
    {

    }
    public override void AttackFixedUpdate()
    {

    }
    public override void AttackOnEnter()
    {
        player.animator.Play("Attack");
    }
    public override void AttackOnExit()
    {

    }

    // ========== 특수 스킬 ==========
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
