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
    public event PlayerInputHandler OnPlayerSkill;
    public event PlayerInputHandler OnPlayerAttack;
    public event PlayerInputHandler OffPlayerAttack;

    public delegate void InputIntHandler(int value);
    public event InputIntHandler OnPlayerCheckDir;


    private void Update()
    {

#if UNITY_EDITOR
        // 이동
        /*if (Input.GetAxisRaw("Horizontal") != 0)
        {
            OnPlayerMove?.Invoke();
        }

        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            OnPlayerIdle?.Invoke();
        }

        OnPlayerCheckDir?.Invoke((int)Input.GetAxisRaw("Horizontal"));*/

        // 대시
        if (Input.GetButtonDown("Fire3"))
        {
            Debug.Log("인풋핸들러");
            OnPlayerDodge?.Invoke();
        }

        // 점프
        if (Input.GetButtonDown("Jump"))
        {
            OnPlayerJump?.Invoke();
        }

        // 공격
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnPlayerAttack?.Invoke();
        }

        if(Input.GetKeyUp(KeyCode.E))
        {
            OffPlayerAttack?.Invoke();
        }

        // 아래 점프
        if(Input.GetAxisRaw("Vertical") == -1)
        {
            OnPlayerDownJump?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            OnPlayerSkill?.Invoke();
        }
#endif
    }

}
    

