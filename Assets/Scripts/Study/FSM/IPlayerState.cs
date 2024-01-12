using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    PlayerController player {get; set;}
    PlayerMovementStateMachine stateMachine {get; set;}

    void Update();
    void FixedUpdate();

    void OnEnter();
    void OnExit();
}
