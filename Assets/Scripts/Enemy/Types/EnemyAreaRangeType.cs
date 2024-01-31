using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAreaRangeType : EnemyType
{
    [SerializeField] GameObject attacker;
    [SerializeField] GameObject warningBox;

    [SerializeField] Vector2 size;
    private Vector2 target;

    private GameObject box;

    public override void PreparationEnter()
    {
        box = Instantiate(warningBox);
        box.transform.localScale = size;

        target = controller.FindPlayerInRadius().transform.position;

        box.transform.position = target;

        controller.animator.Play("Preparation");
    }

    public override void PreparationExit()
    {
        controller.curPreparationTime = 0;
        Destroy(box);
    }

    public override void OnAttack()
    {
        GameObject inst = Instantiate(attacker);
        inst.transform.position = target;
        // inst.GetComponent<EnemyProjectile>().SettingValue();
    }
}
