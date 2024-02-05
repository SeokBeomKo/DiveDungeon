using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNinjaType : PlayerType
{
    public override void AttackUpdate()
    {
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
