using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{

    public bool character0;
    public List<Animator> animators;
    public List<GameObject> characterDir;
    int currentDir;

    public bool left;
    public bool up;
    public bool down;
    public bool right;
    public enum LookPosition { UP, LEFT, DOWN, RIGHT};

    private void Start()
    {
        currentDir = 3;
    }

    private void Update()
    {
        if (left)
        {
            Turn(LookPosition.LEFT);
            left = false;
        }

        if (up)
        {
            Turn(LookPosition.UP);
                up = false;
        }
        if (down)
        {
            Turn(LookPosition.DOWN);
                down = false;
        }
        if (right)
        {
            Turn(LookPosition.RIGHT);
                right = false;
        }
    }

    public void Turn(LookPosition lookPosition)
    {
        animators[currentDir].SetTrigger("Idle");
        switch (lookPosition)
        {
            case LookPosition.UP:
                characterDir[currentDir].SetActive(false);
                currentDir = 0;
                characterDir[currentDir].SetActive(true);
                break;
            case LookPosition.LEFT:
                characterDir[currentDir].SetActive(false);
                currentDir = 1;
                characterDir[currentDir].SetActive(true);
                break;
            case LookPosition.DOWN:
                characterDir[currentDir].SetActive(false);
                currentDir = 2;
                characterDir[currentDir].SetActive(true);
                break;
            case LookPosition.RIGHT:
                characterDir[currentDir].SetActive(false);
                currentDir = 3;
                characterDir[currentDir].SetActive(true);
                break;
        }
    }

    public void RunAnimation()
    {
        animators[currentDir].SetTrigger("Run");
    }

    public void IdleAnimation()
    {
        animators[currentDir].SetTrigger("Idle");
    }

    public void SmokeAnimation()
    {
        if (character0)
            Smoke0Animation();
        else
            Smoke1Animation();
    }

    public void Smoke0Animation()
    {
        animators[currentDir].SetTrigger("Smoke0");
    }

    public void Smoke1Animation()
    {
        animators[currentDir].SetTrigger("Smoke1");
    }
}
