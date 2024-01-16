/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : MonoBehaviour
{
    [Header("플레이어 컨트롤러")]
    [SerializeField] public PlayerController playerController;
    [HideInInspector] public IPlayerState curState;
 
    public Dictionary<PlayerMovementStateEnums, IPlayerState> stateDictionary;

    private void Awake() 
    {
        stateDictionary = new Dictionary<PlayerMovementStateEnums, IPlayerState>
        {
            {PlayerMovementStateEnums.IDLE, new PlayerIdleState(this)},
            {PlayerMovementStateEnums.MOVE, new PlayerMoveState(this)},
            {PlayerMovementStateEnums.JUMP, new PlayerJumpState(this)},
            {PlayerMovementStateEnums.FALL, new PlayerFallState(this)},
            {PlayerMovementStateEnums.DODGE, new PlayerDodgeState(this)},

        };

        if (stateDictionary.TryGetValue(PlayerMovementStateEnums.IDLE, out IPlayerState newState))
        {
            curState = newState;
            curState.OnEnter();
        }
    }

    public void ChangeState(PlayerMovementStateEnums newStateType)
    {
        if (null == curState)   return;

        curState.OnExit();

        if (stateDictionary.TryGetValue(newStateType, out IPlayerState newState))
        {
            newState.OnEnter();
            curState = newState;
        }
    }
}
*/