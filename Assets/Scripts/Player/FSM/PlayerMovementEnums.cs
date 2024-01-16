using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMovementEnums
{
    IDLE,               // 기본
    MOVE,               // 이동
    JUMPREADY,          // 점프준비
    JUMP,               // 점프
    FALL,               // 하강
    LAND,               // 착지 
    DODGE,              // 구르기, 대시
    ATTACK,             // 공격
    SPECIALATTACK,      // 특수 공격
    WALLJUMP,           // 벽 점프
}
