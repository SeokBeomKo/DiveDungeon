using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public PlayerController player { get; set; }
    public PlayerMovementStateMachine stateMachine { get; set; }


    // 생성자의 주요 목적은 객체를 초기화하고 초기 상태로 설정하는 것
    public PlayerIdleState(PlayerMovementStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public HashSet<PlayerMovementEnums> inputHash { get; } = new HashSet<PlayerMovementEnums>()
    {
        PlayerMovementEnums.MOVE,
        PlayerMovementEnums.JUMP,
        PlayerMovementEnums.DODGE,
        PlayerMovementEnums.ATTACK,
        PlayerMovementEnums.DOWNJUMP
    };

    public HashSet<PlayerMovementEnums> logicHash { get; } = new HashSet<PlayerMovementEnums>()
    {
    };

    public void Update()
    {
    }

    public void FixedUpdate()
    {

    }

    public void OnEnter()
    {
        player.PlayAnimation("Idle");
    }

    public void OnExit()
    {

    }
}