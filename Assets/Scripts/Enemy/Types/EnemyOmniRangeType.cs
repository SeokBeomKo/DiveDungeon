using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOmniRangeType : EnemyType
{
    [SerializeField] Transform shootPosition;
    [SerializeField] GameObject projectile;

    [SerializeField] LineRenderer predictionLine;
    [SerializeField] LayerMask predictionLayerMask;

    private Transform target;
    private RaycastHit2D predictionHit;
    private Vector2 direction;

    public override void PreparationFixedUpdate()
    {
        predictionHit = Physics2D.Raycast(shootPosition.position, direction = target.position - shootPosition.position, Mathf.Infinity, predictionLayerMask);

        if(predictionHit.collider == null)
        {
            
            predictionLine.enabled = false;
            return;
        }

        predictionLine.SetPosition(1, predictionHit.point);
    }

    public override void PreparationEnter()
    {
        target = controller.FindPlayerInRadius().transform;
        controller.animator.Play("Preparation");
        predictionLine.enabled = true;

        predictionLine.SetPosition(0, shootPosition.position);
    }

    public override void PreparationExit()
    {
        controller.curPreparationTime = 0;
        predictionLine.enabled = false;
        target = null;
    }

    public override void OnAttack()
    {
        GameObject inst = Instantiate(projectile);
        inst.transform.position = shootPosition.position;
        inst.GetComponent<EnemyProjectile>().SettingValue(direction.normalized);
    }
}
