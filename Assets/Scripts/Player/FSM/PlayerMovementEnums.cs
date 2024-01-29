using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMovementEnums
{
    IDLE,               // 기본
    MOVE,               // 이동
    JUMP,               // 점프
    RISE,               // 상승
    FALL,               // 하강
    LAND,               // 착지 
    DODGE,              // 구르기, 대시
    ATTACK,             // 공격
    DOWNJUMP,           // 아래 점프
    SKILL,              // 특수 스킬
    WALLSLIDE,          // 벽 슬라이드
    WALLJUMP,           // 벽 점프
}
