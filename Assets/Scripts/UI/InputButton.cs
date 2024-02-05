using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputButton : MonoBehaviour
{
    public delegate void ButtonHandler();
    public event ButtonHandler OnDash;

    public delegate void PlayerInputHandler();
    public event PlayerInputHandler OnPlayerDodge;

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetButtonDown("Fire3"))
        {
            Debug.Log("ÀÎÇ²¹öÆ°");
            OnPlayerDodge?.Invoke();
        }
#endif
    }
    public void DashClick()
    {
        OnDash?.Invoke();
    }
}
