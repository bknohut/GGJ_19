using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    [SerializeField] GameObject _inventory;
    [SerializeField] Image _inventoryIcon;
    [SerializeField] GameObject _progressBar;
    [SerializeField] Image _progressBarFill;
    [SerializeField] Sprite _broom;
    [SerializeField] Sprite _sponge;

    
    private void OnEnable()
    {
        EntranceView.GameStarted += OnGameStarted;
    }
    private void OnDisable()
    {
        EntranceView.GameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        _inventory.SetActive(true);
        _progressBar.SetActive(true);
        _progressBarFill.fillAmount = 0f;
        StartCoroutine(StartGameTimer(10));
    }
    public void SetInventoryImage(bool a)
    {
        if( a )
        {
            _inventoryIcon.sprite = _broom;
        }
        else
        {
            _inventoryIcon.sprite  = _sponge;
        }
    }
    private IEnumerator StartGameTimer(int time)
    {
        int i = time * 10;
        float passedTime = 0f;
        while(i > 0)
        {
            yield return new WaitForSeconds(0.1f);
            passedTime += 0.1f;
            _progressBarFill.fillAmount = (time - passedTime )/ time;
        }
    }
}
