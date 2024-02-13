using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputButton : MonoBehaviour
{
    public delegate void ButtonHandler();
    public event ButtonHandler OnDash;
    public event ButtonHandler OnAttack;
    public event ButtonHandler OffAttack;
    public event ButtonHandler OnJump;
    public event ButtonHandler OnSkill;

    public void DashClick()
    {
        OnDash?.Invoke();
    }

    public void AttackPress()
    {
        OnAttack?.Invoke();
    }

    public void AttackRelease()
    {
        OffAttack?.Invoke();
    }

    public void Jumpclick()
    {
        OnJump?.Invoke();
    }

    public void SkillClick()
    {
        OnSkill?.Invoke();
    }
}
