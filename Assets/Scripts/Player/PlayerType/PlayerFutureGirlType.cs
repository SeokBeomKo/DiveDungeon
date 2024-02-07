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
