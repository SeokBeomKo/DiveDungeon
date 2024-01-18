using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalController : EnemyController
{
    public override void Idle()
    {

    }

    public override void Move()
    {
        rigid.AddForce(Vector3.right * direction * moveSpeed);
    }

    public override void Patrol()
    {
        Move();
        ControlSpped();
    }

    public override void Trace()
    {
        direction = FindPlayerInRadius().transform.position.x < transform.position.x ? -1 : 1;

        SetSpriteFlip();
        Move();
        ControlSpped();
    }

    public override void Preparation()
    {

    }

    public override void Attack()
    {

    }
    public override void OnAttack()
    {
        enemyDamager.SettingValue(damage, direction == 1);
        enemyDamager.gameObject.SetActive(!enemyDamager.gameObject.activeSelf);
    }
}
