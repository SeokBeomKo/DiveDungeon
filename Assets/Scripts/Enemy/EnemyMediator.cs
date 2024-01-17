using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMediator : MonoBehaviour
{
    [SerializeField] public EnemyController controller;
    [SerializeField] public EnemyStateMachine stateMachine;

    private void Awake() 
    {
        stateMachine.OnPlayAnimation += controller.PlayAnimation;
    }
}
