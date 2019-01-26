using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public Animator animator;

    public void Turn()
    {

    }

    public void RunAnimation()
    {
        animator.SetTrigger("Run");
    }

    public void IdleAnimation()
    {
        animator.SetTrigger("Idle");
    }

    public void Smoke0Animation()
    {
        animator.SetTrigger("Smoke0");
    }

    public void Smoke1Animation()
    {
        animator.SetTrigger("Smoke1");
    }
}
