using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    private Vector2 direction = Vector2.zero;
    public float speed;

    public void SettingValue(Vector2 _direction)
    {
        direction = _direction;
    }

    private void OnEnable() 
    {
        rigid.velocity = direction * speed;
    }

    void FixedUpdate()
    {
        rigid.velocity = direction * speed;
    }
    
}
