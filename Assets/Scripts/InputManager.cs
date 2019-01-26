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
    public RectTransform stick;

    public delegate void JoystickAction(Vector2 direction);
    public delegate void ClickAction();

    private List<JoystickAction> joystickActions;
    private List<ClickAction> clickActions;

    void Start()
    {
        joystickId = -1;
        startPos = back.position;
    }

    void FixedUpdate()
    {
        if(!moving && Input.GetMouseButtonDown(0) && Input.mousePosition.x < Screen.width / 3 && Input.mousePosition.y < Screen.height / 3)
        {
            moving = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            moving = false;
            front.localPosition = new Vector2(75, 75);
        }
        else
            currentPos = Input.mousePosition;
        if (Input.GetKeyDown(KeyCode.Space))
            ActionClicked();

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
            dir.x = Mathf.Clamp(dir.x, 25, 125);
            dir.y = Mathf.Clamp(dir.y, 25, 125);
            currentPos = startPos + dir;
            front.position = currentPos;
            foreach(JoystickAction joystickAction in joystickActions)
                joystickAction(dir / 50 - new Vector2(1.5f, 1.5f));
            stick.right = -front.localPosition;
        }
    }

    public void ActionClicked()
    {
        foreach(ClickAction clickAction in clickActions)
        clickAction();
    }

    public void EnrollJoystick(JoystickAction joystickAction)
    {
        if (joystickActions == null)
            joystickActions = new List<JoystickAction>();
        joystickActions.Add(joystickAction);
    }

    public void EnrollAction(ClickAction clickAction)
    {
        if (clickActions == null)
            clickActions = new List<ClickAction>();
        clickActions.Add(clickAction);
    }
}
