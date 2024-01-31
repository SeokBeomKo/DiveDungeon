using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontalRangeType : EnemyType
{
    [SerializeField] Transform shootPosition;
    [SerializeField] GameObject projectile;

    [SerializeField] LineRenderer predictionLine;
    [SerializeField] LayerMask predictionLayerMask;

    private RaycastHit2D predictionHit;

    public override void PreparationEnter()
    {
        controller.animator.Play("Preparation");
        predictionLine.enabled = true;

        predictionLine.SetPosition(0, shootPosition.position);
        predictionHit = Physics2D.Raycast(shootPosition.position, new Vector2(controller.direction,0), Mathf.Infinity, predictionLayerMask);

        if(predictionHit.collider == null)
        {
            
            predictionLine.enabled = false;
            return;
        }

        // draw first collision point
        predictionLine.SetPosition(1, predictionHit.point);
    }

    public override void PreparationExit()
    {
        controller.curPreparationTime = 0;
        predictionLine.enabled = false;
    }

    public override void OnAttack()
    {
        GameObject inst = Instantiate(projectile);
        inst.transform.position = shootPosition.position;
        inst.GetComponent<EnemyProjectile>().SettingValue(new Vector2(controller.direction,0));
    }
}
