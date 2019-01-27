using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    [SerializeField] GameObject _pauseButton;
    [SerializeField] GameObject _inventory;
    [SerializeField] Image _inventoryIcon;
    [SerializeField] GameObject _progressBar;
    [SerializeField] Image _progressBarFill;
    [SerializeField] Sprite _broom;
    [SerializeField] Sprite _sponge;
    [SerializeField] Sprite _emptyInventory;

    
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
        _pauseButton.SetActive(true);
        _inventory.SetActive(true);
        _progressBar.SetActive(true);
        _progressBarFill.fillAmount = 1f;
        StartCoroutine(StartGameTimer(90));
    }
    public void SetInventoryImage(CharacterController.Equipment equipment)
    {
        if(equipment == CharacterController.Equipment.BROOM)
        {
            _inventoryIcon.sprite = _broom;
        }
        else if(equipment == CharacterController.Equipment.SPONGE)
        {
            _inventoryIcon.sprite  = _sponge;
        }
        else
        {
            Debug.LogError("INVALID ITEM");
        }
    }
    public void OnRetry()
    {
        _inventoryIcon.sprite = _emptyInventory;
        _progressBarFill.fillAmount = 1f;
        StopAllCoroutines();
        StartCoroutine(StartGameTimer(90));
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
            i--;
        }
        EndGameView.TimeUp();
    }
    
}
