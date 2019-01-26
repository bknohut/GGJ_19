using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public bool character0;
    public List<Animator> animators;
    public List<GameObject> characterDir;
    int currentDir;

    public enum LookPosition { UP, LEFT, DOWN, RIGHT};
    public enum AnimationState { IDLE, RUN, SMOKE };

    public AnimationState animationState;

    private void Start()
    {
        UIManager.EnrollJoystick(Movement);
        animationState = AnimationState.IDLE;
        currentDir = 3;
    }

    public void Movement(Vector2 dir)
    {
        if( dir.x >= 0f && dir.y >= 0f)
        {
            Turn(LookPosition.UP);
        }
        else if (dir.x >= 0f && dir.y < 0f)
        {
            Turn(LookPosition.RIGHT);
        }
        else if (dir.x < 0f && dir.y >= 0f)
        {
            Turn(LookPosition.LEFT);
        }
        else
        {
            Turn(LookPosition.DOWN);
        }
        RunAnimation();
        dir *= 10;
        Vector3 tmp = transform.position;
        tmp.x += dir.x;
        tmp.y += dir.y;
        transform.position = tmp;
    }

    public void Turn(LookPosition lookPosition)
    {
        RunAnimation();
        switch (lookPosition)
        {
            case LookPosition.UP:
                characterDir[currentDir].SetActive(false);
                currentDir = 0;
                characterDir[currentDir].SetActive(true);
                ChangeAnimation();
                break;
            case LookPosition.LEFT:
                characterDir[currentDir].SetActive(false);
                currentDir = 1;
                characterDir[currentDir].SetActive(true);
                ChangeAnimation();
                break;
            case LookPosition.DOWN:
                characterDir[currentDir].SetActive(false);
                currentDir = 2;
                characterDir[currentDir].SetActive(true);
                ChangeAnimation();
                break;
            case LookPosition.RIGHT:
                characterDir[currentDir].SetActive(false);
                currentDir = 3;
                characterDir[currentDir].SetActive(true);
                ChangeAnimation();
                break;
        }
    }

    public void ChangeAnimation()
    {
        Debug.Log(animators[currentDir].gameObject);
        switch(animationState)
        {
            case AnimationState.IDLE:
                if(!animators[currentDir].GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    animators[currentDir].SetTrigger("Idle");
                break;
            case AnimationState.SMOKE:
                if (character0 && !animators[currentDir].GetCurrentAnimatorStateInfo(0).IsName("Smoke0"))
                        animators[currentDir].SetTrigger("Smoke0");
                else if (!character0 && !animators[currentDir].GetCurrentAnimatorStateInfo(0).IsName("Smoke1"))
                        animators[currentDir].SetTrigger("Smoke1");
                break;
            case AnimationState.RUN:
                if (!animators[currentDir].GetCurrentAnimatorStateInfo(0).IsName("Run"))
                    animators[currentDir].SetTrigger("Run");
                break;
        }
    }

    public void RunAnimation()
    {

        if (animationState != AnimationState.RUN)
        {
            animators[currentDir].SetTrigger("Run");
            animationState = AnimationState.RUN;
        }
    }

    public void IdleAnimation()
    {
        if (animationState != AnimationState.IDLE)
        {
            animators[currentDir].SetTrigger("Idle");
            animationState = AnimationState.IDLE;
        }
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
        if (animationState != AnimationState.SMOKE)
        {
            animators[currentDir].SetTrigger("Smoke0");
            animationState = AnimationState.SMOKE;
        }
    }

    public void Smoke1Animation()
    {
        if (animationState != AnimationState.SMOKE)
        {
            animators[currentDir].SetTrigger("Smoke1");
            animationState = AnimationState.SMOKE;
        }
    }
}
