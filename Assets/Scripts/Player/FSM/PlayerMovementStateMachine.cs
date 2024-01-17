using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : MonoBehaviour
{
    [Header("플레이어 컨트롤러")]
    public PlayerController playerController;

    [HideInInspector] public IPlayerState curState;
    public Dictionary<PlayerMovementEnums, IPlayerState> stateDictionary;

    private void Awake()
    {
        stateDictionary = new Dictionary<PlayerMovementEnums, IPlayerState>
        {
            {PlayerMovementEnums.IDLE, new PlayerIdleState(this) },
            {PlayerMovementEnums.MOVE, new PlayerMoveState(this) },
            {PlayerMovementEnums.DODGE, new PlayerDodgeState(this) },
            {PlayerMovementEnums.JUMPREADY, new PlayerJumpReadyState(this) },
            {PlayerMovementEnums.JUMP, new PlayerJumpState(this) },
            {PlayerMovementEnums.FALL, new PlayerFallState(this) },
            {PlayerMovementEnums.LAND, new PlayerLandState(this) },
        };

        if(stateDictionary.TryGetValue(PlayerMovementEnums.IDLE, out IPlayerState newState))
        {
            // TryGetValue Key가 있는지 확인과 동시에 Value도 반환

            curState = newState;
            curState.OnEnter();
        }
    }

    public void ChangeState(PlayerMovementEnums newStateType)
    {
        if (curState == null) return;

        curState.OnExit();

        if(stateDictionary.TryGetValue(newStateType, out IPlayerState newState))
        {
            curState = newState;
            curState.OnEnter();
        }
    }
    
}
