using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFutureGirlType : PlayerType
{
    // ========== ������ �Լ� ==========
    public override void DodgeFixedUpdate()
    {
        player.eightWayDash();
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
