using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeType : EnemyType
{
    public EnemyDamager damager;

    public override void OnAttack()
    {
        damager.SettingValue(controller.damage, controller.direction == 1);
        damager.gameObject.SetActive(true);
    }
}
