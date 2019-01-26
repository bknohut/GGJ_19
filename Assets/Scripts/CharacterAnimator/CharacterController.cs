using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public bool character0;
    public List<Animator> animators;
    public List<GameObject> characterDir;
    public Rigidbody2D rigidbody2D;
    int currentDir;
    private bool smoking;

    public enum InventoryItem { Sponge, Broom };
    public enum LookPosition { UP, LEFT, DOWN, RIGHT};
    public enum AnimationState { IDLE, RUN, SMOKE };

    public AnimationState animationState;

    private void Start()
    {
        UIManager.EnrollJoystickMove(JoystickMove);
        UIManager.EnrollJoystickStop(JoystickStop);
        UIManager.EnrollAction(ClickAction);
        animationState = AnimationState.IDLE;
        currentDir = 3;
    }

    public void JoystickMove(Vector2 dir)
    {
        if (dir.x >= 0f && dir.y >= 0f)
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
        dir *= 50;
        Vector3 tmp = transform.position;
        tmp.x += dir.x;
        tmp.y += dir.y;
        // transform.position = tmp;
        rigidbody2D.AddForce(dir);
        rigidbody2D.velocity = new Vector2(Mathf.Clamp(rigidbody2D.velocity.x, -10, 10), Mathf.Clamp(rigidbody2D.velocity.y, -10, 10));
    }

    public void JoystickStop()
    {
        IdleAnimation();
    }

    public void ClickAction()
    {
        if (!smoking)
        {
            smoking = true;
            SmokeAnimation();
            StartCoroutine(SmokeWaitRoutine());
        }
    }

    private IEnumerator SmokeWaitRoutine()
    {
        yield return new WaitForSeconds(1);
        animationState = AnimationState.IDLE;
        yield return new WaitForSeconds(0f);
        smoking = false;
    }

    public void Turn(LookPosition lookPosition)
    {
        RunAnimation();
        switch (lookPosition)
        {
            case LookPosition.UP:
                if (currentDir == 0)
                    return;
                characterDir[currentDir].SetActive(false);
                currentDir = 0;
                characterDir[currentDir].SetActive(true);
                ChangeAnimation();
                break;
            case LookPosition.LEFT:
                if (currentDir == 1)
                    return;
                characterDir[currentDir].SetActive(false);
                currentDir = 1;
                characterDir[currentDir].SetActive(true);
                ChangeAnimation();
                break;
            case LookPosition.DOWN:
                if (currentDir == 2)
                    return;
                characterDir[currentDir].SetActive(false);
                currentDir = 2;
                characterDir[currentDir].SetActive(true);
                ChangeAnimation();
                break;
            case LookPosition.RIGHT:
                if (currentDir == 3)
                    return;
                characterDir[currentDir].SetActive(false);
                currentDir = 3;
                characterDir[currentDir].SetActive(true);
                ChangeAnimation();
                break;
        }
    }

    public void ChangeAnimation()
    {
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
