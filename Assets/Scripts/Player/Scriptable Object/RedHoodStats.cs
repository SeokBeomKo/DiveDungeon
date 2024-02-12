using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RedHood", menuName = "Players/RedHood")]
public class RedHoodv : PlayerStats
{
    [Header("벽 슬라이드 속도")]
    public float wallSlidingSpeed;

    [Header("벽 점프력")]
    public Vector2 wallJumpPower;
}
