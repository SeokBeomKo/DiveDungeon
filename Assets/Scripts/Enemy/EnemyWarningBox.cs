using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarningBox : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public void SettingValue(Vector2 size)
    {
        spriteRenderer.size = size;
    }
}
