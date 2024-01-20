using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public delegate void PlayerInputHandler();
    public event PlayerInputHandler OnPlayerIdle;
    public event PlayerInputHandler OnPlayerMove;
    public event PlayerInputHandler OnPlayerDodge;
    public event PlayerInputHandler OnPlayerJump;
    public event PlayerInputHandler OnPlayerAttack;
    public event PlayerInputHandler OnPlayerStopAttack;

    public delegate void DirectionInputHandler(int dir);
    public event DirectionInputHandler OnPlayerCheckDir;

    private void Update()
    {
#if UNITY_EDITOR
        // �̵�
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            OnPlayerMove?.Invoke();
        }

        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            OnPlayerIdle?.Invoke();
        }

        OnPlayerCheckDir?.Invoke((int)Input.GetAxisRaw("Horizontal"));

        // ���
        if (Input.GetButtonDown("Fire3"))
        {
            OnPlayerDodge?.Invoke();
        }

        // ����
        if (Input.GetButtonDown("Jump"))
        {
            OnPlayerJump?.Invoke();
        }

        // ����
        if (Input.GetMouseButton(0))
        {
            OnPlayerAttack?.Invoke();
        }

        if(Input.GetMouseButtonUp(0))
        {
            OnPlayerStopAttack?.Invoke();
        }

#endif
    }
}
