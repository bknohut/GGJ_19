using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class EntranceView : MonoBehaviour
{
    public static event Action GameStarted;

    [SerializeField] Transform _logo;
    [SerializeField] Transform _playButton;

    public void OnEnable()
    {
        _logo.DOLocalMoveY(360f, 1f).SetEase(Ease.InOutBounce);
    }
    public void OnStartButtonClicked()
    {
        _logo.DOLocalMoveY(800f, 1f).onComplete = () => 
                                {
                                    GameStarted?.Invoke();
                                    gameObject.SetActive(false); };
                                }
}
