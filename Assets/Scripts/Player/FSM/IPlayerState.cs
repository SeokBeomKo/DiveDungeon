using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    PlayerController player { get; set; }
    PlayerMovementStateMachine stateMachine { get; set; }

    // >> : 리스트로 자기자신이 변경 가능한 상태 목록 보유 \ get : 값을 가져오는 것만 가능
    HashSet<PlayerMovementEnums> inputHash { get; } // 키 입력받았을 때 변경되는 state
    HashSet<PlayerMovementEnums> logicHash { get; } // 스스로 변경되는 state


    void Update();
    void FixedUpdate();
    void OnEnter();
    void OnExit();
}
