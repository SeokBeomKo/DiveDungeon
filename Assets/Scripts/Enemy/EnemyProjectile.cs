using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    private int direction = 0;
    public float speed;

    public void SettingValue(int _direction)
    {
        direction = _direction;
    }

    private void OnEnable() 
    {
        rigid.velocity = new Vector2(speed * direction, 0);
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(speed * direction, 0);
    }
    
}
