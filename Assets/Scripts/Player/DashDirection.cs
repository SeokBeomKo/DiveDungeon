using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDirection : MonoBehaviour
{
    public Joystick joystick;

    public void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, joystick.GetDashAngle());
    }
}
