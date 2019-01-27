using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class EntranceView : MonoBehaviour
{
    public static event Action GameStarted;

    [SerializeField] Transform _logo;
    [SerializeField] Transform _playButton;
    [SerializeField] Transform _motherEntrance;
    [SerializeField] GameObject _background;

    public void OnEnable()
    {
        _logo.DOLocalMoveY(360f, .75f).SetEase(Ease.OutBounce);
    }
    public void OnStartButtonClicked()
    {
        _logo.DOLocalMoveY(800f, .5f).SetEase(Ease.InQuad);
        _motherEntrance.DOLocalMoveX(0, .5f).SetEase(Ease.OutQuad).onComplete = () =>
        {
            StartCoroutine(Wait());
            _background.SetActive(false);
            _playButton.gameObject.SetActive(false);
        };
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);

        _motherEntrance.DOLocalMoveX(1920f, .5f).SetEase(Ease.InQuad).onComplete = () =>
        {
            GameStarted?.Invoke();
        };
    }
        
}
