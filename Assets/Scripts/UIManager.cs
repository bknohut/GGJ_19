using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public InputManager inputManager;

    private void Awake()
    {
        instance = this;
    }

    public static void EnrollJoystick(InputManager.JoystickAction joystickAction)
    {
        instance.inputManager.EnrollJoystick(joystickAction);
    }

    public static void EnrollAction(InputManager.ClickAction clickAction)
    {
        instance.inputManager.EnrollAction(clickAction);
    }
}
