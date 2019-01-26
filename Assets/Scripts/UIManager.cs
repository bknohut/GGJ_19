using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public InputManager inputManager;
    public HUD Hud;

    private void Awake()
    {
        instance = this;
    }

    public static void EnrollJoystickMove(InputManager.JoystickMoveAction joystickMoveAction)
    {
        instance.inputManager.EnrollJoystickMove(joystickMoveAction);
    }

    public static void EnrollJoystickStop(InputManager.JoystickStopAction joystickStopAction)
    {
        instance.inputManager.EnrollJoystickStop(joystickStopAction);
    }

    public static void EnrollAction(InputManager.ClickAction clickAction)
    {
        instance.inputManager.EnrollAction(clickAction);
    }
    public static void SetItemImage(bool a)
    {
        instance.Hud.SetInventoryImage(a);
    }
}
