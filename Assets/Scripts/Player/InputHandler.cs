using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public delegate void PlayerInputHandler();
    public event PlayerInputHandler OnPlayerMove;
    public event PlayerInputHandler OnPlayerDodge;
    public event PlayerInputHandler OnPlayerJump;
    public event PlayerInputHandler OnPlayerAttack;

    public delegate void DirectionInputHandler(float dir);
    public event DirectionInputHandler OnPlayerCheckDir;

    void Update()
    {
#if UNITY_EDITOR
        // 이동
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            OnPlayerMove?.Invoke();
        }
        OnPlayerCheckDir(Input.GetAxisRaw("Horizontal"));

        // 대시
        if (Input.GetButtonDown("Fire3"))
        {
            OnPlayerDodge?.Invoke();
        }

        // 점프
        if (Input.GetButtonDown("Jump"))
        {
            OnPlayerJump?.Invoke();
        }

        // 공격
        if (Input.GetMouseButton(0))
        {
            OnPlayerAttack?.Invoke();
        }

#endif
    }
}
