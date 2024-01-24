using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public EnemyController controller;
    public IEnemyState curState;
    public Dictionary<EnemyStateEnums, IEnemyState> stateDic;

    private void Awake()
    {
        stateDic = new Dictionary<EnemyStateEnums, IEnemyState>
        {
            { EnemyStateEnums.IDLE,         new EnemyIdleState(this)},
            { EnemyStateEnums.PATROL,       new EnemyPatrolState(this)},
            { EnemyStateEnums.TRACE,        new EnemyTraceState(this)},
            { EnemyStateEnums.PREPARATION,  new EnemyPreparationState(this)},
            { EnemyStateEnums.ATTACK,       new EnemyAttackState(this)},
            { EnemyStateEnums.DEAD,         new EnemyDeadState(this)},
            { EnemyStateEnums.Hit,          new EnemyHitState(this)}
        };

        ChangeState(EnemyStateEnums.IDLE);
    }
    public void ChangeState(EnemyStateEnums newStateType)
    {
        if (null == stateDic)   return;
        if (null != curState)   curState.OnExit();
        
        if (stateDic.TryGetValue(newStateType, out IEnemyState newState))
        {
            curState = newState;
            curState.OnEnter();
        }
    }
}
