using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterController : MonoBehaviour
{
    public Transform broom;
    public Collider2D spongeCollider;
    public Collider2D broomCollider;
    public Transform sponge;
    public bool character0;
    public List<Animator> animators;
    public List<GameObject> characterDir;
    public Rigidbody2D rb2d;
    int currentDir;
    private bool smoking;

    private Ground ground;  
    public enum LookPosition { UP, LEFT, DOWN, RIGHT};
    public enum AnimationState { IDLE, RUN, SMOKE };
    public enum Equipment { NONE, BROOM, SPONGE };

    public AnimationState animationState;
    public Equipment equipment;

    private void Start()
    {
        UIManager.EnrollJoystickMove(JoystickMove);
        UIManager.EnrollJoystickStop(JoystickStop);
        UIManager.EnrollAction(ClickAction);
        animationState = AnimationState.IDLE;
        equipment = Equipment.NONE;
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
        rb2d.AddForce(dir * 50);
        rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -10, 10), Mathf.Clamp(rb2d.velocity.y, -10, 10));
    }

    public void JoystickStop()
    {
        IdleAnimation();
    }

    public void ClickAction()
    {
        if (!smoking && ground != null)
        {
            if ((ground.groundType == Ground.GroundType.DIRT && equipment == Equipment.BROOM) ||
                (ground.groundType == Ground.GroundType.WET && equipment == Equipment.SPONGE))
            {
                smoking = true;
                SmokeAnimation();
                StartCoroutine(SmokeWaitRoutine());
                ground.Clean();
            }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Broom"))
        {
            broom.DOMove(transform.position, 0.2f).onComplete = () => broom.gameObject.SetActive(false);
            broomCollider.enabled = false;
            if(equipment == Equipment.SPONGE)
            {
                sponge.gameObject.SetActive(true);
                Vector2 first = transform.position;
                Vector2 second = transform.position;
                sponge.position = first;
                first.y += 0.3f;
                second.y -= 1.4f;
                sponge.DOMove(first, 0.5f).onComplete = () => sponge.DOMove(second, .5f).SetEase(Ease.OutBounce).onComplete = () => spongeCollider.enabled = true;

            }
            UIManager.SetItemImage(Equipment.BROOM);
            equipment = Equipment.BROOM;
        }
        else if(collision.CompareTag("Sponge"))
        {
            sponge.DOMove(transform.position, 0.2f).onComplete = () => sponge.gameObject.SetActive(false);
            spongeCollider.enabled = false;
            if (equipment == Equipment.BROOM)
            {
                broom.gameObject.SetActive(true);
                Vector2 first = transform.position;
                Vector2 second = transform.position;
                broom.position = first;
                first.y += 0.3f;
                second.y -= 1.4f;
                broom.DOMove(first, 0.5f).onComplete = () => broom.DOMove(second, .5f).SetEase(Ease.OutBounce).onComplete = ()=> broomCollider.enabled = true;
            }
            UIManager.SetItemImage(Equipment.SPONGE);

            equipment = Equipment.SPONGE;
        }
        else if(collision.CompareTag("Wet"))
        {
            ground = collision.GetComponent<Ground>();
        }
        else if(collision.CompareTag("Dirt"))
        {
            ground = collision.GetComponent<Ground>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wet"))
        {
            ground = null;
        }
        else if (collision.CompareTag("Dirt"))
        {
            ground = null;
        }
    }
}
