using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityHitScan : MonoBehaviour, IDamageable
{
    [SerializeField] public Animator animator;


    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            // animator.Play("Idle");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<IDamager>() != null)
        {
            IDamager damager = other.GetComponent<IDamager>();
            Damage(damager.BringDamage());
        }
    }


    public void Damage(int damage, bool critical = false)
    {
        animator.Play("Hit");
    }
}
