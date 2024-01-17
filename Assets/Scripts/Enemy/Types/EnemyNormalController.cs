using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalController : EnemyController
{
    public override void Idle()
    {

    }
    public override void Patrol()
    {
        rigid.AddForce(Vector3.right * direction * moveSpeed);

        ControlSpped();
    }
    public override void Trace()
    {

    }
    public override void Attack()
    {

    }
    public override void OnAttack()
    {

    }
}
