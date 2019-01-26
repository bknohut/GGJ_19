using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ground : MonoBehaviour
{
    public enum GroundType { WET, DIRT };
    public GroundType groundType;

    public Collider2D col;
    public SpriteRenderer spriteRenderer;

    public void Clean()
    {
        spriteRenderer.DOFade(0, 1);
        col.enabled = false;
    }

    public void Reappear()
    {
        spriteRenderer.DOFade(1, 1).onComplete = () => col.enabled = true;
    }
}
