using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStateEnums
    {
        IDLE,           // 대기
        PATROL,         // 순찰
        TRACE,          // 추격
        PREPARATION,    // 준비
        ATTACK,         // 공격
        DEAD,           // 적 사망 상태

        Hit,            // 피격
    }

    public enum EnemyTypeEnums
    {
        MELEE,
        RANGE,
        TURRET,

        EPIC,

        BOSS,
    }
