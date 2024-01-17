using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyController : MonoBehaviour
{
    [SerializeField]    public Animator             enemyAnimator;
    [SerializeField]    public Rigidbody2D          rigid;
    [SerializeField]    public EnemyStateMachine    stateMachine;


    private void Update() 
    {
        if (stateMachine != null)
            stateMachine.curState.Update();
    }

    private void FixedUpdate() 
    {
        if (stateMachine != null)
            stateMachine.curState.FixedUpdate();
    }

    public abstract void Idle();
    public abstract void Patrol();
    public abstract void Trace();
    public abstract void Attack();
    public abstract void OnAttack();
    
    
}
