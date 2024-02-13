using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum playerType
{
    REDHOOD,
    FUTUREGIRL,
    NINJA,
    COWBOY
}

public class PlayerStats : ScriptableObject
{
    [Header("캐릭터 프리팹")]
    public GameObject prefab;
    
    [Header("캐릭터 타입")]
    public playerType type;
    
    [Header("최고 속도")]
    public float maxSpeed;

    [Header("이동 속도")]
    public float moveSpeed;

    [Header("점프 높이")]
    public float jumpForce; 
}
