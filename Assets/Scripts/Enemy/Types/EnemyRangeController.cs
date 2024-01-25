using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeController : EnemyController
{
    [SerializeField] Transform shootPosition;
    [SerializeField] GameObject projectile;

    [SerializeField] LineRenderer predictionLine;
    [SerializeField] LayerMask predictionLayerMask;

    private RaycastHit2D predictionHit;
    

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

    public override void EnterPreparation()
    {
        animator.Play("Preparation");
        predictionLine.enabled = true;
    }   

    public override void Preparation()
    {
        predictionLine.SetPosition(0, shootPosition.position);
        predictionHit = Physics2D.Raycast(shootPosition.position, new Vector2(direction,0), Mathf.Infinity, predictionLayerMask);

        if(predictionHit.collider == null)
        {
            
            predictionLine.enabled = false;
            return;
        }

        // draw first collision point
        predictionLine.SetPosition(1, predictionHit.point);
    }

    public override void ExitPreparation()
    {
        curPreparationTime = 0;
        predictionLine.enabled = false;
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
