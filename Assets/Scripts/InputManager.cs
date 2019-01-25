using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    bool moving;
    private Vector2 startPos;
    private Vector2 currentPos;

    private Touch myTouch;
    private int joystickId;

    public RectTransform back;
    public RectTransform front;

    public delegate void JoystickAction(Vector2 direction);
    public delegate void ClickAction();

    private JoystickAction joystickAction;
    private ClickAction clickAction;

    void Start()
    {
        joystickId = -1;
        startPos = back.position;
    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (!moving && touch.phase == TouchPhase.Began && touch.position.x < Screen.width / 3 && touch.position.y < Screen.height / 3)
            {
                joystickId = touch.fingerId;
                moving = true;
            }
            if (touch.fingerId == joystickId)
            {
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    moving = false;
                    joystickId = -1;
                    front.localPosition = new Vector2(75, 75);
                }
                else
                    currentPos = touch.position;
            }
            else
                ActionClicked();
        }

        if (moving)
        {
            Vector2 dir = (currentPos - startPos);
            dir.x = Mathf.Clamp(dir.x, 0, 175);
            dir.y = Mathf.Clamp(dir.y, 0, 175);
            currentPos = startPos + dir;
            front.position = currentPos;
            joystickAction(dir / 87.5f - Vector2.one);
        }
    }

    public void ActionClicked()
    {
        clickAction();
    }


    public void EnrollJoystick(JoystickAction joystickAction)
    {
        this.joystickAction = joystickAction;
    }

    public void EnrollAction(ClickAction clickAction)
    {
        this.clickAction = clickAction;
    }
}
