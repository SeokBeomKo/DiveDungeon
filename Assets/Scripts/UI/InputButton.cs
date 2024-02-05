using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputButton : MonoBehaviour
{
    public delegate void ButtonHandler();
    public event ButtonHandler OnDash;


    public void DashClick()
    {
        OnDash?.Invoke();
    }
}
