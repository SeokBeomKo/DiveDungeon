using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeController : EnemyController
{
    [SerializeField] Transform shootPosition;
    [SerializeField] GameObject projectile;

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
        GameObject inst = Instantiate(projectile);
        inst.transform.position = shootPosition.position;
        inst.GetComponent<EnemyProjectile>().SettingValue(direction);
    }
}
