using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EntranceView : MonoBehaviour
{
    [SerializeField] Transform _logo;
    [SerializeField] Transform _playButton;

    public void OnEnable()
    {
        _logo.DOLocalMoveY(390f, 1f).SetEase(Ease.InOutBounce);
    }
    public void OnStartButtonClicked()
    {
        _logo.DOLocalMoveY(800f, 1f).onComplete = () => gameObject.SetActive(false);
    }
}
