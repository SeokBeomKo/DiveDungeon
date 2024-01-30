using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour, IDamager
{
    [SerializeField]    public BoxCollider2D collider2d;
    [SerializeField]    public Vector2 offset;
    [HideInInspector]   public int damage;

    public void SettingValue(int _damage = 0, bool isRight = true)
    {
        collider2d.offset = isRight ? offset : -offset;
        damage = _damage;
    }

    public int BringDamage()
    {
        return damage;
    }
}
