using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFutureGirlType : PlayerType
{
    // ========== 재정의 함수 ==========
    public override void DodgeFixedUpdate()
    {
        player.eightWayDash();
    }

    // ========== 공격 ==========
    public override void AttackUpdate()
    {
        player.PlayAnimation("Attack");
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f) return;
        
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
        player.SetFacingDirection();
    }

    public override void AttackOnEnter()
    {
        player.PlayAnimation("Attack");
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
