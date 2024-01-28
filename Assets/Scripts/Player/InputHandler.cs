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
    public event PlayerInputHandler OnPlayerDownJump;

    public delegate void InputIntHandler(int value);
    public event InputIntHandler OnPlayerCheckDir;

    public delegate void InputBoolHandler(bool value);
    public event InputBoolHandler OnPlayerAttack;

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
        OnPlayerAttack?.Invoke(Input.GetMouseButton(0));

        // �Ʒ� ����
        if(Input.GetAxisRaw("Vertical") == -1)
        {
            OnPlayerDownJump?.Invoke();
        }
#endif
    }
}
    

